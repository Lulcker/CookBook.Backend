using Autofac;
using Autofac.Extensions.DependencyInjection;
using CookBook.Backend;
using CookBook.Backend.App.Commands.Users;
using CookBook.Backend.App.Helpers;
using CookBook.Backend.App.Mappings;
using CookBook.Backend.Extensions;
using CookBook.Backend.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

#region Database

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CookBookDbContext>(p => p.UseNpgsql(connectionString));

#endregion

#region Auth

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthHelper.Issuer,
            ValidateAudience = true,
            ValidAudience = AuthHelper.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = AuthHelper.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

#endregion

#region Autofac

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterAssemblyModules(typeof(Program).Assembly));

#endregion

#region Automapper

builder.Services.AddAutoMapper(typeof(MappingProfile));

#endregion

#region Swagger

builder.Services.AddSwaggerGen(options =>
{
    const string xmlFilenameBackend = "CookBook.Backend.xml";
    const string xmlFilenameDomain = "CookBook.Backend.Domain.xml";
    const string xmlFilenameApp = "CookBook.Backend.App.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilenameBackend));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilenameDomain));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilenameApp));
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "CookBook API",
            Description = "CookBook API for flutter app",
            Version = "v1"
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

#endregion

builder.Services.AddControllers();

builder.Services.AddLogging(options =>
{
    options.AddFilter("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware", LogLevel.None);
});

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

app.UseExceptionHandler(error => error.Run(GlobalExceptionHandler.Handle));

app.UseCors("CorsPolicy");

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo API");
    options.RoutePrefix = "";
});

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                       ForwardedHeaders.XForwardedProto
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseUserInfo();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CookBookDbContext>();
    if (context.Database.GetPendingMigrations().Any())
        await context.Database.MigrateAsync();

    var createDefaultAdministratorCommand = services.GetRequiredService<CreateDefaultAdministratorCommand>();
    await createDefaultAdministratorCommand.Execute();
}

app.MapControllers();

app.Run();