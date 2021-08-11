using System;
using System.Collections.Generic;

namespace DDDTemplate.Service.Oders.Queries.GetOrderById
{
    public record OrderItemViewModel
    {
        public string Productname { get; init; }
        public int Units { get; init; }
        public double Unitprice { get; init; }
        public string PictureUri { get; init; }
    }

    public record OrderViewModel
    {
        public int Ordernumber { get; init; }
        public DateTime Date { get; init; }
        public int Status { get; init; }
        public string Description { get; init; }
        public string Street { get; init; }
        public string City { get; init; }
        public string Zipcode { get; init; }
        public string Country { get; init; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public decimal Total { get; set; }
    }
}
