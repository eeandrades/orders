﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Api.Rest.Model.GetOrder
{
    public class OrderResponseModel: ResponseModel
    {
        public OrderModel Order { get; init; }
    }
}
