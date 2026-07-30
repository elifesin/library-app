using Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<LoanRepository>();
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<MemberRepository>();
builder.Services.AddScoped<PublisherRepository>();
builder.Services.AddScoped<DbConnectionFactory>();
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