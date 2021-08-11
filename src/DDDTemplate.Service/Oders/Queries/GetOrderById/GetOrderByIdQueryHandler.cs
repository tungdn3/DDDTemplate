using Dapper;
using DDDTemplate.Service.Options;
using MediatR;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTemplate.Service.Oders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderViewModel>
    {
        private readonly string _connectionString;

        public GetOrderByIdQueryHandler(IOptionsMonitor<ConnectionStringOptions> optionsMonitor)
        {
            _connectionString = optionsMonitor.CurrentValue.ConnectionString;
        }

        public async Task<OrderViewModel> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(
                   @"
select
    o.[Id] as ordernumber,
    o.Description as description,
    o.Address_City as city,
    o.Address_Country as country,
    o.Address_State as state,
    o.Address_Street as street,
    o.Address_ZipCode as zipcode,
    o.OrderStatus as status, 
    oi.ProductName as productname, oi.Units as units, oi.UnitPrice as unitprice, oi.PictureUri as pictureuri
FROM Orders o
LEFT JOIN Orderitems oi ON o.Id = oi.orderid 
WHERE o.Id=@id"
                        , new { request.Id }
                    );

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return MapOrderItems(result);
            }
        }

        private static OrderViewModel MapOrderItems(dynamic result)
        {
            var order = new OrderViewModel
            {
                Ordernumber = result[0].ordernumber,
                Status = result[0].status,
                Description = result[0].description,
                Street = result[0].street,
                City = result[0].city,
                Zipcode = result[0].zipcode,
                Country = result[0].country,
                OrderItems = new List<OrderItemViewModel>(),
                Total = 0
            };

            foreach (dynamic item in result)
            {
                var orderitem = new OrderItemViewModel
                {
                    Productname = item.productname,
                    Units = item.units,
                    Unitprice = (double)item.unitprice,
                    PictureUri = item.pictureuri
                };

                order.Total += item.units * item.unitprice;
                order.OrderItems.Add(orderitem);
            }

            return order;
        }
    }
}
