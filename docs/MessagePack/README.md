# MessagePack

## Contents

- [Overview](#overview)
- [Files](#files)
- [Types and Members](#types-and-members)
- [Serialization and Contracts](#serialization-and-contracts)
- [Validation and Constraints](#validation-and-constraints)
- [Diagrams](#diagrams)
- [Examples](#examples)
- [See Also](#see-also)

## Overview

The **MessagePack** area groups 4 documented types, including `DependencyInjection`, `MessagePackInputFormatter`, `MessagePackOutputFormatter`, `MessagePackSubscriptionMessageFormatter`. It provides the contracts and implementation used by this part of ThunderPropagator.SubscriptionMessageFormatters.

## Files

| File | Primary type(s)/symbol(s) | LOC (approx.) | Responsibility |
|---|---|---:|---|
| `AssemblyInfo.cs` | — | 6 | Contains the assembly info implementation or configuration. |
| `DependencyInjection.cs` | `DependencyInjection` | 28 | Defines DependencyInjection and its related behavior. |
| `MessagePackInputFormatter.cs` | `MessagePackInputFormatter` | 6 | Defines MessagePackInputFormatter and its related behavior. |
| `MessagePackOutputFormatter.cs` | `MessagePackOutputFormatter` | 6 | Defines MessagePackOutputFormatter and its related behavior. |
| `MessagePackSubscriptionMessageFormatter.cs` | `MessagePackSubscriptionMessageFormatter` | 13 | Defines MessagePackSubscriptionMessageFormatter and its related behavior. |
| `ThunderPropagator.SubscriptionMessageFormatters.MessagePack.csproj` | — | 8 | Defines project build targets, dependencies, and package metadata. |

## Types and Members

| Type | Kind | Summary | Inherits/Implements | Key Members |
|---|---|---|---|---|
| [`DependencyInjection`](#dependencyinjection) | class | Extension methods for registering ThunderPropagator BuildingBlocks services. | — | `AddMessagePackSubscriptionMessageFormatter(…)` |
| [`MessagePackInputFormatter`](#messagepackinputformatter) | class | Represents the MessagePackInputFormatter class. | — | — |
| [`MessagePackOutputFormatter`](#messagepackoutputformatter) | class | Represents the MessagePackOutputFormatter class. | — | — |
| [`MessagePackSubscriptionMessageFormatter`](#messagepacksubscriptionmessageformatter) | class | Represents the MessagePackSubscriptionMessageFormatter class. | — | `SerializerType`, `ContentType` |

### DependencyInjection

- **Kind:** class
- **Namespace:** `ThunderPropagator.SubscriptionMessageFormatters.MessagePack`
- **Inherits/implements:** None declared
- **Attributes:** None detected
- **Key members:** `AddMessagePackSubscriptionMessageFormatter(…)`
- **Summary:** Extension methods for registering ThunderPropagator BuildingBlocks services.
- **Thread safety:** Follow the lifetime and concurrency guarantees of the owning component; no additional guarantee is inferred.

**Usage recipe**

```csharp
// Resolve DependencyInjection from the configured service container or construct it with its declared dependencies.
```

[↑ Back to top](#contents)

### MessagePackInputFormatter

- **Kind:** class
- **Namespace:** `ThunderPropagator.SubscriptionMessageFormatters.MessagePack`
- **Inherits/implements:** None declared
- **Attributes:** None detected
- **Key members:** Refer to the API surface in the source package
- **Summary:** Represents the MessagePackInputFormatter class.
- **Thread safety:** Follow the lifetime and concurrency guarantees of the owning component; no additional guarantee is inferred.

**Usage recipe**

```csharp
// Resolve MessagePackInputFormatter from the configured service container or construct it with its declared dependencies.
```

[↑ Back to top](#contents)

### MessagePackOutputFormatter

- **Kind:** class
- **Namespace:** `ThunderPropagator.SubscriptionMessageFormatters.MessagePack`
- **Inherits/implements:** None declared
- **Attributes:** None detected
- **Key members:** Refer to the API surface in the source package
- **Summary:** Represents the MessagePackOutputFormatter class.
- **Thread safety:** Follow the lifetime and concurrency guarantees of the owning component; no additional guarantee is inferred.

**Usage recipe**

```csharp
// Resolve MessagePackOutputFormatter from the configured service container or construct it with its declared dependencies.
```

[↑ Back to top](#contents)

### MessagePackSubscriptionMessageFormatter

- **Kind:** class
- **Namespace:** `ThunderPropagator.SubscriptionMessageFormatters.MessagePack`
- **Inherits/implements:** None declared
- **Attributes:** None detected
- **Key members:** `SerializerType`, `ContentType`
- **Summary:** Represents the MessagePackSubscriptionMessageFormatter class.
- **Thread safety:** Follow the lifetime and concurrency guarantees of the owning component; no additional guarantee is inferred.

**Usage recipe**

```csharp
// Resolve MessagePackSubscriptionMessageFormatter from the configured service container or construct it with its declared dependencies.
```

[↑ Back to top](#contents)

## Serialization and Contracts

Serialization behavior is part of the public wire or persistence contract in this area. Preserve field names, ordering rules, content negotiation, and backward-compatibility expectations when changing these types.

## Validation and Constraints

Inputs are validated at component boundaries. Callers should provide non-null required values and handle domain or argument exceptions without retrying invalid requests unchanged.

## Diagrams

### Component overview

```mermaid
graph TD
  Current["MessagePack"]
  Current --> T0["DependencyInjection"]
  Current --> T1["MessagePackInputFormatter"]
  Current --> T2["MessagePackOutputFormatter"]
  Current --> T3["MessagePackSubscriptionMessageFormatter"]
```

The diagram shows the direct components documented by the **MessagePack** area.

## Examples

Start with `DependencyInjection` as the primary entry point for this folder, then follow its linked contracts and collaborators.

## See Also

- [Documentation home](../README.md)
- [NetJson](../NetJson/README.md)
- [Protobuf](../Protobuf/README.md)
- [Toon](../Toon/README.md)
- [Xml](../Xml/README.md)
- [Yaml](../Yaml/README.md)

[↑ Back to top](#contents)
