using DSWI_T1_01.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSWI_T1_01.Controllers
{
    public class ConsumoElectricoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new ConsumoElectricoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ConsumoElectricoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            double kwh = model.KwhConsumidos;

            // Tramo 1: primeros 100 kWh a 0.10
            double tramo1 = Math.Min(kwh, 100.0);
            decimal costoTramo1 = (decimal)tramo1 * 0.10m;

            // Tramo 2: siguientes 150 kWh (101 a 250) a 0.15
            double tramo2 = kwh > 100.0 ? Math.Min(kwh - 100.0, 150.0) : 0.0;
            decimal costoTramo2 = (decimal)tramo2 * 0.15m;

            // Tramo 3: excedente sobre 250 kWh a 0.22
            double tramo3 = kwh > 250.0 ? kwh - 250.0 : 0.0;
            decimal costoTramo3 = (decimal)tramo3 * 0.22m;

            decimal subtotal = costoTramo1 + costoTramo2 + costoTramo3;

            // Impuesto: 8% si consumo > 500 kWh, de lo contrario 3%
            decimal tasaImpuesto = kwh > 500.0 ? 0.08m : 0.03m;
            decimal impuesto = subtotal * tasaImpuesto;
            decimal total = subtotal + impuesto;

            model.Subtotal = Math.Round(subtotal, 2);
            model.PorcentajeImpuesto = tasaImpuesto * 100m;
            model.MontoImpuesto = Math.Round(impuesto, 2);
            model.TotalPagar = Math.Round(total, 2);
            model.Calculado = true;

            return View(model);
        }
    }
}