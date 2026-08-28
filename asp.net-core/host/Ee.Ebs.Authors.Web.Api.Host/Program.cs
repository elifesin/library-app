using Ee.Ebs.Application;
using Ee.Ebs.Data.EfCore;
using Ee.Ebs.Data.EfCore.Contexts;
using Ee.Ebs.Domain;
using Ee.Ebs.Web.Api.Shared.ExceptionHandlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationModuleServices()
    .AddDomainModuleServices();

builder.Services.AddDataEFLayerServices();

builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.UseExceptionHandler();

app.Run();