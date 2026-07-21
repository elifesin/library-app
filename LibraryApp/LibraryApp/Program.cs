using Microsoft.EntityFrameworkCore;
using LibaryApp.Data;

var builder = WebApplication.CreateBuilder(args);

// --- TÜM SERVİSLER BURADA EKLENMELİ (BUILD'DEN ÖNCE) ---
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// -----------------------------------------------------

// Servis kayıtları bittikten sonra uygulama inşa edilir:
var app = builder.Build(); 

// --- BUILD İŞLEMİNDEN SONRA SADECE MIDDLEWARE'LER YAZILIR ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<LibraryDbContext>();
        bool canConnect = context.Database.CanConnect();
        
        if (canConnect)
        {
            Console.WriteLine("--> HARİKA! Veritabanına başarıyla bağlanıldı.");
        }
        else
        {
            Console.WriteLine("--> HATA: Veritabanına ulaşılamıyor. Bilgileri kontrol et.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"--> BAĞLANTI HATASI DETAYI: {ex.Message}");
    }
}

app.Run(); 