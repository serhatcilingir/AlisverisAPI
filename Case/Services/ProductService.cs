using System.Collections.Generic;
using System.Threading.Tasks;
using Case.Dtos;
using Case.Data;
using Microsoft.EntityFrameworkCore;
using Case.Models;
using Case.Services;

namespace Case.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            var productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                productDtos.Add(new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    ImageUrl = product.ImageUrl,
                    CategoryId = product.CategoryId
                });
            }

            return productDtos;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId
            };
        }

        public async Task<bool> CreateProduct(ProductDto productDto)
        {
            var newProduct = new Product
            {
                Name = productDto.Name,
                ImageUrl = productDto.ImageUrl,
                CategoryId = productDto.CategoryId
            };

            _context.Products.Add(newProduct);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProduct(ProductDto productDto)
        {
            var product = await _context.Products.FindAsync(productDto.Id);
            if (product == null)
                return false;

            product.Name = productDto.Name;
            product.ImageUrl = productDto.ImageUrl;
            product.CategoryId = productDto.CategoryId;

            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
