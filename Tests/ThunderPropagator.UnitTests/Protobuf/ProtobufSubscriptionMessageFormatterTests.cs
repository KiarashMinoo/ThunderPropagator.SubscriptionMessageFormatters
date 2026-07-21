using FluentAssertions;
using NSubstitute;
using ThunderPropagator.BuildingBlocks.Application.Serializations;
using ThunderPropagator.FormatSerializers.Protobuf;
using ThunderPropagator.SubscriptionMessageFormatters.Protobuf;

namespace ThunderPropagator.UnitTests.Protobuf;

public sealed class ProtobufSubscriptionMessageFormatterTests
{
    [Fact]
    public void Constructor_ValidRegistry_CreatesFormatter()
    {
        // Arrange
        var registry = Substitute.For<IFormatSerializerRegistry>();

        // Act
        var formatter = new ProtobufSubscriptionMessageFormatter(registry);

        // Assert
        formatter.Should().NotBeNull();
    }

    [Fact]
    public void SerializerType_Get_ReturnsProtobuf()
    {
        // Arrange
        var formatter = new ProtobufSubscriptionMessageFormatter(Substitute.For<IFormatSerializerRegistry>());

        // Act
        var result = formatter.SerializerType;

        // Assert
        result.Should().Be(ProtobufFormatSerializer.Protobuf);
    }

    [Fact]
    public void ContentType_Get_ReturnsProtobufMediaType()
    {
        // Arrange
        var formatter = new ProtobufSubscriptionMessageFormatter(Substitute.For<IFormatSerializerRegistry>());

        // Act
        var result = formatter.ContentType;

        // Assert
        result.Should().Be(ProtobufFormatSerializer.ProtobufMediaType);
    }
}
