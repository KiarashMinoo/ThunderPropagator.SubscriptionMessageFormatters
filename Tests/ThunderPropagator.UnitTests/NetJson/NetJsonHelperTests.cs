using System.Text;
using FluentAssertions;
using ThunderPropagator.SubscriptionMessageFormatters.NetJson;
using ThunderPropagator.UnitTests.Serializations;

namespace ThunderPropagator.UnitTests.NetJson;

public sealed class NetJsonHelperTests
{
    [Fact]
    public void ToNetJson_ValidObject_UsesCamelCaseByDefault()
    {
        // Arrange
        var model = new SimpleTestObject { Name = "Ada", Value = 42 };

        // Act
        var result = model.ToNetJson();

        // Assert
        result.Should().Contain("\"name\"").And.NotContain("\"Name\"");
    }

    [Fact]
    public void ToNetJson_SettingsCallback_InvokesCallback()
    {
        // Arrange
        var callbackInvoked = false;
        var model = new SimpleTestObject();

        // Act
        model.ToNetJson(settings =>
        {
            callbackInvoked = true;
            return settings;
        });

        // Assert
        callbackInvoked.Should().BeTrue();
    }

    [Fact]
    public void ToNetJsonBytes_ValidObject_ReturnsUtf8Json()
    {
        // Arrange
        var model = new SimpleTestObject { Name = "Grace", Value = 7 };
        var expected = Encoding.UTF8.GetBytes(model.ToNetJson());

        // Act
        var result = model.ToNetJsonBytes();

        // Assert
        result.Should().Equal(expected);
    }

    [Fact]
    public void ToNetJsonBase64_ValidObject_ReturnsBase64EncodedJson()
    {
        // Arrange
        var model = new SimpleTestObject { Name = "Linus", Value = 1 };

        // Act
        var result = model.ToNetJsonBase64();

        // Assert
        Encoding.UTF8.GetString(Convert.FromBase64String(result)).Should().Be(model.ToNetJson());
    }

    [Fact]
    public void FromNetJson_GenericJson_ReturnsTypedObject()
    {
        // Arrange
        var expected = new SimpleTestObject { Name = "Margaret", Value = 99 };
        var json = expected.ToNetJson();

        // Act
        var result = json.FromNetJson<SimpleTestObject>();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void FromNetJson_RuntimeType_ReturnsRequestedObject()
    {
        // Arrange
        var expected = new SimpleTestObject { Name = "Barbara", Value = 5 };
        var json = expected.ToNetJson();

        // Act
        var result = json.FromNetJson(typeof(SimpleTestObject));

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void FromNetJsonBytes_EmptyOrWhitespace_ReturnsDefault(string value)
    {
        // Arrange
        var bytes = Encoding.UTF8.GetBytes(value);

        // Act
        var result = bytes.FromNetJsonBytes<SimpleTestObject>();

        // Assert
        result.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void FromNetJsonBase64_EmptyOrWhitespace_ReturnsDefault(string value)
    {
        // Arrange
        var input = value;

        // Act
        var result = input.FromNetJsonBase64<SimpleTestObject>();

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void FromNetJsonBase64_InvalidBase64_ThrowsFormatException()
    {
        // Arrange
        const string input = "not-base64";

        // Act
        var act = () => input.FromNetJsonBase64<SimpleTestObject>();

        // Assert
        act.Should().Throw<FormatException>();
    }

}
