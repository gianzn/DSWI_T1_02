using DSWI_T1_01.Data;
using DSWI_T1_01.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSWI_T1_01.Repositories
{
    public class InventarioRepository : IInventarioRepository
    {
        private readonly InventarioContext _context;

        public InventarioRepository(InventarioContext context)
        {
            _context = context;
        }

        // Obtener todos los inventarios
        public async Task<List<Inventario>> ObtenerTodosAsync()
        {
            return await _context.Inventarios
                .FromSqlRaw("EXEC dbo.usp_Inventario_Listar")
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener un inventario por ID
        public async Task<Inventario?> ObtenerPorIdAsync(int id)
        {
            var inventarios = await _context.Inventarios
                .FromSqlInterpolated(
                    $"EXEC dbo.usp_Inventario_ObtenerPorId @InventarioID={id}")
                .AsNoTracking()
                .ToListAsync();

            return inventarios.FirstOrDefault();
        }

        // Registrar un inventario
        public async Task CrearAsync(Inventario inventario)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC dbo.usp_Inventario_Crear @FechaIngreso={inventario.FechaIngreso}, @Cantidad={inventario.Cantidad}, @Ubicacion={inventario.Ubicacion}, @Estado={inventario.Estado}, @ProductoID={inventario.ProductoId}");
        }

        // Editar un inventario
        public async Task EditarAsync(Inventario inventario)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC dbo.usp_Inventario_Editar @InventarioID={inventario.InventarioId}, @FechaIngreso={inventario.FechaIngreso}, @Cantidad={inventario.Cantidad}, @Ubicacion={inventario.Ubicacion}, @Estado={inventario.Estado}, @ProductoID={inventario.ProductoId}");
        }
    }
}