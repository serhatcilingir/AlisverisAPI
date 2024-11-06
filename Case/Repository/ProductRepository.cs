using System.Collections.Generic;
using System.Threading.Tasks;
using Case.Models;
using Case.Repositories;
using Case.Data;
using Microsoft.EntityFrameworkCore;

namespace Case.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly AppDbContext _context;

        // Birincil kurucu kullanarak _context değişkenini ayarlayın
        public ProductRepository(AppDbContext context) => _context = context;

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task AddAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
        }

        public void Update(Product entity)
        {
            _context.Products.Update(entity);
        }

        public void Delete(Product entity)
        {
            _context.Products.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
