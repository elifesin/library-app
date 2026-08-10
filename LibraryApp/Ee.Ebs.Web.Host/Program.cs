using Ee.Ebs.Application;
using Ee.Ebs.Data.EfCore;
using Ee.Ebs.Data.EfCore.Contexts;
using Ee.Ebs.Web.Razor.Library;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationModuleServices()
    .AddLibraryModuleServices();

builder.Services.AddAutoMapper(cfg => { }, 
    typeof(LibraryModule),
    typeof(ApplicationModule)  
);

//builder.Services.AddDataLayerServices();
builder.Services.AddDataEFLayerServices();

builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation()
    .AddApplicationPart(typeof(Ee.Ebs.Web.Razor.Library.LibraryModule).Assembly);

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();