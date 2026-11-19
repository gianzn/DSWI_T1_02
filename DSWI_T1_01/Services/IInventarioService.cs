using DSWI_T1_01.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSWI_T1_01.Services
{
    public interface IInventarioService
    {
        Task<List<Inventario>> ObtenerTodosAsync();

        Task<Inventario?> ObtenerPorIdAsync(int id);

        Task CrearAsync(Inventario inventario);

        Task EditarAsync(Inventario inventario);
    }
}
