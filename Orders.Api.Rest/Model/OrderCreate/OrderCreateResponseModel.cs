using System;

namespace Orders.Api.Rest.Model.OrderCreate
{
    public class OrderCreateResponseModel: ResponseModel
    {
        public Guid OrderId { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
