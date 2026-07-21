using System.Linq;
using NetArchTest.Rules;
using Xunit;

namespace ArchTests
{
    public class ArchitectureTests
    {
        [Fact]
        public void Application_Should_Not_Depend_On_Infrastructure()
        {
            var result = Types.InAssembly(typeof(ThunderPropagator.BuildingBlocks.Application.ServiceConfiguration).Assembly)
                .That().ResideInNamespace("ThunderPropagator.BuildingBlocks.Application")
                .ShouldNot().HaveDependencyOn("ThunderPropagator.SubscriptionMessageFormatters.MessagePack").GetResult();

            Assert.True(result.IsSuccessful, string.Join("\n", result.FailingTypeNames ?? Enumerable.Empty<string>()));
        }
    }
}
