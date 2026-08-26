using Ee.Ebs.Application;
using Ee.Ebs.Data.EfCore;
using Ee.Ebs.Data.EfCore.Contexts;
using Ee.Ebs.Domain;
using Ee.Ebs.Web.Api.ExceptionHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationModuleServices()
    .AddDomainModuleServices();

//builder.Services.AddDataLayerServices();
builder.Services.AddDataEFLayerServices();

builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",  // Angular
                "http://localhost:5173"   // Vue
            ) 
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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
app.UseCors("AllowMyFrontend");
app.MapControllers();
app.UseExceptionHandler();
app.Run();