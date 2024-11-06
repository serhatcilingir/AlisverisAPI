using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Case.Dtos;
using Case.Services;

namespace Case.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            return category != null ? Ok(category) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDto categoryDto)
        {
            var result = await _categoryService.CreateCategory(categoryDto);
            return result ? Ok("Kategori oluşturuldu") : BadRequest("Kategori oluşturulamadı");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest("ID uyuşmazlığı");
            }
            var result = await _categoryService.UpdateCategory(categoryDto);
            return result ? Ok("Kategori güncellendi") : NotFound("Kategori bulunamadı");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            return result ? Ok("Kategori silindi") : NotFound("Kategori bulunamadı");
        }
    }
}
