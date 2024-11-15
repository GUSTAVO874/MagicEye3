using MagicEye2.Services.AuthAPI.Data;
using MagicEye2.Services.AuthAPI.Models;
using MagicEye2.Services.AuthAPI.Service;
using MagicEye2.Services.AuthAPI.Service.IService;
using MagicEye2.Services.AuthAPI.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Agregar el contexto de base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService,AuthService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//de chatgpt para validar email
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values.SelectMany(v => v.Errors)
                                                  .Select(e => e.ErrorMessage);
            var response = new ResponseDto
            {
                IsSuccess = false,
                Message = string.Join(" | ", errors)
            };
            return new BadRequestObjectResult(response);
        };
    });

//anadir cors
// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7073") // URL de tu aplicación Blazor WebAssembly
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Usar CORS
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();//linea nueva para autenticar
app.UseAuthorization();

app.MapControllers();

app.Run();
