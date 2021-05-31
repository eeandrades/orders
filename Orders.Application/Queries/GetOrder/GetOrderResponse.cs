namespace Orders.Application.Queries.GetOrder
{
    public class GetOrderResponse: Lib.Application.ResponseBase
    {
        public Domain.Order Order { get; set; }
    }
}
