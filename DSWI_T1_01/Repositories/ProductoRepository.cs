using DSWI_T1_01.Data;
using DSWI_T1_01.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSWI_T1_01.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly InventarioContext _context;

        public ProductoRepository(InventarioContext context)
        {
            _context = context;
        }

        // Obtener todos los productos
        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos
                .FromSqlRaw("EXEC dbo.usp_Productos_Listar")
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener un producto por ID
        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            var productos = await _context.Productos
                .FromSqlInterpolated(
                    $"EXEC dbo.usp_Productos_ObtenerPorId @ProductoID={id}")
                .AsNoTracking()
                .ToListAsync();

            return productos.FirstOrDefault();
        }

        // Registrar un producto
        public async Task CrearAsync(Producto producto)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC dbo.usp_Productos_Crear @Nombre={producto.Nombre}, @Categoria={producto.Categoria}");
        }

        // Modificar un producto
        public async Task EditarAsync(Producto producto)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC dbo.usp_Productos_Editar @ProductoID={producto.ProductoId}, @Nombre={producto.Nombre}, @Categoria={producto.Categoria}");
        }
    }
}