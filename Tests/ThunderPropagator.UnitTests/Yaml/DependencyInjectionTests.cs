using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ThunderPropagator.Application.PushModules.Formatters;
using ThunderPropagator.SubscriptionMessageFormatters.Yaml;

namespace ThunderPropagator.UnitTests.Yaml;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void AddYamlSubscriptionMessageFormatter_NullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;

        // Act
        var act = () => services.AddYamlSubscriptionMessageFormatter();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddYamlSubscriptionMessageFormatter_ValidServices_RegistersFormatter()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddYamlSubscriptionMessageFormatter();

        // Assert
        result.Should().BeSameAs(services);
        services.Should().ContainSingle(descriptor =>
            descriptor.ServiceType == typeof(ISubscriptionMessageFormatter) &&
            descriptor.ImplementationType == typeof(YamlSubscriptionMessageFormatter));
    }
}
