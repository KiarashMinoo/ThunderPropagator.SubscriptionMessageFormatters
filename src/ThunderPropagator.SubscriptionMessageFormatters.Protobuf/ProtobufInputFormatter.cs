using ThunderPropagator.FormatSerializers.Protobuf;
using ThunderPropagator.Infrastructure.Formatters;

namespace ThunderPropagator.SubscriptionMessageFormatters.Protobuf;

public sealed class ProtobufInputFormatter() : FormatInputFormatter(ProtobufFormatSerializer.Protobuf, ProtobufFormatSerializer.ProtobufMediaType);
