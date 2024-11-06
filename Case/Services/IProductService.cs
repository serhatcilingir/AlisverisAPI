using System.Collections.Generic;
using System.Threading.Tasks;
using Case.Dtos;

namespace Case.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProductById(int id);
        Task<bool> CreateProduct(ProductDto product);
        Task<bool> UpdateProduct(ProductDto product);
        Task<bool> DeleteProduct(int id);
    }
}
