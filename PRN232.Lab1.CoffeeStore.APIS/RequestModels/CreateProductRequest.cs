using System.ComponentModel.DataAnnotations;

namespace PRN232.Lab1.CoffeeStore.APIS.RequestModels
{
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm không được quá 100 ký tự")]
        public string Name { get; set; } = null!;

        [Range(1, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Price { get; set; }

        [StringLength(255, ErrorMessage = "Mô tả không được quá 255 ký tự")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "CategoryId là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "CategoryId phải lớn hơn 0")]
        public int CategoryId { get; set; }
    }
}
