using System.Collections.Generic;
using System.Threading.Tasks;
using Case.Data;
using Case.Models;
using Microsoft.EntityFrameworkCore;

namespace Case.Repositories
{
    public class ShoppingListRepository : IRepository<ShoppingList>
    {
        private readonly AppDbContext _context;

        public ShoppingListRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ShoppingList> GetByIdAsync(int id)
        {
            return await _context.ShoppingLists
                .Include(sl => sl.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(sl => sl.Id == id);
        }

        public async Task<IEnumerable<ShoppingList>> GetAllAsync()
        {
            return await _context.ShoppingLists
                .Include(sl => sl.Items)
                .ThenInclude(i => i.Product)
                .ToListAsync();
        }

        public async Task AddAsync(ShoppingList entity)
        {
            await _context.ShoppingLists.AddAsync(entity);
        }

        public void Update(ShoppingList entity)
        {
            _context.ShoppingLists.Update(entity);
        }

        public void Delete(ShoppingList entity)
        {
            _context.ShoppingLists.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
