using PRN232.Lab1.CoffeeStore.Repositories.Models;
using PRN232.Lab1.CoffeeStore.Services.BusinessModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN232.Lab1.CoffeeStore.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> GetAllAsync();
    Task<ProductModel?> GetByIdAsync(int id);
    Task<ProductModel> CreateAsync(ProductModel model);
    Task<ProductModel> UpdateAsync(ProductModel model);
    Task DeleteAsync(int id);
}
