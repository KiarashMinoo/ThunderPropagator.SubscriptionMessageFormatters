using FluentAssertions;
using NSubstitute;
using ThunderPropagator.BuildingBlocks.Application.Serializations;
using ThunderPropagator.FormatSerializers.MessagePack;
using ThunderPropagator.SubscriptionMessageFormatters.MessagePack;

namespace ThunderPropagator.UnitTests.MessagePack;

public sealed class MessagePackSubscriptionMessageFormatterTests
{
    [Fact]
    public void Constructor_ValidRegistry_CreatesFormatter()
    {
        // Arrange
        var registry = Substitute.For<IFormatSerializerRegistry>();

        // Act
        var formatter = new MessagePackSubscriptionMessageFormatter(registry);

        // Assert
        formatter.Should().NotBeNull();
    }

    [Fact]
    public void SerializerType_Get_ReturnsMessagePack()
    {
        // Arrange
        var formatter = new MessagePackSubscriptionMessageFormatter(Substitute.For<IFormatSerializerRegistry>());

        // Act
        var result = formatter.SerializerType;

        // Assert
        result.Should().Be(MessagePackFormatSerializer.MessagePack);
    }

    [Fact]
    public void ContentType_Get_ReturnsMessagePackMediaType()
    {
        // Arrange
        var formatter = new MessagePackSubscriptionMessageFormatter(Substitute.For<IFormatSerializerRegistry>());

        // Act
        var result = formatter.ContentType;

        // Assert
        result.Should().Be(MessagePackFormatSerializer.MessagePackMediaType);
    }
}
