using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Case.Dtos;
using Case.Services;

namespace Case.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto product)
        {
            var result = await _productService.CreateProduct(product);
            return result ? Ok("Ürün oluşturuldu") : BadRequest("Ürün oluşturulamadı");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto product)
        {
            if (id != product.Id)
            {
                return BadRequest("ID uyuşmazlığı");
            }
            var result = await _productService.UpdateProduct(product);
            return result ? Ok("Ürün güncellendi") : NotFound("Ürün bulunamadı");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            return result ? Ok("Ürün silindi") : NotFound("Ürün bulunamadı");
        }
    }
}
