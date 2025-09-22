using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab1.CoffeeStore.Services.BusinessModels
{
    public class MenuModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<MenuProductModel> Products { get; set; } = new();
    }
}
