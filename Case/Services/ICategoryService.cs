using System.Collections.Generic;
using System.Threading.Tasks;
using Case.Dtos;

namespace Case.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategories();
        Task<CategoryDto> GetCategoryById(int id);
        Task<bool> CreateCategory(CategoryDto categoryDto);
        Task<bool> UpdateCategory(CategoryDto categoryDto);
        Task<bool> DeleteCategory(int id);
    }
}
