namespace PRN232.Lab1.CoffeeStore.APIS.ResponseModels
{
    public class MenuResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public List<MenuProductResponse> Products { get; set; } = new();
    }
}
