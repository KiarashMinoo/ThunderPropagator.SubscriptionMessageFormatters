using ThunderPropagator.Application.PushModules.Formatters;
using ThunderPropagator.BuildingBlocks.Application.Serializations;

namespace ThunderPropagator.SubscriptionMessageFormatters.NetJson;

public sealed class NetJsonSubscriptionMessageFormatter(
    IFormatSerializerRegistry registry
) : StructuredSubscriptionMessageFormatter(registry)
{
    public override SerializerType SerializerType => NetJsonFormatSerializer.NetJson;
    public override string ContentType => NetJsonFormatSerializer.NetJsonMediaType;
}
