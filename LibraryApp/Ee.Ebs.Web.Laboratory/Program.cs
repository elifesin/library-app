using Ee.Ebs.Application;
using Ee.Ebs.Data.Dapper;
using Ee.Ebs.Data.EfCore;
using Ee.Ebs.Data.EfCore.Contexts;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationModuleServices();

builder.Services.AddAutoMapper(cfg => { }, 
    typeof(Program),                        // Web katmanındaki profilleri (AuthorMappingProfile vb.) tarar
    typeof(Ee.Ebs.Application.ApplicationModule)   // Ee.Ebs.Application katmanındaki profilleri (AuthorProfile vb.) tarar
);

builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDataEFLayerServices();

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