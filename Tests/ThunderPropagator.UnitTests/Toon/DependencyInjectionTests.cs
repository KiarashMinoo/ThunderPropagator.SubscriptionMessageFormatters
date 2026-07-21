using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ThunderPropagator.Application.PushModules.Formatters;
using ThunderPropagator.SubscriptionMessageFormatters.Toon;

namespace ThunderPropagator.UnitTests.Toon;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void AddToonSubscriptionMessageFormatter_NullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;

        // Act
        var act = () => services.AddToonSubscriptionMessageFormatter();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddToonSubscriptionMessageFormatter_ValidServices_RegistersFormatter()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddToonSubscriptionMessageFormatter();

        // Assert
        result.Should().BeSameAs(services);
        services.Should().ContainSingle(descriptor =>
            descriptor.ServiceType == typeof(ISubscriptionMessageFormatter) &&
            descriptor.ImplementationType == typeof(ToonSubscriptionMessageFormatter));
    }
}
