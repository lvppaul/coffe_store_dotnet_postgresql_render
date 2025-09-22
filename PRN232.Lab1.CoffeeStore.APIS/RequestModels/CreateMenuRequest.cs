using System.ComponentModel.DataAnnotations;

namespace PRN232.Lab1.CoffeeStore.APIS.RequestModels
{
    public class CreateMenuRequest
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required, MinLength(1, ErrorMessage = "Menu cần ít nhất 1 sản phẩm")]
        public List<CreateMenuProductRequest> Products { get; set; } = new();
    }
}
