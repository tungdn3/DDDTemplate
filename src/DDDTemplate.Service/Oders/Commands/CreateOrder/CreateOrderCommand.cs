﻿using MediatR;
using System;
using System.Collections.Generic;

namespace DDDTemplate.Service.Oders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public string CardNumber { get; set; }

        public string CardHolderName { get; set; }

        public DateTime CardExpiration { get; set; }

        public string CardSecurityNumber { get; set; }

        public int CardTypeId { get; set; }

        public IEnumerable<OrderItemDTO> OrderItems { get; set; }
    }
}
