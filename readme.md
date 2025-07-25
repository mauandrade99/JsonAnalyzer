
```markdown
# Analisador Global de Propriedades JSON

Este projeto é uma aplicação web simples e poderosa construída com ASP.NET Core Razor Pages. Seu principal objetivo é analisar arquivos JSON de grande volume (centenas de megabytes) de forma eficiente, sem sobrecarregar a memória do servidor, para contar ocorrências de propriedades específicas.

A ferramenta foi projetada para ser genérica, permitindo ao usuário especificar qual propriedade (chave) deseja encontrar e qual valor associado a ela deve ser contado, tratando corretamente casos como valores nulos, strings vazias, números e booleanos.



## ✨ Funcionalidades Principais

-   **Processamento de Arquivos Gigantes:** Utiliza leitura em streaming com a biblioteca `Newtonsoft.Json` para analisar arquivos de qualquer tamanho com consumo mínimo de memória.
-   **Análise Genérica:** O usuário pode definir dinamicamente a propriedade e o valor a serem buscados em todo o documento JSON.
-   **Interface Responsiva:** Construída com Bootstrap, a interface é limpa e se adapta a diferentes tamanhos de tela.
-   **Experiência de Usuário Moderna:** Usa AJAX para enviar o formulário, evitando o recarregamento da página e permitindo que o arquivo selecionado seja mantido para múltiplas análises.
-   **Contagem Detalhada:** Fornece um resumo claro dos resultados, incluindo:
    -   O total de vezes que a propriedade foi encontrada.
    -   O número de correspondências exatas com o valor especificado.
    -   O número de ocorrências com valores diferentes.

## 🚀 Tecnologias Utilizadas

-   **Backend:** C# com ASP.NET Core 6.0 (ou superior)
    -   **Framework Web:** Razor Pages
    -   **Manipulação de JSON:** `Newtonsoft.Json` (Json.NET)
-   **Frontend:**
    -   HTML5 & CSS3
    -   Bootstrap 5
    -   JavaScript (Fetch API para requisições AJAX)
-   **Servidor Web:** Kestrel

## ⚙️ Como Executar o Projeto

### Pré-requisitos

-   [.NET SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0) ou mais recente.
-   Um editor de código como [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/).

### Passos para Execução

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/mauandrade99/JsonAnalyzer.git
    cd analisador-json
    ```

2.  **Restaure as dependências:**
    O .NET CLI fará o download da biblioteca `Newtonsoft.Json` e outras dependências necessárias.
    ```bash
    dotnet restore
    ```

3.  **Execute a aplicação:**
    ```bash
    dotnet run
    ```

4.  **Acesse no navegador:**
    Após a execução, o terminal mostrará as URLs onde a aplicação está rodando. Geralmente será algo como:
    -   `https://localhost:7123`
    -   `http://localhost:5123`

    Abra uma dessas URLs no seu navegador de preferência.

## 📄 Lógica de Funcionamento

A aplicação foi cuidadosamente projetada para lidar com a principal restrição de arquivos grandes: o consumo de memória.

1.  **Limite de Upload Aumentado:** O servidor Kestrel e as opções de formulário do ASP.NET Core foram configurados no arquivo `Program.cs` para aceitar requisições de até 500 MB.
2.  **Leitura em Streaming:** Ao receber o arquivo, o método `OnPost` no backend não o carrega na memória. Em vez disso, ele abre um `Stream` e usa o `JsonTextReader` da `Newtonsoft.Json`.
3.  **Análise Token a Token:** O leitor percorre o arquivo JSON token por token (`{`, `}`, `[`, `]`, nomes de propriedades, valores).
4.  **Busca Global:** O código simplesmente procura por um `JsonToken.PropertyName` que corresponda ao texto fornecido pelo usuário. Ao encontrar, ele avança para o próximo token (o valor) e realiza a comparação.
5.  **Resposta Parcial (AJAX):** Para evitar recarregamentos de página, o servidor responde à requisição AJAX com uma "Partial View" contendo apenas o HTML dos resultados. O JavaScript no frontend então injeta esse HTML no local apropriado da página.

## 💡 Possíveis Melhorias Futuras

-   Adicionar mais tipos de análise (ex: "valor contém o texto", "valor é um número maior que").
-   Implementar uma barra de progresso para arquivos muito grandes.
-   Permitir a análise de JSON a partir de uma URL externa.

---

_Este projeto foi desenvolvido como uma solução prática para a análise eficiente de grandes volumes de dados em formato JSON._
```
### Como Usar:

1.  Copie todo o texto abaixo.
2.  No seu projeto (no Visual Studio Code ou no site do GitHub/GitLab), crie um novo arquivo chamado `README.md` na pasta raiz.
3.  Cole o texto nesse arquivo e salve. O Git irá reconhecê-lo e exibi-lo lindamente na página principal do seu repositório.

---

```markdown
# Analisador Global de Propriedades JSON

Este projeto é uma aplicação web simples e poderosa construída com ASP.NET Core Razor Pages. Seu principal objetivo é analisar arquivos JSON de grande volume (centenas de megabytes) de forma eficiente, sem sobrecarregar a memória do servidor, para contar ocorrências de propriedades específicas.

A ferramenta foi projetada para ser genérica, permitindo ao usuário especificar qual propriedade (chave) deseja encontrar e qual valor associado a ela deve ser contado, tratando corretamente casos como valores nulos, strings vazias, números e booleanos.

 <!-- Opcional: Tire um screenshot e substitua a URL -->

## ✨ Funcionalidades Principais

-   **Processamento de Arquivos Gigantes:** Utiliza leitura em streaming com a biblioteca `Newtonsoft.Json` para analisar arquivos de qualquer tamanho com consumo mínimo de memória.
-   **Análise Genérica:** O usuário pode definir dinamicamente a propriedade e o valor a serem buscados em todo o documento JSON.
-   **Interface Responsiva:** Construída com Bootstrap, a interface é limpa e se adapta a diferentes tamanhos de tela.
-   **Experiência de Usuário Moderna:** Usa AJAX para enviar o formulário, evitando o recarregamento da página e permitindo que o arquivo selecionado seja mantido para múltiplas análises.
-   **Contagem Detalhada:** Fornece um resumo claro dos resultados, incluindo:
    -   O total de vezes que a propriedade foi encontrada.
    -   O número de correspondências exatas com o valor especificado.
    -   O número de ocorrências com valores diferentes.

## 🚀 Tecnologias Utilizadas

-   **Backend:** C# com ASP.NET Core 6.0 (ou superior)
    -   **Framework Web:** Razor Pages
    -   **Manipulação de JSON:** `Newtonsoft.Json` (Json.NET)
-   **Frontend:**
    -   HTML5 & CSS3
    -   Bootstrap 5
    -   JavaScript (Fetch API para requisições AJAX)
-   **Servidor Web:** Kestrel

## ⚙️ Como Executar o Projeto

### Pré-requisitos

-   [.NET SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0) ou mais recente.
-   Um editor de código como [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/).

### Passos para Execução

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/mauandrade99/JsonAnalyzer.git
    cd analisador-json
    ```

2.  **Restaure as dependências:**
    O .NET CLI fará o download da biblioteca `Newtonsoft.Json` e outras dependências necessárias.
    ```bash
    dotnet restore
    ```

3.  **Execute a aplicação:**
    ```bash
    dotnet run
    ```

4.  **Acesse no navegador:**
    Após a execução, o terminal mostrará as URLs onde a aplicação está rodando. Geralmente será algo como:
    -   `https://localhost:7123`
    -   `http://localhost:5123`

    Abra uma dessas URLs no seu navegador de preferência.

## 📄 Lógica de Funcionamento

A aplicação foi cuidadosamente projetada para lidar com a principal restrição de arquivos grandes: o consumo de memória.

1.  **Limite de Upload Aumentado:** O servidor Kestrel e as opções de formulário do ASP.NET Core foram configurados no arquivo `Program.cs` para aceitar requisições de até 500 MB.
2.  **Leitura em Streaming:** Ao receber o arquivo, o método `OnPost` no backend não o carrega na memória. Em vez disso, ele abre um `Stream` e usa o `JsonTextReader` da `Newtonsoft.Json`.
3.  **Análise Token a Token:** O leitor percorre o arquivo JSON token por token (`{`, `}`, `[`, `]`, nomes de propriedades, valores).
4.  **Busca Global:** O código simplesmente procura por um `JsonToken.PropertyName` que corresponda ao texto fornecido pelo usuário. Ao encontrar, ele avança para o próximo token (o valor) e realiza a comparação.
5.  **Resposta Parcial (AJAX):** Para evitar recarregamentos de página, o servidor responde à requisição AJAX com uma "Partial View" contendo apenas o HTML dos resultados. O JavaScript no frontend então injeta esse HTML no local apropriado da página.

## 💡 Possíveis Melhorias Futuras

-   Adicionar mais tipos de análise (ex: "valor contém o texto", "valor é um número maior que").
-   Implementar uma barra de progresso para arquivos muito grandes.
-   Permitir a análise de JSON a partir de uma URL externa.

---

_Este projeto foi desenvolvido como uma solução prática para a análise eficiente de grandes volumes de dados em formato JSON._
```