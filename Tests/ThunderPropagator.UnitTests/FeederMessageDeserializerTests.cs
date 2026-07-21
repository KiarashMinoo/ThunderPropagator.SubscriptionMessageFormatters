using JetBrains.Annotations;
using NSubstitute;
using ThunderPropagator.Application.Feeders;
using ThunderPropagator.BuildingBlocks.Application;
using ThunderPropagator.BuildingBlocks.Application.Serializations;
using ThunderPropagator.BuildingBlocks.Application.Serializations.Json;
using ThunderPropagator.FormatSerializers.MessagePack;
using ThunderPropagator.FormatSerializers.Protobuf;
using ThunderPropagator.Infrastructure.Feeders;

namespace ThunderPropagator.UnitTests;

[TestSubject(typeof(FeederMessageDeserializer<,>))]
public class FeederMessageDeserializerTests
{
    private readonly IFormatSerializerRegistry _registry = Substitute.For<IFormatSerializerRegistry>();
    private readonly IFormatDeserializer _formatDeserializer = Substitute.For<IFormatDeserializer>();
    private readonly TestFeederConfiguration _config = new();

    [Fact]
    public void Deserialize_Bytes_Protobuf_DelegatesToRegistry()
    {
        _config.SerializerType = ProtobufFormatSerializer.Protobuf;
        var expected = new TestFeederMessage();
        _registry.GetDeserializer(ProtobufFormatSerializer.Protobuf).Returns(_formatDeserializer);
        _formatDeserializer.Deserialize<TestFeederMessage>(Arg.Any<byte[]>()).Returns(expected);
        var sut = new FeederMessageDeserializer<TestFeederMessage, TestFeederConfiguration>(_config, _registry);

        var result = sut.Deserialize([0x01, 0x02, 0x03]);

        Assert.Equal(expected, result);
        _registry.Received(1).GetDeserializer(ProtobufFormatSerializer.Protobuf);
        _formatDeserializer.Received(1).Deserialize<TestFeederMessage>(Arg.Any<byte[]>());
    }

    [Fact]
    public void Deserialize_Bytes_MessagePack_DelegatesToRegistry()
    {
        _config.SerializerType = MessagePackFormatSerializer.MessagePack;
        var expected = new TestFeederMessage();
        _registry.GetDeserializer(MessagePackFormatSerializer.MessagePack).Returns(_formatDeserializer);
        _formatDeserializer.Deserialize<TestFeederMessage>(Arg.Any<byte[]>()).Returns(expected);
        var sut = new FeederMessageDeserializer<TestFeederMessage, TestFeederConfiguration>(_config, _registry);

        var result = sut.Deserialize([0x04, 0x05, 0x06]);

        Assert.Equal(expected, result);
        _registry.Received(1).GetDeserializer(MessagePackFormatSerializer.MessagePack);
    }

    [Fact]
    public void Deserialize_String_Protobuf_DelegatesToRegistry()
    {
        _config.SerializerType = ProtobufFormatSerializer.Protobuf;
        var expected = new TestFeederMessage();
        _registry.GetDeserializer(ProtobufFormatSerializer.Protobuf).Returns(_formatDeserializer);
        _formatDeserializer.Deserialize<TestFeederMessage>(Arg.Any<string>()).Returns(expected);
        var sut = new FeederMessageDeserializer<TestFeederMessage, TestFeederConfiguration>(_config, _registry);

        var result = sut.Deserialize("proto-payload");

        Assert.Equal(expected, result);
        _registry.Received(1).GetDeserializer(ProtobufFormatSerializer.Protobuf);
        _formatDeserializer.Received(1).Deserialize<TestFeederMessage>(Arg.Any<string>());
    }

    [Fact]
    public void Deserialize_String_MessagePack_DelegatesToRegistry()
    {
        _config.SerializerType = MessagePackFormatSerializer.MessagePack;
        var expected = new TestFeederMessage();
        _registry.GetDeserializer(MessagePackFormatSerializer.MessagePack).Returns(_formatDeserializer);
        _formatDeserializer.Deserialize<TestFeederMessage>(Arg.Any<string>()).Returns(expected);
        var sut = new FeederMessageDeserializer<TestFeederMessage, TestFeederConfiguration>(_config, _registry);

        var result = sut.Deserialize("msgpack-payload");

        Assert.Equal(expected, result);
        _registry.Received(1).GetDeserializer(MessagePackFormatSerializer.MessagePack);
    }

    [Fact]
    public void Deserialize_Bytes_Json_DoesNotUseRegistry()
    {
        _config.SerializerType = JsonFormatSerializer.Json;
        var sut = new FeederMessageDeserializer<TestFeederMessage, TestFeederConfiguration>(_config, _registry);

        sut.Deserialize("{}"u8.ToArray());

        _registry.DidNotReceive().GetDeserializer(Arg.Any<SerializerType>());
    }

    [Fact]
    public void Deserialize_String_Json_DoesNotUseRegistry()
    {
        _config.SerializerType = JsonFormatSerializer.Json;
        var sut = new FeederMessageDeserializer<TestFeederMessage, TestFeederConfiguration>(_config, _registry);

        sut.Deserialize("{}");

        _registry.DidNotReceive().GetDeserializer(Arg.Any<SerializerType>());
    }

    [Fact]
    public void DeserializeInto_Bytes_Protobuf_DelegatesToRegistry()
    {
        _config.SerializerType = ProtobufFormatSerializer.Protobuf;
        var source = new TestFeederMessage();
        _registry.GetDeserializer(ProtobufFormatSerializer.Protobuf).Returns(_formatDeserializer);
        _formatDeserializer.Deserialize<TestFeederMessage>(Arg.Any<byte[]>()).Returns(source);
        var sut = new FeederMessageDeserializer<TestFeederMessage, TestFeederConfiguration>(_config, _registry);
        var target = new TestFeederMessage();

        sut.DeserializeInto([0x01, 0x02], target);

        _registry.Received(1).GetDeserializer(ProtobufFormatSerializer.Protobuf);
    }

    public class TestFeederConfiguration : AbstractFeederConfiguration
    {
    }

    public class TestFeederMessage : FeederMessage
    {
    }
}
