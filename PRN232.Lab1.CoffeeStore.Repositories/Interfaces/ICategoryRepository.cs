using PRN232.Lab1.CoffeeStore.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab1.CoffeeStore.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
