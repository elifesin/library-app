using Data;

var builder = WebApplication.CreateBuilder(args);

// Data Layes Servies
builder.Services.AddDataLayerServices();

// Web Layer Services
builder.Services.AddAutoMapper(cfg => { }, typeof(Program));
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();// MVC sistemi aktif edilir.

var app = builder.Build();

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