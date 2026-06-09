
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using EmployeeManagement.Application.Common.DependencyInjection;
using EmployeeManagement.Infrastructure.DependencyInjection;
using EmployeeManagement.Persistence.DependencyInjection;
using EmployeeManagement.API.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(ApiConstants.SwaggerVersion, new OpenApiInfo
    {
        Title = ApiConstants.SwaggerTitle,
        Version = ApiConstants.SwaggerVersion
    });

    options.AddSecurityDefinition(ApiConstants.BearerSecurityScheme, new OpenApiSecurityScheme
    {
        Name = ApiConstants.AuthorizationHeader,
        Type = SecuritySchemeType.Http,
        Scheme = ApiConstants.BearerScheme,
        BearerFormat = ApiConstants.JwtBearerFormat,
        In = ParameterLocation.Header,
        Description = ApiConstants.SwaggerBearerDescription
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = ApiConstants.BearerSecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration[ApiConstants.JwtConfigurationKeys.Issuer],
            ValidateAudience = true,
            ValidAudience = builder.Configuration[ApiConstants.JwtConfigurationKeys.Audience],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[ApiConstants.JwtConfigurationKeys.SecretKey] ?? string.Empty)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

WebApplication app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(ApiConstants.SwaggerJsonEndpoint, ApiConstants.SwaggerDisplayName);
        options.RoutePrefix = ApiConstants.SwaggerRoutePrefix;

    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
