using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Nayax.Dex.Api.Authentication;
using Nayax.Dex.Application.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    string allowedHosts = builder.Configuration.GetSection(key: "CorsSettings:AllowedHosts").Value ?? string.Empty;

    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedHosts.Split(';'))
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


builder.Services.AddControllers();
builder.Services.AddDependenciesInjection();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Nayax",
        Version = "v1",
        Description = "An API for Full-Stack Technical Project"
    });
});

builder.Services.AddAuthentication("Basic")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
