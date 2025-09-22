using System.ComponentModel.DataAnnotations;

namespace PRN232.Lab1.CoffeeStore.APIS.RequestModels
{
    public class CreateMenuProductRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }
    }
}
