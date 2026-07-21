using System.Collections.Generic;
using ThunderPropagator.BuildingBlocks.Application.Serializations;
using ThunderPropagator.BuildingBlocks.Application.Serializations.Json;
using ThunderPropagator.FormatSerializers.MessagePack;
using ThunderPropagator.SubscriptionMessageFormatters.MessagePack;
using ThunderPropagator.SubscriptionMessageFormatters.NetJson;
using ThunderPropagator.FormatSerializers.Protobuf;
using ThunderPropagator.FormatSerializers.Xml;
using ThunderPropagator.FormatSerializers.Yaml;
using ThunderPropagator.SubscriptionMessageFormatters.Xml;
using ThunderPropagator.SubscriptionMessageFormatters.Yaml;
using Xunit;

namespace ThunderPropagator.UnitTests.Serializations
{
    public class FormatSerializerRegistryTests
    {
        private static FormatSerializerRegistry BuildRegistry()
        {
            IFormatSerializer[] serializers =
            [
                new JsonFormatSerializer(),
                new NJsonFormatSerializer(),
                new NetJsonFormatSerializer(),
                new ProtobufFormatSerializer(),
                new MessagePackFormatSerializer(),
                new XmlFormatSerializer(),
                new YamlFormatSerializer(),
            ];

            IFormatDeserializer[] deserializers =
            [
                new JsonFormatSerializer(),
                new NJsonFormatSerializer(),
                new NetJsonFormatSerializer(),
                new ProtobufFormatSerializer(),
                new MessagePackFormatSerializer(),
                new XmlFormatSerializer(),
                new YamlFormatSerializer(),
            ];

            return new FormatSerializerRegistry(serializers, deserializers);
        }

        // SerializerType instances must be the exact singletons each serializer registers with
        // (the type has no value equality override), so theory data is supplied via MemberData
        // rather than InlineData, which would require re-converting from a plain int each time.
        public static IEnumerable<object[]> AllSerializerTypes()
        {
            yield return [JsonFormatSerializer.Json];
            yield return [NJsonFormatSerializer.NJson];
            yield return [NetJsonFormatSerializer.NetJson];
            yield return [ProtobufFormatSerializer.Protobuf];
            yield return [MessagePackFormatSerializer.MessagePack];
            yield return [XmlFormatSerializer.Xml];
            yield return [YamlFormatSerializer.Yaml];
        }

        [Theory]
        [MemberData(nameof(AllSerializerTypes))]
        public void GetSerializer_ByType_ShouldReturnMatchingSerializer(SerializerType type)
        {
            var registry = BuildRegistry();
            var serializer = registry.GetSerializer(type);
            Assert.NotNull(serializer);
            Assert.Equal(type.Value, serializer.SerializerType.Value);
        }

        [Theory]
        [MemberData(nameof(AllSerializerTypes))]
        public void GetDeserializer_ByType_ShouldReturnMatchingDeserializer(SerializerType type)
        {
            var registry = BuildRegistry();
            var deserializer = registry.GetDeserializer(type);
            Assert.NotNull(deserializer);
            Assert.Equal(type.Value, deserializer.SerializerType.Value);
        }

        [Theory]
        [InlineData("application/json")]
        [InlineData(ProtobufFormatSerializer.ProtobufMediaType)]
        [InlineData(MessagePackFormatSerializer.MessagePackMediaType)]
        [InlineData(XmlFormatSerializer.XmlMediaType)]
        [InlineData(YamlFormatSerializer.YamlMediaType)]
        public void GetSerializer_ByMediaType_ShouldReturnSerializer(string mediaType)
        {
            var registry = BuildRegistry();
            var serializer = registry.GetSerializer(mediaType);
            Assert.NotNull(serializer);
            Assert.Equal(mediaType, serializer.MediaType, StringComparer.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData("application/json")]
        [InlineData(ProtobufFormatSerializer.ProtobufMediaType)]
        [InlineData(MessagePackFormatSerializer.MessagePackMediaType)]
        [InlineData(XmlFormatSerializer.XmlMediaType)]
        [InlineData(YamlFormatSerializer.YamlMediaType)]
        public void GetDeserializer_ByMediaType_ShouldReturnDeserializer(string mediaType)
        {
            var registry = BuildRegistry();
            var deserializer = registry.GetDeserializer(mediaType);
            Assert.NotNull(deserializer);
            Assert.Equal(mediaType, deserializer.MediaType, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public void GetSerializer_ByJsonMediaType_ShouldReturnJsonFormatSerializer()
        {
            var registry = BuildRegistry();
            var serializer = registry.GetSerializer("application/json");
            Assert.IsType<JsonFormatSerializer>(serializer);
        }

        [Fact]
        public void GetDeserializer_ByJsonMediaType_ShouldReturnJsonFormatSerializer()
        {
            var registry = BuildRegistry();
            var deserializer = registry.GetDeserializer("application/json");
            Assert.IsType<JsonFormatSerializer>(deserializer);
        }

        [Fact]
        public void GetSerializer_UnknownType_ShouldThrow()
        {
            var registry = BuildRegistry();
            Assert.Throws<InvalidOperationException>(() => registry.GetSerializer((SerializerType)99));
        }

        [Fact]
        public void GetDeserializer_UnknownType_ShouldThrow()
        {
            var registry = BuildRegistry();
            Assert.Throws<InvalidOperationException>(() => registry.GetDeserializer((SerializerType)99));
        }

        [Fact]
        public void GetSerializer_UnknownMediaType_ShouldThrow()
        {
            var registry = BuildRegistry();
            Assert.Throws<InvalidOperationException>(() => registry.GetSerializer("application/unknown"));
        }

        [Fact]
        public void GetDeserializer_UnknownMediaType_ShouldThrow()
        {
            var registry = BuildRegistry();
            Assert.Throws<InvalidOperationException>(() => registry.GetDeserializer("application/unknown"));
        }

        [Fact]
        public void Constructor_NullSerializers_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FormatSerializerRegistry(null!, Array.Empty<IFormatDeserializer>()));
        }

        [Fact]
        public void Constructor_NullDeserializers_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FormatSerializerRegistry(Array.Empty<IFormatSerializer>(), null!));
        }
    }
}
