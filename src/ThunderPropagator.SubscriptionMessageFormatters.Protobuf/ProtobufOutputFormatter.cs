using ThunderPropagator.FormatSerializers.Protobuf;
using ThunderPropagator.Infrastructure.Formatters;

namespace ThunderPropagator.SubscriptionMessageFormatters.Protobuf;

public sealed class ProtobufOutputFormatter() : FormatOutputFormatter(ProtobufFormatSerializer.Protobuf, ProtobufFormatSerializer.ProtobufMediaType);
