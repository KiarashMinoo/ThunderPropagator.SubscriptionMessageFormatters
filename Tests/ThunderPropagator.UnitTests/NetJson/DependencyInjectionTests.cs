using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ThunderPropagator.Application.PushModules.Formatters;
using ThunderPropagator.SubscriptionMessageFormatters.NetJson;

namespace ThunderPropagator.UnitTests.NetJson;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void AddNetJsonSubscriptionMessageFormatter_NullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;

        // Act
        var act = () => services.AddNetJsonSubscriptionMessageFormatter();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddNetJsonSubscriptionMessageFormatter_ValidServices_RegistersFormatter()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddNetJsonSubscriptionMessageFormatter();

        // Assert
        result.Should().BeSameAs(services);
        services.Should().ContainSingle(descriptor =>
            descriptor.ServiceType == typeof(ISubscriptionMessageFormatter) &&
            descriptor.ImplementationType == typeof(NetJsonSubscriptionMessageFormatter));
    }
}
