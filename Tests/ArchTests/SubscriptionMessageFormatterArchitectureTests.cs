using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using ThunderPropagator.SubscriptionMessageFormatters.MessagePack;
using ThunderPropagator.SubscriptionMessageFormatters.NetJson;
using ThunderPropagator.SubscriptionMessageFormatters.Protobuf;
using ThunderPropagator.SubscriptionMessageFormatters.Toon;
using ThunderPropagator.SubscriptionMessageFormatters.Xml;
using ThunderPropagator.SubscriptionMessageFormatters.Yaml;
using Xunit;

namespace ArchTests;

public sealed class SubscriptionMessageFormatterArchitectureTests
{
    public static IEnumerable<object[]> FormatterAssemblies()
    {
        yield return [typeof(MessagePackSubscriptionMessageFormatter).Assembly, "ThunderPropagator.SubscriptionMessageFormatters.MessagePack"];
        yield return [typeof(NetJsonSubscriptionMessageFormatter).Assembly, "ThunderPropagator.SubscriptionMessageFormatters.NetJson"];
        yield return [typeof(ProtobufSubscriptionMessageFormatter).Assembly, "ThunderPropagator.SubscriptionMessageFormatters.Protobuf"];
        yield return [typeof(ToonSubscriptionMessageFormatter).Assembly, "ThunderPropagator.SubscriptionMessageFormatters.Toon"];
        yield return [typeof(XmlSubscriptionMessageFormatter).Assembly, "ThunderPropagator.SubscriptionMessageFormatters.Xml"];
        yield return [typeof(YamlSubscriptionMessageFormatter).Assembly, "ThunderPropagator.SubscriptionMessageFormatters.Yaml"];
    }

    public static IEnumerable<object[]> ForbiddenAssemblyDependencies()
    {
        var assemblies = FormatterAssemblies()
            .Select(values => ((Assembly)values[0], (string)values[1]))
            .ToArray();

        foreach (var source in assemblies)
        foreach (var target in assemblies)
        {
            if (source.Item1 != target.Item1)
                yield return [source.Item1, target.Item2];
        }
    }

    [Theory]
    [MemberData(nameof(FormatterAssemblies))]
    public void Types_InFormatterAssembly_ResideInExpectedNamespace(Assembly assembly, string expectedNamespace)
    {
        // Arrange
        var types = Types.InAssembly(assembly);

        // Act
        var result = types.Should().ResideInNamespace(expectedNamespace).GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(
            "all types should reside in {0}; failing types: {1}",
            expectedNamespace,
            string.Join(", ", result.FailingTypeNames ?? []));
    }

    [Theory]
    [MemberData(nameof(ForbiddenAssemblyDependencies))]
    public void FormatterAssembly_OtherFormatterAssembly_HasNoDependency(
        Assembly sourceAssembly,
        string forbiddenNamespace)
    {
        // Arrange
        var types = Types.InAssembly(sourceAssembly);

        // Act
        var result = types.ShouldNot().HaveDependencyOn(forbiddenNamespace).GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(
            "{0} should not depend on sibling formatter namespace {1}; failing types: {2}",
            sourceAssembly.GetName().Name,
            forbiddenNamespace,
            string.Join(", ", result.FailingTypeNames ?? []));
    }

    [Fact]
    public void Helpers_InHelpersNamespace_AreStatic()
    {
        // Arrange
        var helperTypes = typeof(NetJsonHelper).Assembly.GetTypes()
            .Where(type => type.Namespace?.Contains("Helpers", StringComparison.Ordinal) == true)
            .ToArray();

        // Act
        var nonStaticTypes = helperTypes.Where(type => !type.IsAbstract || !type.IsSealed);

        // Assert
        nonStaticTypes.Should().BeEmpty();
    }
}
