# CLAUDE.md

<!-- Humans: keep this file lean — the per-format template lives in .claude/rules/, not here. -->

## What This Repo Is

Thin adapter layer, not a general-purpose serialization library: each sibling project depends on the matching upstream format-serializer package and wraps it for two consumers — the host app's pub/sub message-envelope formatting contract, and (where applicable) ASP.NET Core's input/output formatter contracts for HTTP content negotiation. Encode/decode logic belongs upstream; this repo only adapts it.

## Commands

```powershell
dotnet restore
dotnet build -c Release
dotnet test -c Release
dotnet test --filter "FullyQualifiedName~<Name>"
dotnet pack -c Release -o artifacts/pkg
```

Per-format class template: `.claude/rules/format-template.md`.

## Architecture Rules (Enforced)

- Each format project's types stay in its own namespace.
- No sibling formatter project depends on another.

## Conventions

- Guard-clause library for argument validation; telemetry activity wrapping around formatting calls, matching the upstream package's convention.
- Nullable + implicit usings on; centrally managed package versions, TFMs, version.

## Adding a Format

New sibling project (depends on the matching upstream serializer package) → subscription-formatter class → input/output formatter classes only if HTTP negotiation is needed → DI extension → unit tests → architecture-test row confirming no dependency on/from any sibling formatter project.

## Testing

xUnit + NSubstitute. Architecture-test project enforces per-project namespace isolation and no-sibling-dependency across every format.

## Build & Versioning

Version/TFMs centralized; CI bumps automatically on a beta branch (prerelease, every push) and a release branch (finalizes version) — never hand-edit during feature work. Package versions centrally managed, including the pinned upstream format-serializer version each sibling project depends on.
