using DSWI_T1_01.Models;
using DSWI_T1_01.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSWI_T1_01.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // LISTAR PRODUCTOS
        public async Task<IActionResult> Index()
        {
            var productos = await _productoService.ObtenerTodosAsync();
            return View(productos);
        }

        // CREAR PRODUCTO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(string nombre, string categoria)
        {
            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(categoria))
            {
                TempData["Error"] = "Complete todos los campos.";
                return RedirectToAction(nameof(Index));
            }

            var producto = new Producto
            {
                Nombre = nombre.Trim(),
                Categoria = categoria.Trim()
            };

            await _productoService.CrearAsync(producto);

            TempData["Mensaje"] = "Producto registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // EDITAR PRODUCTO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(
            int productoId, string nombre, string categoria)
        {
            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(categoria))
            {
                TempData["Error"] = "Complete todos los campos.";
                return RedirectToAction(nameof(Index));
            }

            var producto = await _productoService.ObtenerPorIdAsync(productoId);

            if (producto == null)
            {
                return NotFound();
            }

            producto.Nombre = nombre.Trim();
            producto.Categoria = categoria.Trim();

            await _productoService.EditarAsync(producto);

            TempData["Mensaje"] = "Producto actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}