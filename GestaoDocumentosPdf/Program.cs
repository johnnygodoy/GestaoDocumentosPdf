using GestaoDocumentosPdf.Components;
using GestaoDocumentosPdf.Data;
using GestaoDocumentosPdf.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// DbContext Firebird
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseFirebird(builder.Configuration.GetConnectionString("DefaultConnection")));

// REGISTRA serviço de sessão via estado Scoped (sem HttpContext)
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<SessaoScoped>();



var app = builder.Build();

// Cria DB e dados iniciais
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();

    if (!context.Usuarios.Any())
    {
        context.Usuarios.Add(new Usuario
        {
            Nome = "Admin",
            Email = "admin@admin.com",
            Senha = "123456",
            Ativo = true
        });

        context.SaveChanges();
    }
}

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
