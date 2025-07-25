using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.Configure<FormOptions>(options =>
{
    // Define o limite para o tamanho de cada parte individual do multipart/form-data.
    // Essencial para uploads de arquivos. Vamos colocar um limite generoso.
    options.MultipartBodyLengthLimit = 500 * 1024 * 1024; // 500 MB
});

// 2. Configurar os limites do Kestrel (o servidor web padrão)
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // Define o limite para o tamanho total do corpo da requisição.
    // É a configuração mais importante para este problema.
    // Colocar 'null' desabilita o limite, mas é mais seguro definir um valor alto.
    serverOptions.Limits.MaxRequestBodySize = 500 * 1024 * 1024; // 500 MB
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
