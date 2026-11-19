using DSWI_T1_01.Data;
using DSWI_T1_01.Repositories;
using DSWI_T1_01.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Servicios de MVC
builder.Services.AddControllersWithViews();

// Conexión a SQL Server mediante Entity Framework Core
builder.Services.AddDbContext<InventarioContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("InventarioBD")));

// Inyección de dependencias: Repositories
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IInventarioRepository, InventarioRepository>();

// Inyección de dependencias: Services
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IInventarioService, InventarioService>();

var app = builder.Build();

// Configuración de errores
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();