using DDDTemplate.Service.Extensions;
using DDDTemplate.Service.Oders.Commands;
using DDDTemplate.Service.Oders.Queries.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DDDTemplate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IMediator _mediator;

        public OrdersController(
            ILogger<OrdersController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{orderId:int}")]
        public async Task<ActionResult> GetOrderAsync(int orderId)
        {
            try
            {
                var order = await _mediator.Send(new GetOrderByIdQuery { Id = orderId });

                return Ok(order);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} ({@Command})",
                command.GetGenericTypeName(),
                command);

            await _mediator.Send(command);

            return Ok();
        }
    }
}
