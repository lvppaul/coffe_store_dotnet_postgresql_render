namespace PRN232.Lab1.CoffeeStore.APIS.ResponseModels
{
    public class MenuProductResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
