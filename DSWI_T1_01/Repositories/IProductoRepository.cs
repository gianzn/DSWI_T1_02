using DSWI_T1_01.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSWI_T1_01.Repositories
{
    public interface IProductoRepository
    {
        Task<List<Producto>> ObtenerTodosAsync();

        Task<Producto?> ObtenerPorIdAsync(int id);

        Task CrearAsync(Producto producto);

        Task EditarAsync(Producto producto);
    }
}