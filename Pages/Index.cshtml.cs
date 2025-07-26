using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; // Usando a biblioteca robusta e correta.

namespace AnalisadorJson.Pages
{
    public class IndexModel : PageModel
    {
        // --- CONTRATO ALINHADO COM O FORMULÁRIO ---

        [BindProperty]
        [Required(ErrorMessage = "Por favor, selecione um arquivo.")]
        public IFormFile? JsonFile { get; set; }

        [BindProperty, Required(ErrorMessage = "O nome da propriedade é obrigatório.")]
        public string? PropertyNameToCount { get; set; }
        
        // O valor da busca NÃO é obrigatório, permitindo buscas por strings vazias ou apenas para ver a contagem total.
        [BindProperty] 
        public string? PropertyValueToMatch { get; set; }

        public AnalysisResult? Result { get; private set; }
        
        public class JsonValidationModel
        {
            // O nome da propriedade aqui deve corresponder EXATAMENTE à chave que enviamos no JavaScript.
            public string? jsonText { get; set; }
        }

        public void OnGet() { }
        
        public IActionResult OnPostValidateJson([FromBody] JsonValidationModel data)
        {
            string jsonText = data.jsonText!;

            if (string.IsNullOrWhiteSpace(jsonText))
            {
                return new JsonResult(new { isValid = false, error = "A área de texto está vazia." });
            }

            try
            {
                // A MÁGICA DA NEWTONSOFT: Tentamos ler o texto.
                // Usamos JToken.Parse, que é genérico e aceita tanto objetos {} quanto arrays [].
                JToken.Parse(jsonText);
                
                // Se a linha acima não lançou uma exceção, o JSON é válido!
                // Retornamos o próprio JSON para o frontend usar na árvore.
                return new JsonResult(new { isValid = true, json = jsonText });
            }
            catch (JsonReaderException ex)
            {
                // Se pegarmos uma exceção, o JSON é inválido.
                // Retornamos uma mensagem de erro precisa para o usuário.
                string errorMessage = $"JSON inválido. Linha {ex.LineNumber}, Posição {ex.LinePosition}: {ex.Message}";
                return new JsonResult(new { isValid = false, error = errorMessage });
            }
            catch (Exception ex)
            {
                // Captura outros erros inesperados.
                return new JsonResult(new { isValid = false, error = $"Ocorreu um erro inesperado: {ex.Message}" });
            }
        }

        public IActionResult OnPost()
        {
            PropertyValueToMatch ??= string.Empty;

            if (!ModelState.IsValid)
            {
                // Se a validação falhar, retornamos um Bad Request que o JavaScript pode tratar.
                return BadRequest(ModelState);
            }

            var stopwatch = Stopwatch.StartNew();
            long matchingItems = 0;
            long nonMatchingItems = 0;

            try
            {
                using var stream = JsonFile?.OpenReadStream();
                using var streamReader = new StreamReader(stream!);
                using var jsonReader = new JsonTextReader(streamReader);

                // --- INÍCIO DA CORREÇÃO ---
                const char separator = '|';
                bool hasMultipleValues = PropertyValueToMatch.Contains(separator);

                // Prepara a lista de busca se houver múltiplos valores.
                var searchValues = hasMultipleValues
                    ? new HashSet<string>(PropertyValueToMatch.Split(separator).Select(v => v.Trim()), StringComparer.OrdinalIgnoreCase)
                    : null;

                // A busca global permanece a mesma.
                while (jsonReader.Read())
                {
                    if (jsonReader.TokenType == JsonToken.PropertyName && (string)jsonReader.Value! == PropertyNameToCount)
                    {
                        jsonReader.Read(); // Avança para o valor.

                        bool isMatch = false;

                        // Lógica de comparação aprimorada.
                        if (hasMultipleValues)
                        {
                            // Para múltiplos valores, incluindo "null" se estiver na lista.
                            string jsonValueAsString = (jsonReader.TokenType == JsonToken.Null) ? "null" : (jsonReader.Value?.ToString() ?? "");
                            isMatch = searchValues!.Contains(jsonValueAsString);

                        }
                        else // Lógica para valor único (a mesma de antes, mas mais limpa).
                        {
                            if (PropertyValueToMatch.Equals("null", StringComparison.OrdinalIgnoreCase))
                            {
                                isMatch = (jsonReader.TokenType == JsonToken.Null);
                            }
                            else
                            {
                                string jsonValueAsString = jsonReader.Value?.ToString() ?? "";
                                isMatch = jsonValueAsString.Equals(PropertyValueToMatch, StringComparison.OrdinalIgnoreCase);
                            }
                        }

                        if (isMatch)
                        {
                            matchingItems++;
                        }
                        else
                        {
                            nonMatchingItems++;
                        }
                    }
                }

            }
            catch (JsonReaderException ex)
            {
                ModelState.AddModelError("JsonFile", $"Erro fatal de JSON na Linha {ex.LineNumber}, Posição {ex.LinePosition}: {ex.Message}.");
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Ocorreu um erro inesperado: {ex.Message}");
                return BadRequest(ModelState);
            }

            stopwatch.Stop();

            Result = new AnalysisResult(matchingItems, nonMatchingItems, stopwatch.Elapsed.TotalMilliseconds);

            // ***** MUDANÇA PRINCIPAL *****
            // Em vez de retornar a página inteira, retornamos apenas o HTML da partial view.
            return Partial("Shared/_AnalysisResultPartial", this);
        }
    }

    public record AnalysisResult(long MatchingItems, long NonMatchingItems, double ElapsedMilliseconds)
    {
        // Soma os dois contadores para dar o total de propriedades encontradas.
        public long TotalPropertiesFound => MatchingItems + NonMatchingItems;
    }
}