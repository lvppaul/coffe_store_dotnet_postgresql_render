using PRN232.Lab1.CoffeeStore.Repositories.Context;
using PRN232.Lab1.CoffeeStore.Repositories.Interfaces;
using PRN232.Lab1.CoffeeStore.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PRN232.Lab1.CoffeeStore.Repositories.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CoffeeStoreContext _context;

        public ProductRepository(CoffeeStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
            => await _context.Products.Include(p => p.Category).ToListAsync();

        public async Task<Product?> GetByIdAsync(int id)
            => await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> IsProductInMenu(int productId)
           => await _context.ProductInMenus.AnyAsync(pim => pim.ProductId == productId);
    }
}
