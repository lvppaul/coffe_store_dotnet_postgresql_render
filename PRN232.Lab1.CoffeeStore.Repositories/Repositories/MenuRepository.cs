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
    public class MenuRepository : IMenuRepository
    {
        private readonly CoffeeStoreContext _context;

        public MenuRepository(CoffeeStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu>> GetAllAsync()
            => await _context.Menus
                .Include(m => m.ProductInMenus)
                .ThenInclude(pim => pim.Product)
                .ToListAsync();

        public async Task<Menu?> GetByIdAsync(int id)
            => await _context.Menus
                .Include(m => m.ProductInMenus)
                .ThenInclude(pim => pim.Product)
                .FirstOrDefaultAsync(m => m.MenuId == id);

        public async Task AddAsync(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Menu menu)
        {
            _context.Menus.Update(menu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
            }
        }
    }
}
