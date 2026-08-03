using Data;
using DataEF;
using DataEF.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDataLayerServices();
builder.Services.AddDataEFLayerServices();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(cfg => { }, typeof(Program));
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();// MVC sistemi aktif edilir.

var app = builder.Build(); // Uygulamayı oluştur.
    

// Standart Middleware ayarları
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run(); 