using Microsoft.EntityFrameworkCore;
using PRN232.Lab1.CoffeeStore.Repositories.Context;
using PRN232.Lab1.CoffeeStore.Repositories.Interfaces;
using PRN232.Lab1.CoffeeStore.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab1.CoffeeStore.Repositories.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CoffeeStoreContext _context;

        public CategoryRepository(CoffeeStoreContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
