namespace PRN232.Lab1.CoffeeStore.APIS.ResponseModels
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
