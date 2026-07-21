using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ThunderPropagator.Application.PushModules.Formatters;
using ThunderPropagator.SubscriptionMessageFormatters.Xml;

namespace ThunderPropagator.UnitTests.Xml;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void AddXmlSubscriptionMessageFormatter_NullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;

        // Act
        var act = () => services.AddXmlSubscriptionMessageFormatter();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddXmlSubscriptionMessageFormatter_ValidServices_RegistersFormatter()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddXmlSubscriptionMessageFormatter();

        // Assert
        result.Should().BeSameAs(services);
        services.Should().ContainSingle(descriptor =>
            descriptor.ServiceType == typeof(ISubscriptionMessageFormatter) &&
            descriptor.ImplementationType == typeof(XmlSubscriptionMessageFormatter));
    }
}
