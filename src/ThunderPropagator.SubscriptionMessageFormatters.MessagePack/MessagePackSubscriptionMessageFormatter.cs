using ThunderPropagator.Application.PushModules.Formatters;
using ThunderPropagator.BuildingBlocks.Application.Serializations;
using ThunderPropagator.FormatSerializers.MessagePack;

namespace ThunderPropagator.SubscriptionMessageFormatters.MessagePack;

public sealed class MessagePackSubscriptionMessageFormatter(
    IFormatSerializerRegistry registry
) : StructuredSubscriptionMessageFormatter(registry)
{
    public override SerializerType SerializerType => MessagePackFormatSerializer.MessagePack;
    public override string ContentType => MessagePackFormatSerializer.MessagePackMediaType;
}
