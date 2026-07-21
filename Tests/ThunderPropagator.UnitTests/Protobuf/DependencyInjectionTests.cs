using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ThunderPropagator.Application.PushModules.Formatters;
using ThunderPropagator.SubscriptionMessageFormatters.Protobuf;

namespace ThunderPropagator.UnitTests.Protobuf;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void AddProtobufSubscriptionMessageFormatter_NullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;

        // Act
        var act = () => services.AddProtobufSubscriptionMessageFormatter();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddProtobufSubscriptionMessageFormatter_ValidServices_RegistersFormatterAndMvcFormatters()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddProtobufSubscriptionMessageFormatter();
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<MvcOptions>>().Value;

        // Assert
        result.Should().BeSameAs(services);
        services.Should().ContainSingle(descriptor =>
            descriptor.ServiceType == typeof(ISubscriptionMessageFormatter) &&
            descriptor.ImplementationType == typeof(ProtobufSubscriptionMessageFormatter));
        options.InputFormatters.Should().ContainSingle(formatter => formatter is ProtobufInputFormatter);
        options.OutputFormatters.Should().ContainSingle(formatter => formatter is ProtobufOutputFormatter);
    }
}
