using Proyecto.UI.Repository.CategoriaRepository;
using Proyecto.UI.Repository.ProductoRepository;
using Proyecto.UI.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configuracion de Repositorios
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

// Configuraci�n de HttpClient para la API (puerto 7093 HTTPS)
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7093/");
});

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// Services
builder.Services.AddScoped<IUtilitarios, Utilitarios>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Autenticacion}/{action=Index}/{id?}");

app.Run();
