using ThunderPropagator.Application.PushModules.Formatters;
using ThunderPropagator.BuildingBlocks.Application.Serializations;
using ThunderPropagator.FormatSerializers.Toon;

namespace ThunderPropagator.SubscriptionMessageFormatters.Toon;

public sealed class ToonSubscriptionMessageFormatter(
    IFormatSerializerRegistry registry
) : StructuredSubscriptionMessageFormatter(registry)
{
    public override SerializerType SerializerType => ToonFormatSerializer.Toon;
    public override string ContentType => ToonFormatSerializer.ToonMediaType;
}
