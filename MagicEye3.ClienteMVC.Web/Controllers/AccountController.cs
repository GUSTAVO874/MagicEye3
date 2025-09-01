using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using System.Web.Mvc; // Si usas ASP.NET MVC 5
// Para ASP.NET Core MVC usa: using Microsoft.AspNetCore.Mvc;
using MagicEye3.ClienteMVC.Web.Models;
using MagicEye3.ClienteMVC.Web.Service.IService;
using MagicEye3.ClienteMVC.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;


namespace MagicEye3.ClienteMVC.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        
        // GET: /Account/Register
        public ActionResult Register()
        {
            // Enviamos la lista de roles a la vista usando ViewBag
            ViewBag.RoleList = new List<string> { SD.RoleAdmin, SD.RoleCustomer };
            return View(new RegistrationRequestDto());
        }

        
        // POST: /Account/Register
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegistrationRequestDto registrationModel)
        {
            // Volvemos a asignar la lista de roles en caso de error
            ViewBag.RoleList = new List<string> { SD.RoleAdmin, SD.RoleCustomer };

            if (!ModelState.IsValid)
            {
                return View(registrationModel);
            }
                
            // Intentar registrar al usuario
            var response = await _authService.RegisterAsync(registrationModel);

            if (response != null && response.IsSuccess)
            {
                // Si no se especificó un rol, asignar el rol por defecto
                if (string.IsNullOrEmpty(registrationModel.Role))
                {
                    registrationModel.Role = SD.RoleCustomer;
                }

                // Asignar el rol al usuario
                var assignRoleResponse = await _authService.AssignRoleAsync(registrationModel);

                if (assignRoleResponse != null && assignRoleResponse.IsSuccess)
                {
                    // Registro y asignación de rol exitosos: redirigimos a la página de login
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", assignRoleResponse?.Message ?? "Error al asignar el rol.");
                }
            }
            else
            {
                ModelState.AddModelError("", response?.Message ?? "Error al registrar el usuario.");
            }

            return View(registrationModel);
        }
        
        // GET: /Account/Login
        public ActionResult Login()
        {
            return View(new LoginRequestDto());
        }

        // POST: /Account/Login
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginRequestDto loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            // Intentar iniciar sesión
            var response = await _authService.LoginAsync(loginModel);

            if (response != null && response.IsSuccess)
            {
                // Deserializamos la respuesta en LoginResponseDto para obtener el token
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));
                string token = loginResponse?.Token;

                if (!string.IsNullOrEmpty(token))
                {
                    // Guardar el token en una cookie HttpOnly
                    HttpContext.Response.Cookies.Append("JWTToken", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,  // Asegúrate de usar HTTPS en producción
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddDays(7) // Ajusta la expiración según lo requerido
                    });
                }

                // Login exitoso: redirigir al usuario, por ejemplo, a la página principal
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Manejar el error de login y mostrar el mensaje en la vista
                ModelState.AddModelError("", response?.Message ?? "Error al iniciar sesión.");
                return View(loginModel);
            }
        }

    }
}
