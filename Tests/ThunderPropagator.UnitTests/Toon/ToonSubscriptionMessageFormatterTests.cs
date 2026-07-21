using FluentAssertions;
using NSubstitute;
using ThunderPropagator.BuildingBlocks.Application.Serializations;
using ThunderPropagator.FormatSerializers.Toon;
using ThunderPropagator.SubscriptionMessageFormatters.Toon;

namespace ThunderPropagator.UnitTests.Toon;

public sealed class ToonSubscriptionMessageFormatterTests
{
    [Fact]
    public void Constructor_ValidRegistry_CreatesFormatter()
    {
        // Arrange
        var registry = Substitute.For<IFormatSerializerRegistry>();

        // Act
        var formatter = new ToonSubscriptionMessageFormatter(registry);

        // Assert
        formatter.Should().NotBeNull();
    }

    [Fact]
    public void SerializerType_Get_ReturnsToon()
    {
        // Arrange
        var formatter = new ToonSubscriptionMessageFormatter(Substitute.For<IFormatSerializerRegistry>());

        // Act
        var result = formatter.SerializerType;

        // Assert
        result.Should().Be(ToonFormatSerializer.Toon);
    }

    [Fact]
    public void ContentType_Get_ReturnsToonMediaType()
    {
        // Arrange
        var formatter = new ToonSubscriptionMessageFormatter(Substitute.For<IFormatSerializerRegistry>());

        // Act
        var result = formatter.ContentType;

        // Assert
        result.Should().Be(ToonFormatSerializer.ToonMediaType);
    }
}
