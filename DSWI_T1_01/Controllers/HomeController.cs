using DSWI_T1_01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DSWI_T1_01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Moto()
        {
            decimal precioMoto = 6000m;
            ViewBag.PrecioMoto = precioMoto;

            var opciones = new List<ComprarMoto>
            {
                new ComprarMoto("Martes", 12, precioMoto),
                new ComprarMoto("Sábado", 18, precioMoto),
                new ComprarMoto("Feriado", 25, precioMoto)
            };

            return View(opciones);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
