//using AutoMapper;
//using MagicEye3.Services.BackEndAPI.Data;
//using MagicEye3.Services.BackEndAPI.Extensions;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;

//var builder = WebApplication.CreateBuilder(args);

//// Agregar el contexto de base de datos
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Registrar servicio AutoMapper
//var config = new MapperConfiguration(cfg => {
//    cfg.AddProfile<MagicEye2.Services.BackEndAPI.MappingConfig>();
//});

//IMapper mapper = config.CreateMapper();
//builder.Services.AddSingleton(mapper);

//// Agregar controladores
//builder.Services.AddControllers();

//// Configurar Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(option =>
//{
//    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });
//    option.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference= new OpenApiReference
//                {
//                    Type=ReferenceType.SecurityScheme,
//                    Id=JwtBearerDefaults.AuthenticationScheme
//                }
//            }, new string[]{}
//        }
//    });
//});
//builder.AddAppAuthetication();

//builder.Services.AddAuthorization();

//// **Leer los or�genes permitidos desde la configuraci�n**
//var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

//// Configurar CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy", policy =>
//    {
//        policy.WithOrigins(allowedOrigins)
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});

//var app = builder.Build();

//// Usar CORS
//app.UseCors("CorsPolicy");

//// Configurar el pipeline de solicitudes HTTP
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();


using AutoMapper;
using MagicEye3.Services.BackEndAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Agregar el contexto de base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
        Description = @"Introduzca 'Bearer' [espacio] y luego su token v�lido en el campo de texto a continuaci�n.
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

//en MagicEye2 hab�a extensi�n personalizada builder.AddAppAuthetication(); esta ez
//chatgpt lo puso directamente en este archivo

// Configurar autenticaci�n JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true; // Requiere HTTPS para los metadatos (recomendado en producci�n)
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

// Leer los or�genes permitidos desde la configuraci�n
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Usar CORS
app.UseCors("CorsPolicy");

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usar autenticaci�n y autorizaci�n
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
