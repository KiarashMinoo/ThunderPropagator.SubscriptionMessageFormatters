using FluentAssertions;
using NSubstitute;
using ThunderPropagator.BuildingBlocks.Application.Serializations;
using ThunderPropagator.FormatSerializers.Xml;
using ThunderPropagator.SubscriptionMessageFormatters.Xml;

namespace ThunderPropagator.UnitTests.Xml;

public sealed class XmlSubscriptionMessageFormatterTests
{
    [Fact]
    public void Constructor_ValidRegistry_CreatesFormatter()
    {
        // Arrange
        var registry = Substitute.For<IFormatSerializerRegistry>();

        // Act
        var formatter = new XmlSubscriptionMessageFormatter(registry);

        // Assert
        formatter.Should().NotBeNull();
    }

    [Fact]
    public void SerializerType_Get_ReturnsXml()
    {
        // Arrange
        var formatter = new XmlSubscriptionMessageFormatter(Substitute.For<IFormatSerializerRegistry>());

        // Act
        var result = formatter.SerializerType;

        // Assert
        result.Should().Be(XmlFormatSerializer.Xml);
    }

    [Fact]
    public void ContentType_Get_ReturnsXmlMediaType()
    {
        // Arrange
        var formatter = new XmlSubscriptionMessageFormatter(Substitute.For<IFormatSerializerRegistry>());

        // Act
        var result = formatter.ContentType;

        // Assert
        result.Should().Be(XmlFormatSerializer.XmlMediaType);
    }
}
