using Microsoft.AspNetCore.Mvc;
using MagicEye3.ClienteMVC.Web.Models; // Para CarreraPerCicParcDto
using MagicEye3.ClienteMVC.Web.Service.IService;       // Para ICarreraPerCicParcService

namespace TuProyectoMVC.Controllers
{
    public class CarreraPerCicParcController : Controller
    {
        private readonly ICarreraPerCicParcService _service;

        public CarreraPerCicParcController(ICarreraPerCicParcService service)
        {
            _service = service;
        }

        // GET: /CarreraPerCicParc/Create
        public IActionResult Create()
        {
            // Devuelve la vista con un modelo nuevo
            var model = new CarreraPerCicParcDto
            {
                Carrera = new CarreraDto(),
                Periodo = new PeriodoDto(),
                Ciclo = new CicloDto(),
                Parcial = new ParcialDto()
            };
            return View(model);
        }

        // POST: /CarreraPerCicParc/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarreraPerCicParcDto model)
        {
            // 1) Validar el modelo
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 2) Llamar al servicio que invoca tu API
            var response = await _service.CreateAsync(model);

            if (response != null && response.IsSuccess)
            {
                // Exito: redirigir o mostrar mensaje
                TempData["Success"] = "Registro creado exitosamente.";
                return RedirectToAction("Index", "Home"); // O donde quieras
            }
            else
            {
                // Error: mostrar mensaje en la misma vista
                ModelState.AddModelError("", response?.Message ?? "Error desconocido al crear.");
                return View(model);
            }
        }
    }
}
