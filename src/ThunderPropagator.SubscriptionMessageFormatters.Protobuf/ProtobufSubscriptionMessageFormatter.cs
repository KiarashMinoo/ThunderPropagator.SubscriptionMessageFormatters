using ThunderPropagator.Application.PushModules.Formatters;
using ThunderPropagator.BuildingBlocks.Application.Serializations;
using ThunderPropagator.FormatSerializers.Protobuf;

namespace ThunderPropagator.SubscriptionMessageFormatters.Protobuf;

public sealed class ProtobufSubscriptionMessageFormatter(
    IFormatSerializerRegistry registry
) : StructuredSubscriptionMessageFormatter(registry)
{
    public override SerializerType SerializerType => ProtobufFormatSerializer.Protobuf;
    public override string ContentType => ProtobufFormatSerializer.ProtobufMediaType;
}
