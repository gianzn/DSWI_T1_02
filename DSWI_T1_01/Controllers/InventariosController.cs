using DSWI_T1_01.Models;
using DSWI_T1_01.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSWI_T1_01.Controllers
{
    public class InventariosController : Controller
    {
        private readonly IInventarioService _inventarioService;
        private readonly IProductoService _productoService;

        public InventariosController(
            IInventarioService inventarioService,
            IProductoService productoService)
        {
            _inventarioService = inventarioService;
            _productoService = productoService;
        }

        // LISTAR INVENTARIO
        public async Task<IActionResult> Index()
        {
            ViewBag.Productos = await _productoService.ObtenerTodosAsync();

            var inventarios = await _inventarioService.ObtenerTodosAsync();

            return View(inventarios);
        }

        // CREAR INVENTARIO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(
            DateOnly? fechaIngreso,
            int? cantidad,
            string? ubicacion,
            string? estado,
            int? productoId)
        {
            if (!DatosValidos(
                fechaIngreso, cantidad, ubicacion, estado, productoId))
            {
                TempData["Error"] = "Complete correctamente todos los campos.";
                return RedirectToAction(nameof(Index));
            }

            if (await _productoService.ObtenerPorIdAsync(
                    productoId!.Value) == null)
            {
                TempData["Error"] = "El producto seleccionado no existe.";
                return RedirectToAction(nameof(Index));
            }

            var inventario = new Inventario
            {
                FechaIngreso = fechaIngreso,
                Cantidad = cantidad,
                Ubicacion = ubicacion!.Trim(),
                Estado = estado!.Trim(),
                ProductoId = productoId
            };

            await _inventarioService.CrearAsync(inventario);

            TempData["Mensaje"] = "Inventario registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // EDITAR INVENTARIO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(
            int inventarioId,
            DateOnly? fechaIngreso,
            int? cantidad,
            string? ubicacion,
            string? estado,
            int? productoId)
        {
            if (!DatosValidos(
                fechaIngreso, cantidad, ubicacion, estado, productoId))
            {
                TempData["Error"] = "Complete correctamente todos los campos.";
                return RedirectToAction(nameof(Index));
            }

            var inventario = await _inventarioService
                .ObtenerPorIdAsync(inventarioId);

            if (inventario == null)
            {
                return NotFound();
            }

            if (await _productoService.ObtenerPorIdAsync(
                    productoId!.Value) == null)
            {
                TempData["Error"] = "El producto seleccionado no existe.";
                return RedirectToAction(nameof(Index));
            }

            inventario.FechaIngreso = fechaIngreso;
            inventario.Cantidad = cantidad;
            inventario.Ubicacion = ubicacion!.Trim();
            inventario.Estado = estado!.Trim();
            inventario.ProductoId = productoId;

            await _inventarioService.EditarAsync(inventario);

            TempData["Mensaje"] = "Inventario actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // VALIDAR DATOS
        private static bool DatosValidos(
            DateOnly? fechaIngreso,
            int? cantidad,
            string? ubicacion,
            string? estado,
            int? productoId)
        {
            return fechaIngreso.HasValue
                && cantidad.GetValueOrDefault() > 0
                && !string.IsNullOrWhiteSpace(ubicacion)
                && ubicacion.Length <= 100
                && !string.IsNullOrWhiteSpace(estado)
                && estado.Length <= 50
                && productoId.GetValueOrDefault() > 0;
        }
    }
}