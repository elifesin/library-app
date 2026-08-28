var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
// builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",  // Angular
                "http://localhost:5173"  // Vue
            ) 
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
// }
// else
// {
//     app.UseDeveloperExceptionPage();
//
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseCors("AllowMyFrontend");
app.MapReverseProxy();

app.Run();