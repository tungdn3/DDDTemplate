﻿using DDDTemplate.Domain.AggregatesModel.OrderAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTemplate.Service.Oders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler
        : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        //private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;
        //private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        // Using DI to inject infrastructure persistence Repositories
        public CreateOrderCommandHandler(
            IMediator mediator,
            //IOrderingIntegrationEventService orderingIntegrationEventService,
            IOrderRepository orderRepository,
            //IIdentityService identityService,
            ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            //_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            //_orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateOrderCommand message, CancellationToken cancellationToken)
        {
            // Add Integration event to clean the basket
            //var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(message.UserId);
            //await _orderingIntegrationEventService.AddAndSaveEventAsync(orderStartedIntegrationEvent);

            var address = new Address(message.Street, message.City, message.State, message.Country, message.ZipCode);
            var order = new Order(message.UserId, message.UserName, address, message.CardTypeId, message.CardNumber, message.CardSecurityNumber, message.CardHolderName, message.CardExpiration);

            foreach (var item in message.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl, item.Units);
            }

            _logger.LogInformation("----- Creating Order - Order: {@Order}", order);

            _orderRepository.Add(order);

            return await _orderRepository.UnitOfWork.SaveChanges(cancellationToken);
        }
    }


    // Use for Idempotency in Command process
    //public class CreateOrderIdentifiedCommandHandler : IdentifiedCommandHandler<CreateOrderCommand, bool>
    //{
    //    public CreateOrderIdentifiedCommandHandler(
    //        IMediator mediator,
    //        IRequestManager requestManager,
    //        ILogger<IdentifiedCommandHandler<CreateOrderCommand, bool>> logger)
    //        : base(mediator, requestManager, logger)
    //    {
    //    }

    //    protected override bool CreateResultForDuplicateRequest()
    //    {
    //        return true;                // Ignore duplicate requests for creating order.
    //    }
    //}
}
