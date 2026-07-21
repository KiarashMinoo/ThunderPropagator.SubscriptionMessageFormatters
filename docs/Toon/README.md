# Toon Subscription Message Formatter

## Contents

- [Overview](#overview)
- [Files](#files)
- [Types & Members](#types--members)
- [Serialization & Contracts](#serialization--contracts)
- [Validation & Constraints](#validation--constraints)
- [Performance Notes](#performance-notes)
- [Package Dependencies](#package-dependencies)
- [Diagrams](#diagrams)
- [Examples](#examples)
- [See Also](#see-also)

## Overview

The Toon module adds TOON support to ThunderPropagator's structured subscription-message pipeline. It contributes a singleton `ISubscriptionMessageFormatter` that advertises `text/toon` and delegates serialization through the shared registry. Unlike the binary HTTP adapters, this module does not add ASP.NET Core MVC input or output formatters.

## Files

| File | Primary type(s)/symbol(s) | LOC (approx) | Responsibility |
|---|---|---:|---|
| `AssemblyInfo.cs` | Assembly attributes | 4 | Exposes internals to test proxies and the unit-test assembly. |
| `DependencyInjection.cs` | `DependencyInjection` | 25 | Registers the Toon subscription-message formatter. |
| `ToonSubscriptionMessageFormatter.cs` | `ToonSubscriptionMessageFormatter` | 13 | Supplies the serializer identity and content type. |
| `ThunderPropagator.SubscriptionMessageFormatters.Toon.csproj` | Project manifest | 8 | Declares core ThunderPropagator and Toon serializer dependencies. |

## Types & Members

| Type | Kind | Summary | Inherits/Implements | Key Members |
|---|---|---|---|---|
| `DependencyInjection` | Static class | Registers Toon subscription formatting. | Extension container | `AddToonSubscriptionMessageFormatter` |
| `ToonSubscriptionMessageFormatter` | Sealed class | Connects structured messages to the TOON serializer. | `StructuredSubscriptionMessageFormatter` | `SerializerType`, `ContentType` |

### DependencyInjection

- **Kind:** Static extension class
- **Namespace:** `ThunderPropagator.SubscriptionMessageFormatters.Toon`
- **Key method:** `IServiceCollection AddToonSubscriptionMessageFormatter(IServiceCollection services)`
- **Validation:** Throws when the service collection is null.
- **Registration behavior:** Adds one singleton implementation of `ISubscriptionMessageFormatter` with `TryAddEnumerable`.
- **Thread-safety:** Intended for application startup; no mutable module state is retained.

**Usage Recipe**

```csharp
builder.Services.AddToonSubscriptionMessageFormatter();
```

### ToonSubscriptionMessageFormatter

- **Kind:** Sealed class
- **Namespace:** `ThunderPropagator.SubscriptionMessageFormatters.Toon`
- **Inherits:** `StructuredSubscriptionMessageFormatter`
- **Constructor:** `ToonSubscriptionMessageFormatter(IFormatSerializerRegistry registry)`
- **Key properties:** `SerializerType : SerializerType` returns the Toon serializer identifier; `ContentType : string` returns `text/toon`.
- **Thread-safety:** Relies on the injected registry and serializer implementation.
- **Serialization notes:** The formatter delegates text-oriented TOON encoding to `ToonFormatSerializer`; the adapter itself does not inspect the model.
- **Validation notes:** No model validation is added by the adapter.

**Usage Recipe**

```csharp
var formatter = serviceProvider
    .GetServices<ISubscriptionMessageFormatter>()
    .Single(candidate => candidate.ContentType == "text/toon");
```

[↑ Back to top](#contents)

## Serialization & Contracts

The formatter delegates text-oriented TOON encoding to `ToonFormatSerializer`; the adapter itself does not inspect the model. The shared `StructuredSubscriptionMessageFormatter` controls message handling, while this adapter fixes the serializer identity and media type used on the wire.

## Validation & Constraints

- The service collection and serializer registry are required.
- The paired Toon serializer must be registered in `IFormatSerializerRegistry`.
- Producers and consumers must agree on `text/toon`.
- Codec-specific model rules remain the responsibility of the format-serializer package.

## Performance Notes

The adapter performs constant-time format selection and adds no additional payload transformation. Runtime cost is dominated by TOON serialization and any transport buffering performed by the shared subscription pipeline.

## Package Dependencies

| Package | Version | Description | Links |
|---|---|---|---|
| `ThunderPropagator` | `1.0.1-beta.186` | Core registry and structured subscription-message abstractions. | [Registry](https://github.com/KiarashMinoo?tab=packages) · [Repository](https://github.com/KiarashMinoo/ThunderPropagator) · [registration](#dependencyinjection) |
| `ThunderPropagator.FormatSerializers.Toon` | `1.0.1-beta.4` | TOON serializer used by this adapter. | [Registry](https://github.com/KiarashMinoo?tab=packages) · [Repository](https://github.com/KiarashMinoo/ThunderPropagator.FormatSerializers) · [formatter](#toonsubscriptionmessageformatter) |

The restored package manifests list ThunderPropagator as author and Apache-2.0 as the license. Shared build properties may select Debug or platform-suffixed package IDs.

## Diagrams

### Subscription formatting flow

```mermaid
sequenceDiagram
    participant App as Application startup
    participant DI as AddToonSubscriptionMessageFormatter
    participant Pipeline as Subscription pipeline
    participant Formatter as ToonSubscriptionMessageFormatter
    participant Registry as IFormatSerializerRegistry
    App->>DI: Register services
    DI-->>Pipeline: Singleton formatter
    Pipeline->>Formatter: Format structured message
    Formatter->>Registry: Resolve Toon serializer
    Registry-->>Formatter: Serialized payload
    Formatter-->>Pipeline: text/toon
```

The adapter is selected by content type and delegates encoding to the registered Toon serializer.

## Examples

```csharp
using ThunderPropagator.SubscriptionMessageFormatters.Toon;

builder.Services.AddToonSubscriptionMessageFormatter();

var candidates = app.Services.GetServices<ISubscriptionMessageFormatter>();
var toon = candidates.Single(
    formatter => formatter.ContentType == "text/toon");
```

## See Also

- [Documentation hub](../README.md)
- [MessagePack](../MessagePack/README.md)
- [NetJson](../NetJson/README.md)
- [Protobuf](../Protobuf/README.md)
- [Toon](../Toon/README.md)
- [Xml](../Xml/README.md)
- [Yaml](../Yaml/README.md)

[↑ Back to top](#contents)
