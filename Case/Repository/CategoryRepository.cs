using System.Collections.Generic;
using System.Threading.Tasks;
using Case.Data;
using Case.Models;
using Microsoft.EntityFrameworkCore;

namespace Case.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task AddAsync(Category entity)
        {
            await _context.Categories.AddAsync(entity);
        }

        public void Update(Category entity)
        {
            _context.Categories.Update(entity);
        }

        public void Delete(Category entity)
        {
            _context.Categories.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
