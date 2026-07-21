using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ThunderPropagator.Application.PushModules.Formatters;

namespace ThunderPropagator.SubscriptionMessageFormatters.NetJson
{
    /// <summary>
    /// Extension methods for registering ThunderPropagator BuildingBlocks services.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddNetJsonSubscriptionMessageFormatter(this IServiceCollection services)
        {
            Guard.Against.Null(services);

            services.TryAddEnumerable(ServiceDescriptor.Singleton<ISubscriptionMessageFormatter, NetJsonSubscriptionMessageFormatter>());

            return services;
        }
    }
}
