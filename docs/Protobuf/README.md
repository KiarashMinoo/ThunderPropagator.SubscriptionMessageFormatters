# Protobuf

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

The **Protobuf** area groups 4 documented types, including `DependencyInjection`, `ProtobufInputFormatter`, `ProtobufOutputFormatter`, `ProtobufSubscriptionMessageFormatter`. It provides the contracts and implementation used by this part of ThunderPropagator.SubscriptionMessageFormatters.

## Files

| File | Primary type(s)/symbol(s) | LOC (approx.) | Responsibility |
|---|---|---:|---|
| `AssemblyInfo.cs` | — | 6 | Contains the assembly info implementation or configuration. |
| `DependencyInjection.cs` | `DependencyInjection` | 29 | Defines DependencyInjection and its related behavior. |
| `ProtobufInputFormatter.cs` | `ProtobufInputFormatter` | 6 | Defines ProtobufInputFormatter and its related behavior. |
| `ProtobufOutputFormatter.cs` | `ProtobufOutputFormatter` | 6 | Defines ProtobufOutputFormatter and its related behavior. |
| `ProtobufSubscriptionMessageFormatter.cs` | `ProtobufSubscriptionMessageFormatter` | 13 | Defines ProtobufSubscriptionMessageFormatter and its related behavior. |
| `ThunderPropagator.SubscriptionMessageFormatters.Protobuf.csproj` | — | 8 | Defines project build targets, dependencies, and package metadata. |

## Types and Members

| Type | Kind | Summary | Inherits/Implements | Key Members |
|---|---|---|---|---|
| [`DependencyInjection`](#dependencyinjection) | class | Extension methods for registering ThunderPropagator BuildingBlocks services. | — | `AddProtobufSubscriptionMessageFormatter(…)` |
| [`ProtobufInputFormatter`](#protobufinputformatter) | class | Represents the ProtobufInputFormatter class. | — | — |
| [`ProtobufOutputFormatter`](#protobufoutputformatter) | class | Represents the ProtobufOutputFormatter class. | — | — |
| [`ProtobufSubscriptionMessageFormatter`](#protobufsubscriptionmessageformatter) | class | Represents the ProtobufSubscriptionMessageFormatter class. | — | `SerializerType`, `ContentType` |

### DependencyInjection

- **Kind:** class
- **Namespace:** `ThunderPropagator.SubscriptionMessageFormatters.Protobuf`
- **Inherits/implements:** None declared
- **Attributes:** None detected
- **Key members:** `AddProtobufSubscriptionMessageFormatter(…)`
- **Summary:** Extension methods for registering ThunderPropagator BuildingBlocks services.
- **Thread safety:** Follow the lifetime and concurrency guarantees of the owning component; no additional guarantee is inferred.

**Usage recipe**

```csharp
// Resolve DependencyInjection from the configured service container or construct it with its declared dependencies.
```

[↑ Back to top](#contents)

### ProtobufInputFormatter

- **Kind:** class
- **Namespace:** `ThunderPropagator.SubscriptionMessageFormatters.Protobuf`
- **Inherits/implements:** None declared
- **Attributes:** None detected
- **Key members:** Refer to the API surface in the source package
- **Summary:** Represents the ProtobufInputFormatter class.
- **Thread safety:** Follow the lifetime and concurrency guarantees of the owning component; no additional guarantee is inferred.

**Usage recipe**

```csharp
// Resolve ProtobufInputFormatter from the configured service container or construct it with its declared dependencies.
```

[↑ Back to top](#contents)

### ProtobufOutputFormatter

- **Kind:** class
- **Namespace:** `ThunderPropagator.SubscriptionMessageFormatters.Protobuf`
- **Inherits/implements:** None declared
- **Attributes:** None detected
- **Key members:** Refer to the API surface in the source package
- **Summary:** Represents the ProtobufOutputFormatter class.
- **Thread safety:** Follow the lifetime and concurrency guarantees of the owning component; no additional guarantee is inferred.

**Usage recipe**

```csharp
// Resolve ProtobufOutputFormatter from the configured service container or construct it with its declared dependencies.
```

[↑ Back to top](#contents)

### ProtobufSubscriptionMessageFormatter

- **Kind:** class
- **Namespace:** `ThunderPropagator.SubscriptionMessageFormatters.Protobuf`
- **Inherits/implements:** None declared
- **Attributes:** None detected
- **Key members:** `SerializerType`, `ContentType`
- **Summary:** Represents the ProtobufSubscriptionMessageFormatter class.
- **Thread safety:** Follow the lifetime and concurrency guarantees of the owning component; no additional guarantee is inferred.

**Usage recipe**

```csharp
// Resolve ProtobufSubscriptionMessageFormatter from the configured service container or construct it with its declared dependencies.
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
  Current["Protobuf"]
  Current --> T0["DependencyInjection"]
  Current --> T1["ProtobufInputFormatter"]
  Current --> T2["ProtobufOutputFormatter"]
  Current --> T3["ProtobufSubscriptionMessageFormatter"]
```

The diagram shows the direct components documented by the **Protobuf** area.

## Examples

Start with `DependencyInjection` as the primary entry point for this folder, then follow its linked contracts and collaborators.

## See Also

- [Documentation home](../README.md)
- [MessagePack](../MessagePack/README.md)
- [NetJson](../NetJson/README.md)
- [Toon](../Toon/README.md)
- [Xml](../Xml/README.md)
- [Yaml](../Yaml/README.md)

[↑ Back to top](#contents)
