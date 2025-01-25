using ModelArena;
using ModelArena.Components;
using ModelArena.Models;

var builder = WebApplication.CreateBuilder(args);

// Bind configuration
builder.Services.Configure<List<AzureAIModelConfig>>(
    builder.Configuration.GetSection("AzureAIModels"));

// Add model manager as a singleton
builder.Services.AddSingleton<AzureAIModelManager>();

builder.Services.AddSingleton<ModelSessionServiceFactory>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();