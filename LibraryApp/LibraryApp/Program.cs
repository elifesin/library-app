using Data;

var builder = WebApplication.CreateBuilder(args);

// Data Layer Services
builder.Services.AddDataLayerServices();

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