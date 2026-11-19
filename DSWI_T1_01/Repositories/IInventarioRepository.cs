using DSWI_T1_01.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSWI_T1_01.Repositories
{
    public interface IInventarioRepository
    {
        Task<List<Inventario>> ObtenerTodosAsync();

        Task<Inventario?> ObtenerPorIdAsync(int id);
    }
}
