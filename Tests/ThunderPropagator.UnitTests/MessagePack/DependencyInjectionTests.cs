using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ThunderPropagator.Application.PushModules.Formatters;
using ThunderPropagator.SubscriptionMessageFormatters.MessagePack;

namespace ThunderPropagator.UnitTests.MessagePack;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void AddMessagePackSubscriptionMessageFormatter_NullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;

        // Act
        var act = () => services.AddMessagePackSubscriptionMessageFormatter();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddMessagePackSubscriptionMessageFormatter_ValidServices_RegistersFormatterAndMvcFormatters()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddMessagePackSubscriptionMessageFormatter();
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<MvcOptions>>().Value;

        // Assert
        result.Should().BeSameAs(services);
        services.Should().ContainSingle(descriptor =>
            descriptor.ServiceType == typeof(ISubscriptionMessageFormatter) &&
            descriptor.ImplementationType == typeof(MessagePackSubscriptionMessageFormatter));
        options.InputFormatters.Should().ContainSingle(formatter => formatter is MessagePackInputFormatter);
        options.OutputFormatters.Should().ContainSingle(formatter => formatter is MessagePackOutputFormatter);
    }
}
