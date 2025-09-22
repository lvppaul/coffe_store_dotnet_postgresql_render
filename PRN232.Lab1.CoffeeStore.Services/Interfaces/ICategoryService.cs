using PRN232.Lab1.CoffeeStore.Services.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab1.CoffeeStore.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryModel>> GetAllAsync();
        Task<CategoryModel?> GetByIdAsync(int id);
    }
}
