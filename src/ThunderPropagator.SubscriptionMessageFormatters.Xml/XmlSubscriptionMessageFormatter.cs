using ThunderPropagator.Application.PushModules.Formatters;
using ThunderPropagator.BuildingBlocks.Application.Serializations;
using ThunderPropagator.FormatSerializers.Xml;

namespace ThunderPropagator.SubscriptionMessageFormatters.Xml
{
    public sealed class XmlSubscriptionMessageFormatter(IFormatSerializerRegistry registry) : StructuredSubscriptionMessageFormatter(registry)
    {
        public override SerializerType SerializerType => XmlFormatSerializer.Xml;
        public override string ContentType => XmlFormatSerializer.XmlMediaType;
    }
}
