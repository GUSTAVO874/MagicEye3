using AutoMapper;
using MagicEye3.Services.BackEndAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

using jsreport.AspNetCore;
using jsreport.Local;
using jsreport.Types;
using jsreport.Binary;
using System.IO;
using System.Diagnostics;
using MagicEye3.Services.BackEndAPI.Engines.Queries;
using MagicEye3.Services.BackEndAPI.Engines.Render;

var builder = WebApplication.CreateBuilder(args);

//// Configurar jsreport como utilidad sin middleware, funcionó para generar pdf
//builder.Services.AddJsReport(new LocalReporting()
//    .UseBinary(JsReportBinary.GetBinary())
//    .Configure(cfg => cfg
//        .DoTrustUserCode() // Reemplaza AllowedLocalFilesAccess()
//        .BaseUrlAsWorkingDirectory()
//    // Eliminar ReadResponseHeadersFromServer()
//    )
//    .AsUtility()
//    .Create());
// Configurar jsreport para usar la instalación personalizada
//builder.Services.AddJsReport(new LocalReporting()
//    .UseBinary(Process.GetCurrentProcess().MainModule.FileName) // Utiliza el ejecutable actual
//    .Configure(cfg => cfg
//        .DoTrustUserCode()
//        .BaseUrlAsWorkingDirectory()
//    )
//    .KillRunningJsReportProcesses()
//    .RunInDirectory(Path.Combine(Directory.GetCurrentDirectory(), "jsreport")) // Ruta a la carpeta jsreport
//    .AsUtility()
//    .Create());

// Configurar jsreport para usar la instalación personalizada
//builder.Services.AddJsReport(new LocalReporting()
//    .RunInDirectory(Path.Combine(Directory.GetCurrentDirectory(), "jsreport")) // Ruta a la carpeta jsreport
//    .Configure(cfg => cfg
//        .DoTrustUserCode()
//        .BaseUrlAsWorkingDirectory()
//    )
//    .KillRunningJsReportProcesses()
//    .AsUtility()
//    .Create());


// Agregar el contexto de base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<jsreport.Client.IReportingService>(_ =>
    new jsreport.Client.ReportingService(
        // Constructor con URL, user y pass
        "http://localhost:5488",
        "admin",
        "password"
    )
);

builder.Services.AddScoped<AnaliticoQueries>();
builder.Services.AddScoped<JsReportRenderService>();

// Registrar servicio AutoMapper
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MagicEye2.Services.BackEndAPI.MappingConfig>();
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

// Agregar controladores
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = @"Introduzca 'Bearer' [espacio] y luego su token válido en el campo de texto a continuación.
Ejemplo: 'Bearer abcdef12345'",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});

//en MagicEye2 había extensión personalizada builder.AddAppAuthetication(); esta ez
//chatgpt lo puso directamente en este archivo

// Configurar autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true; // Requiere HTTPS para los metadatos (recomendado en producción)
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["ApiSettings:Issuer"],
        ValidAudience = builder.Configuration["ApiSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ApiSettings:Secret"]))
    };
});

builder.Services.AddAuthorization();

// Leer los orígenes permitidos desde la configuración
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

// Configurar CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy", policy =>
//    {
//        policy.WithOrigins(allowedOrigins)
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();

// Usar jsreport middleware
//app.UseJsReport();

// Usar CORS
app.UseCors("CorsPolicy");

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
