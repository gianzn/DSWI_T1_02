using DSWI_T1_01.Models;
using DSWI_T1_01.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSWI_T1_01.Services
{
    public class InventarioService : IInventarioService
    {
        private readonly IInventarioRepository _repository;

        public InventarioService(IInventarioRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Inventario>> ObtenerTodosAsync()
        {
            return _repository.ObtenerTodosAsync();
        }

        public Task<Inventario?> ObtenerPorIdAsync(int id)
        {
            return _repository.ObtenerPorIdAsync(id);
        }

        public Task CrearAsync(Inventario inventario)
        {
            return _repository.CrearAsync(inventario);
        }

        public Task EditarAsync(Inventario inventario)
        {
            return _repository.EditarAsync(inventario);
        }
    }
}