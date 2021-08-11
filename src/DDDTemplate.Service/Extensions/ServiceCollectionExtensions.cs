using DDDTemplate.Service.Behaviors;
using DDDTemplate.Service.Oders.Commands;
using DDDTemplate.Service.Options;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DDDTemplate.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services, Action<ConnectionStringOptions> connectionStringOptions)
        {
            services.AddMediatR(typeof(CreateOrderCommandHandler));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            services.AddValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();

            services.Configure(connectionStringOptions);

            return services;
        }
    }
}
