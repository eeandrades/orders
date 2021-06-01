namespace Orders.Api.Rest.Model
{
    public class OrderItemModel
    {
        public ProductModel Product { get; set; }
        public int Amount { get; set; }
    }
}
