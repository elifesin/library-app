using Ee.Ebs.Application;
using Ee.Ebs.Data.EfCore;
using Ee.Ebs.Data.EfCore.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationModuleServices();

builder.Services.AddAutoMapper(cfg => { }, 
    typeof(Program),                        // Web katmanındaki profilleri (AuthorMappingProfile vb.) tarar
    typeof(Ee.Ebs.Application.ApplicationModule)   // Ee.Ebs.Application katmanındaki profilleri (AuthorProfile vb.) tarar
);
//builder.Services.AddDataLayerServices();
builder.Services.AddDataEFLayerServices();

builder.Services.AddDbContextPool<AppDbContext>(options =>
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