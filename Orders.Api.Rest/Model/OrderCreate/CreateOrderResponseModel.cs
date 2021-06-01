using System;

namespace Orders.Api.Rest.Model.CreateOrder
{
    public class CreateOrderResponseModel: ResponseModel
    {
        public Guid OrderId { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
