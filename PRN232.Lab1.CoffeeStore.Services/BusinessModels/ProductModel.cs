using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab1.CoffeeStore.Services.BusinessModels
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
