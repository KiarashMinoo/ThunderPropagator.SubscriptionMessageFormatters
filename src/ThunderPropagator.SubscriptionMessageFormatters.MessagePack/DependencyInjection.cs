using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ThunderPropagator.Application.PushModules.Formatters;

namespace ThunderPropagator.SubscriptionMessageFormatters.MessagePack
{
    /// <summary>
    /// Extension methods for registering ThunderPropagator BuildingBlocks services.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddMessagePackSubscriptionMessageFormatter(this IServiceCollection services)
        {
            Guard.Against.Null(services);

            services.TryAddEnumerable(ServiceDescriptor.Singleton<ISubscriptionMessageFormatter, MessagePackSubscriptionMessageFormatter>());
            services.Configure<MvcOptions>(options =>
            {
                options.InputFormatters.Add(new MessagePackInputFormatter());
                options.OutputFormatters.Add(new MessagePackOutputFormatter());
            });

            return services;
        }
    }
}
