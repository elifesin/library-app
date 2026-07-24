using LibraryApp.Data;
using Microsoft.Data.SqlClient; // Raw SQL için gerekli kütüphane

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<LoanRepository>();

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