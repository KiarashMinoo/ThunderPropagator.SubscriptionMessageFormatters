# CLAUDE.md

Guidance for working in this repository.

## What this repo is

A thin adapter layer, not a general-purpose serialization library: each sibling project takes a dependency on the matching upstream format-serializer package and wraps it for two consumers — the host application's pub/sub message-envelope formatting contract, and (where applicable) ASP.NET Core's input/output formatter contracts for HTTP content negotiation. The actual encode/decode logic belongs upstream; this repo only adapts it.

## Commands

```powershell
dotnet restore
dotnet build -c Release
dotnet test -c Release
dotnet test --filter "FullyQualifiedName~<Name>"
dotnet pack -c Release -o artifacts/pkg
```

## The per-format template

- **`{Format}SubscriptionMessageFormatter`** — sealed, takes the format-serializer registry as a constructor dependency, derives from the structured-message-formatter base, overrides the serializer-type and content-type it answers to.
- **`{Format}InputFormatter` / `{Format}OutputFormatter`** — only present for formats that also need HTTP content negotiation; derive from the host application's formatter base classes. Not every format ships these — check whether the concern at hand is pub/sub envelope formatting only, or also HTTP negotiation, before assuming both are needed.
- **`DependencyInjection`** — static class exposing one `Add{Format}SubscriptionMessageFormatter` extension, registering the subscription formatter as an enumerable service and, where MVC formatters exist, configuring MVC options to add them.

One existing format doesn't fully delegate to its upstream package and instead carries a local, duplicated serializer — treat that as a legacy exception to fix opportunistically, not a template to copy for a new format.

## Architecture rules (enforced)

- Each format project keeps its types inside its own namespace.
- No sibling formatter project may depend on another — the one-contract-many-implementations shape is checked directly, not just at the shared-package boundary.

## Conventions

- Guard-clause library for argument validation; telemetry activity wrapping around formatting calls, matching the upstream package's convention.
- Nullable + implicit usings on; centrally managed package versions, target frameworks, and version.

## Adding a format

New sibling project depending on the matching upstream serializer package → the subscription-formatter class → input/output formatter classes only if HTTP content negotiation is actually needed for this format → the DI extension → unit tests → an architecture-test row confirming the new project doesn't depend on, and isn't depended on by, any sibling formatter project.

## Testing

xUnit + NSubstitute. A separate architecture-test project enforces per-project namespace isolation and the no-sibling-dependency rule across every format.

## Build & versioning

Version and target frameworks are centralized; CI bumps automatically on a beta branch (prerelease, every push) and a release branch (finalizes the version) — never hand-edit during feature work. Package versions are centrally managed, including the pinned version of the upstream format-serializer package each sibling project depends on.
