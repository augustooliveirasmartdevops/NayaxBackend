using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Nayax.Dex.Api.Authentication;
using Nayax.Dex.Application.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    // Get allowed hosts from appsetting file
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

    // Define the Basic Authentication scheme
    options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        Description = "Enter your username and password."
    });

    // Require the scheme for all operations by default
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "basic"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Add Basic Authentication
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
