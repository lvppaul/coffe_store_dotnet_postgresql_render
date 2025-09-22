using System.Collections.Generic;
using System.Threading.Tasks;
using PRN232.Lab1.CoffeeStore.Repositories.Models;

namespace PRN232.Lab1.CoffeeStore.Repositories.Interfaces;

public interface IMenuRepository
{
    Task<IEnumerable<Menu>> GetAllAsync();
    Task<Menu?> GetByIdAsync(int id);
    Task AddAsync(Menu menu);
    Task UpdateAsync(Menu menu);
    Task DeleteAsync(int id);
}
