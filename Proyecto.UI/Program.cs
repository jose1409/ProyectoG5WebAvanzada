using System.Net.Http;
using Proyecto.UI.Repository.CategoriaRepository;
using Proyecto.UI.Repository.ProductoRepository;
using Proyecto.UI.Utils;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews()
    .AddJsonOptions(o =>
    {
        // Por si consumís JSON con camelCase
        o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Repositorios UI
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

// HttpContext + Session
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(opt =>
{
    opt.Cookie.Name = ".BoteroBeauty.Session";
    opt.IdleTimeout = TimeSpan.FromHours(2);
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
});

// Utils
builder.Services.AddScoped<IUtilitarios, Utilitarios>();

// (OPCIONAL) Handler para agregar automáticamente el Bearer al HttpClient "API"
// Si usas JWT en la UI y guardas el token en Session/Cookie.
// Descomenta estas 20 líneas y ajusta de dónde lees el token.
/*
builder.Services.AddTransient<AuthTokenHandler>();
*/

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7093/"); // ?? URL de tu API
    client.Timeout = TimeSpan.FromSeconds(100);
})
// (OPCIONAL) agrega el handler que mete el Bearer al header Authorization
//.AddHttpMessageHandler<AuthTokenHandler>()
.ConfigurePrimaryHttpMessageHandler(sp =>
{
    // En Development, ignorar certificado de localhost
    if (builder.Environment.IsDevelopment())
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) => true
        };
    }
    return new HttpClientHandler();
});

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();        // ?? antes de Auth si usas token en Session
// app.UseAuthentication(); // si usas cookies/JWT en la UI
app.UseAuthorization();

// Rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Autenticacion}/{action=Index}/{id?}");

app.UseStaticFiles(); // para servir wwwroot/js/carrito.js
app.UseSession();     // si tu carrito usa Session en el backend


app.Run();




// ========== OPCIONAL: Handler para propagar JWT ==========
/*
using System.Net.Http.Headers;
 
public class AuthTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _http;
 
    public AuthTokenHandler(IHttpContextAccessor http) => _http = http;
 
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Lee el token desde Session o Cookie (ajusta a tu implementación real)
        var token = _http.HttpContext?.Session.GetString("JWT"); // o desde cookie
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return base.SendAsync(request, cancellationToken);
    }
}
*/
