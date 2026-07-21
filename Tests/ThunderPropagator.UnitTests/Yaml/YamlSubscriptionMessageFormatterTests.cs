using FluentAssertions;
using NSubstitute;
using ThunderPropagator.BuildingBlocks.Application.Serializations;
using ThunderPropagator.FormatSerializers.Yaml;
using ThunderPropagator.SubscriptionMessageFormatters.Yaml;

namespace ThunderPropagator.UnitTests.Yaml;

public sealed class YamlSubscriptionMessageFormatterTests
{
    [Fact]
    public void Constructor_ValidRegistry_CreatesFormatter()
    {
        // Arrange
        var registry = Substitute.For<IFormatSerializerRegistry>();

        // Act
        var formatter = new YamlSubscriptionMessageFormatter(registry);

        // Assert
        formatter.Should().NotBeNull();
    }

    [Fact]
    public void SerializerType_Get_ReturnsYaml()
    {
        // Arrange
        var formatter = new YamlSubscriptionMessageFormatter(Substitute.For<IFormatSerializerRegistry>());

        // Act
        var result = formatter.SerializerType;

        // Assert
        result.Should().Be(YamlFormatSerializer.Yaml);
    }

    [Fact]
    public void ContentType_Get_ReturnsYamlMediaType()
    {
        // Arrange
        var formatter = new YamlSubscriptionMessageFormatter(Substitute.For<IFormatSerializerRegistry>());

        // Act
        var result = formatter.ContentType;

        // Assert
        result.Should().Be(YamlFormatSerializer.YamlMediaType);
    }
}
