using System.Text;
using FluentAssertions;
using ThunderPropagator.SubscriptionMessageFormatters.NetJson;

namespace ThunderPropagator.UnitTests.NetJson;

public sealed class NetJsonFormatSerializerTests
{
    private readonly NetJsonFormatSerializer _serializer = new();

    [Fact]
    public void SerializeToBytes_ValidObject_ReturnsUtf8SerializedValue()
    {
        // Arrange
        var model = new TestModel { Name = "Test" };

        // Act
        var result = _serializer.SerializeToBytes(model);

        // Assert
        Encoding.UTF8.GetString(result).Should().Be(_serializer.Serialize(model));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Deserialize_EmptyOrWhitespaceString_ReturnsDefault(string value)
    {
        // Arrange
        var input = value;

        // Act
        var result = _serializer.Deserialize<TestModel>(input);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Deserialize_EmptyBytes_ReturnsDefault()
    {
        // Arrange
        var input = Array.Empty<byte>();

        // Act
        var result = _serializer.Deserialize<TestModel>(input);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void MediaType_Get_ReturnsApplicationJson()
    {
        // Arrange
        var serializer = _serializer;

        // Act
        var result = serializer.MediaType;

        // Assert
        result.Should().Be(NetJsonFormatSerializer.NetJsonMediaType);
    }

    private sealed class TestModel
    {
        public string Name { get; set; } = string.Empty;
    }
}
