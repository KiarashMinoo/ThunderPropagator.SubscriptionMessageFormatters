using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using NSubstitute;
using ThunderPropagator.BuildingBlocks.Application.Serializations;
using ThunderPropagator.BuildingBlocks.Application.Serializations.Json;
using ThunderPropagator.FormatSerializers.MessagePack;
using ThunderPropagator.FormatSerializers.Protobuf;
using ThunderPropagator.Infrastructure.Extensions;
using ThunderPropagator.Infrastructure.Formatters;
using ThunderPropagator.SubscriptionMessageFormatters.MessagePack;
using ThunderPropagator.SubscriptionMessageFormatters.Protobuf;

namespace ThunderPropagator.UnitTests.Formatters;

[TestSubject(typeof(FormatInputFormatter))]
[TestSubject(typeof(FormatOutputFormatter))]
public class FormattersTests
{
    private readonly IFormatSerializerRegistry _registry = Substitute.For<IFormatSerializerRegistry>();
    private readonly IFormatSerializer _serializer = Substitute.For<IFormatSerializer>();

    private IServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddSingleton(_registry);
        return services.BuildServiceProvider();
    }

    // ── SupportedMediaTypes ──────────────────────────────────────────────────

    [Fact]
    public void ProtobufInputFormatter_SupportedMediaTypes_ContainsProtobuf()
        => Assert.Contains(ProtobufFormatSerializer.ProtobufMediaType,
            new ProtobufInputFormatter().SupportedMediaTypes.Select(m => m.ToString()));

    [Fact]
    public void ProtobufOutputFormatter_SupportedMediaTypes_ContainsProtobuf()
        => Assert.Contains(ProtobufFormatSerializer.ProtobufMediaType,
            new ProtobufOutputFormatter().SupportedMediaTypes.Select(m => m.ToString()));

    [Fact]
    public void MessagePackInputFormatter_SupportedMediaTypes_ContainsMessagePack()
        => Assert.Contains(MessagePackFormatSerializer.MessagePackMediaType,
            new MessagePackInputFormatter().SupportedMediaTypes.Select(m => m.ToString()));

    [Fact]
    public void MessagePackOutputFormatter_SupportedMediaTypes_ContainsMessagePack()
        => Assert.Contains(MessagePackFormatSerializer.MessagePackMediaType,
            new MessagePackOutputFormatter().SupportedMediaTypes.Select(m => m.ToString()));

    // ── CanReadType always returns true ──────────────────────────────────────

    [Fact]
    public void ProtobufInputFormatter_CanReadType_AlwaysReturnsTrue()
    {
        var canRead = (bool)typeof(FormatInputFormatter)
            .GetMethod("CanReadType", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .Invoke(new ProtobufInputFormatter(), [typeof(object)])!;
        Assert.True(canRead);
    }

    [Fact]
    public void MessagePackInputFormatter_CanReadType_AlwaysReturnsTrue()
    {
        var canRead = (bool)typeof(FormatInputFormatter)
            .GetMethod("CanReadType", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .Invoke(new MessagePackInputFormatter(), [typeof(object)])!;
        Assert.True(canRead);
    }

    // ── CanWriteType always returns true ─────────────────────────────────────

    [Fact]
    public void ProtobufOutputFormatter_CanWriteType_AlwaysReturnsTrue()
    {
        var canWrite = (bool)typeof(FormatOutputFormatter)
            .GetMethod("CanWriteType", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .Invoke(new ProtobufOutputFormatter(), [typeof(object)])!;
        Assert.True(canWrite);
    }

    [Fact]
    public void MessagePackOutputFormatter_CanWriteType_AlwaysReturnsTrue()
    {
        var canWrite = (bool)typeof(FormatOutputFormatter)
            .GetMethod("CanWriteType", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .Invoke(new MessagePackOutputFormatter(), [typeof(object)])!;
        Assert.True(canWrite);
    }

    // ── 406 — formatters only accept their own media type ────────────────────

    [Fact]
    public void ProtobufOutputFormatter_CanWrite_JsonContentType_ReturnsFalse()
        => Assert.False(new ProtobufOutputFormatter().CanWriteResult(WriteContext(JsonFormatSerializer.JsonMediaType)));

    [Fact]
    public void ProtobufOutputFormatter_CanWrite_ProtobufContentType_ReturnsTrue()
        => Assert.True(new ProtobufOutputFormatter().CanWriteResult(WriteContext(ProtobufFormatSerializer.ProtobufMediaType)));

    [Fact]
    public void MessagePackOutputFormatter_CanWrite_JsonContentType_ReturnsFalse()
        => Assert.False(new MessagePackOutputFormatter().CanWriteResult(WriteContext(JsonFormatSerializer.JsonMediaType)));

    [Fact]
    public void MessagePackOutputFormatter_CanWrite_MessagePackContentType_ReturnsTrue()
        => Assert.True(new MessagePackOutputFormatter().CanWriteResult(WriteContext(MessagePackFormatSerializer.MessagePackMediaType)));

    // ── Output formatter writes bytes using the registry ────────────────────

    [Fact]
    public async Task ProtobufOutputFormatter_WriteResponseBody_CallsProtobufSerializer()
    {
        _registry.GetSerializer(ProtobufFormatSerializer.Protobuf).Returns(_serializer);
        _serializer.MediaType.Returns(ProtobufFormatSerializer.ProtobufMediaType);
        _serializer.SerializeToBytes(Arg.Any<TestModel>()).Returns([0xAA, 0xBB]);

        var responseBody = new MemoryStream();
        var httpContext = new DefaultHttpContext { RequestServices = BuildServiceProvider() };
        httpContext.Response.Body = responseBody;

        await new ProtobufOutputFormatter().WriteResponseBodyAsync(
            new OutputFormatterWriteContext(httpContext, (s, e) => new StreamWriter(s, e), typeof(TestModel), new TestModel()));

        _registry.Received(1).GetSerializer(ProtobufFormatSerializer.Protobuf);
        Assert.Equal(ProtobufFormatSerializer.ProtobufMediaType, httpContext.Response.ContentType);
    }

    [Fact]
    public async Task MessagePackOutputFormatter_WriteResponseBody_CallsMessagePackSerializer()
    {
        _registry.GetSerializer(MessagePackFormatSerializer.MessagePack).Returns(_serializer);
        _serializer.MediaType.Returns(MessagePackFormatSerializer.MessagePackMediaType);
        _serializer.SerializeToBytes(Arg.Any<TestModel>()).Returns([0xCC, 0xDD]);

        var responseBody = new MemoryStream();
        var httpContext = new DefaultHttpContext { RequestServices = BuildServiceProvider() };
        httpContext.Response.Body = responseBody;

        await new MessagePackOutputFormatter().WriteResponseBodyAsync(
            new OutputFormatterWriteContext(httpContext, (s, e) => new StreamWriter(s, e), typeof(TestModel), new TestModel()));

        _registry.Received(1).GetSerializer(MessagePackFormatSerializer.MessagePack);
        Assert.Equal(MessagePackFormatSerializer.MessagePackMediaType, httpContext.Response.ContentType);
    }

    [Fact]
    public async Task ProtobufOutputFormatter_WriteResponseBody_WritesBytesToResponse()
    {
        var expected = new byte[] { 0x01, 0x02, 0x03 };
        _registry.GetSerializer(ProtobufFormatSerializer.Protobuf).Returns(_serializer);
        _serializer.MediaType.Returns(ProtobufFormatSerializer.ProtobufMediaType);
        _serializer.SerializeToBytes(Arg.Any<TestModel>()).Returns(expected);

        var responseBody = new MemoryStream();
        var httpContext = new DefaultHttpContext { RequestServices = BuildServiceProvider() };
        httpContext.Response.Body = responseBody;

        await new ProtobufOutputFormatter().WriteResponseBodyAsync(
            new OutputFormatterWriteContext(httpContext, (s, e) => new StreamWriter(s, e), typeof(TestModel), new TestModel()));

        Assert.Equal(expected, responseBody.ToArray());
    }

    [Fact]
    public async Task MessagePackOutputFormatter_WriteResponseBody_WritesBytesToResponse()
    {
        var expected = new byte[] { 0x04, 0x05, 0x06 };
        _registry.GetSerializer(MessagePackFormatSerializer.MessagePack).Returns(_serializer);
        _serializer.MediaType.Returns(MessagePackFormatSerializer.MessagePackMediaType);
        _serializer.SerializeToBytes(Arg.Any<TestModel>()).Returns(expected);

        var responseBody = new MemoryStream();
        var httpContext = new DefaultHttpContext { RequestServices = BuildServiceProvider() };
        httpContext.Response.Body = responseBody;

        await new MessagePackOutputFormatter().WriteResponseBodyAsync(
            new OutputFormatterWriteContext(httpContext, (s, e) => new StreamWriter(s, e), typeof(TestModel), new TestModel()));

        Assert.Equal(expected, responseBody.ToArray());
    }

    // ── Extension method registers all four formatters ───────────────────────

    [Fact]
    public void AddThunderPropagatorFormatters_RegistersAllFourFormatters()
    {
        var services = new ServiceCollection();
        services
            .AddMessagePackSubscriptionMessageFormatter()
            .AddProtobufSubscriptionMessageFormatter();

        var options = services.BuildServiceProvider()
            .GetRequiredService<IOptions<MvcOptions>>().Value;

        Assert.Contains(options.InputFormatters, f => f is ProtobufInputFormatter);
        Assert.Contains(options.InputFormatters, f => f is MessagePackInputFormatter);
        Assert.Contains(options.OutputFormatters, f => f is ProtobufOutputFormatter);
        Assert.Contains(options.OutputFormatters, f => f is MessagePackOutputFormatter);
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static OutputFormatterWriteContext WriteContext(string contentType)
    {
        var ctx = new OutputFormatterWriteContext(new DefaultHttpContext(), (s, e) => new StreamWriter(s, e), typeof(TestModel), null);
        ctx.ContentType = new StringSegment(contentType);
        return ctx;
    }

    public class TestModel
    {
    }
}
