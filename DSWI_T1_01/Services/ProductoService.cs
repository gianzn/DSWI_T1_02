using DSWI_T1_01.Models;
using DSWI_T1_01.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSWI_T1_01.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        // Listar todos los productos
        public Task<List<Producto>> ObtenerTodosAsync()
        {
            return _repository.ObtenerTodosAsync();
        }

        // Buscar producto por ID
        public Task<Producto?> ObtenerPorIdAsync(int id)
        {
            return _repository.ObtenerPorIdAsync(id);
        }

        // Registrar un producto
        public Task CrearAsync(Producto producto)
        {
            return _repository.CrearAsync(producto);
        }

        // Editar un producto
        public Task EditarAsync(Producto producto)
        {
            return _repository.EditarAsync(producto);
        }
    }
}