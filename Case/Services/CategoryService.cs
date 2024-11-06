using System.Collections.Generic;
using System.Threading.Tasks;
using Case.Models;
using Case.Repositories;
using Case.Services;
using Case.Dtos;

namespace Case.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryDtos = new List<CategoryDto>();

            foreach (var category in categories)
            {
                categoryDtos.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return categoryDtos;
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return null;
            }

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<bool> CreateCategory(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name
            };

            await _categoryRepository.AddAsync(category);
            var changes = await _categoryRepository.SaveChangesAsync(); // changes değişkenine atayın
            return changes > 0;
        }

        public async Task<bool> UpdateCategory(CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDto.Id);
            if (category == null)
            {
                return false;
            }

            category.Name = categoryDto.Name;
            _categoryRepository.Update(category);
            var changes = await _categoryRepository.SaveChangesAsync(); // changes değişkenine atayın
            return changes > 0;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }

            _categoryRepository.Delete(category);
            var changes = await _categoryRepository.SaveChangesAsync(); // changes değişkenine atayın
            return changes > 0;
        }
    }
}
