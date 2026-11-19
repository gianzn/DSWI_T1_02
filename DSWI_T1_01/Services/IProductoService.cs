using DSWI_T1_01.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSWI_T1_01.Services
{
    public interface IProductoService
    {
        Task<List<Producto>> ObtenerTodosAsync();

        Task<Producto?> ObtenerPorIdAsync(int id);
    }
}
