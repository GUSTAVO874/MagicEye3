//using MagicEye3.Cliente.Web;
//using MagicEye3.Cliente.Web.Service;
//using MagicEye3.Cliente.Web.Service.IService;
//using Microsoft.AspNetCore.Components.Authorization;
//using Blazored.LocalStorage;
//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

//var builder = WebAssemblyHostBuilder.CreateDefault(args);

//// Configura la URL base de tu API
//builder.Services.AddScoped(sp => new HttpClient 
//{ BaseAddress = new Uri("https://localhost:7240") });

//// Registra Blazored.LocalStorage
//builder.Services.AddBlazoredLocalStorage();

//// Registra tus servicios
//builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<IBaseService, BaseService>();
//builder.Services.AddScoped<ITokenProvider, TokenProvider>();

//// Registra el AuthenticationStateProvider
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
//builder.Services.AddAuthorizationCore();

//builder.RootComponents.Add<App>("#app");
//builder.RootComponents.Add<HeadOutlet>("head::after");

//await builder.Build().RunAsync();

using MagicEye3.Cliente.Web;
using MagicEye3.Cliente.Web.Service;
using MagicEye3.Cliente.Web.Service.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Elimina la BaseAddress del HttpClient
builder.Services.AddScoped(sp => new HttpClient());

// Registra Blazored.LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Registra tus servicios
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<ICarreraService, CarreraService>(); // Añade este registro

// Registra el AuthenticationStateProvider
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
