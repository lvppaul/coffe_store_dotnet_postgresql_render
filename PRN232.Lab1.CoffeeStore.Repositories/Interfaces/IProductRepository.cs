using System.Collections.Generic;
using System.Threading.Tasks;
using PRN232.Lab1.CoffeeStore.Repositories.Models;

namespace PRN232.Lab1.CoffeeStore.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);

    Task<bool> IsProductInMenu(int productId);
}
