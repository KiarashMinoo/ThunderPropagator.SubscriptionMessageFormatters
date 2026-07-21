using FluentAssertions;
using NSubstitute;
using ThunderPropagator.BuildingBlocks.Application.Serializations;
using ThunderPropagator.SubscriptionMessageFormatters.NetJson;

namespace ThunderPropagator.UnitTests.NetJson;

public sealed class NetJsonSubscriptionMessageFormatterTests
{
    [Fact]
    public void Constructor_ValidRegistry_CreatesFormatter()
    {
        // Arrange
        var registry = Substitute.For<IFormatSerializerRegistry>();

        // Act
        var formatter = new NetJsonSubscriptionMessageFormatter(registry);

        // Assert
        formatter.Should().NotBeNull();
    }

    [Fact]
    public void SerializerType_Get_ReturnsNetJson()
    {
        // Arrange
        var formatter = new NetJsonSubscriptionMessageFormatter(Substitute.For<IFormatSerializerRegistry>());

        // Act
        var result = formatter.SerializerType;

        // Assert
        result.Should().Be(NetJsonFormatSerializer.NetJson);
    }

    [Fact]
    public void ContentType_Get_ReturnsNetJsonMediaType()
    {
        // Arrange
        var formatter = new NetJsonSubscriptionMessageFormatter(Substitute.For<IFormatSerializerRegistry>());

        // Act
        var result = formatter.ContentType;

        // Assert
        result.Should().Be(NetJsonFormatSerializer.NetJsonMediaType);
    }
}
