using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ThunderPropagator.Application.PushModules.Formatters;
using ThunderPropagator.FormatSerializers.Xml;

namespace ThunderPropagator.SubscriptionMessageFormatters.Xml
{
    /// <summary>
    /// Extension methods for registering ThunderPropagator BuildingBlocks services.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddXmlSubscriptionMessageFormatter(this IServiceCollection services)
        {
            Guard.Against.Null(services);

            services.TryAddEnumerable(ServiceDescriptor.Singleton<ISubscriptionMessageFormatter, XmlSubscriptionMessageFormatter>());

            return services;
        }
    }
}
