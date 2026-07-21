using ThunderPropagator.Application.PushModules.Formatters;
using ThunderPropagator.BuildingBlocks.Application.Serializations;
using ThunderPropagator.FormatSerializers.Yaml;

namespace ThunderPropagator.SubscriptionMessageFormatters.Yaml;

public sealed class YamlSubscriptionMessageFormatter(
    IFormatSerializerRegistry registry
) : StructuredSubscriptionMessageFormatter(registry)
{
    public override SerializerType SerializerType => YamlFormatSerializer.Yaml;
    public override string ContentType => YamlFormatSerializer.YamlMediaType;
}
