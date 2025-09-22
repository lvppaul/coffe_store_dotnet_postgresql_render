using PRN232.Lab1.CoffeeStore.Repositories.Models;
using PRN232.Lab1.CoffeeStore.Services.BusinessModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN232.Lab1.CoffeeStore.Services.Interfaces;

public interface IMenuService
{
    Task<IEnumerable<MenuModel>> GetAllAsync();
    Task<MenuModel?> GetByIdAsync(int id);
    Task<MenuModel> CreateAsync(MenuModel model);
    Task<MenuModel> UpdateAsync(MenuModel model);
    Task DeleteAsync(int id);
}
