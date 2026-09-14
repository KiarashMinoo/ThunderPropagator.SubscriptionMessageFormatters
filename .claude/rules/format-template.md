---
paths:
  - "src/ThunderPropagator.SubscriptionMessageFormatters.MessagePack/**"
  - "src/ThunderPropagator.SubscriptionMessageFormatters.NetJson/**"
  - "src/ThunderPropagator.SubscriptionMessageFormatters.Protobuf/**"
  - "src/ThunderPropagator.SubscriptionMessageFormatters.Toon/**"
  - "src/ThunderPropagator.SubscriptionMessageFormatters.Xml/**"
  - "src/ThunderPropagator.SubscriptionMessageFormatters.Yaml/**"
---

# Per-Format Template

- **`{Format}SubscriptionMessageFormatter`** — sealed, takes the format-serializer registry as a constructor dep, derives from the structured-message-formatter base, overrides the serializer-type + content-type it answers to.
- **`{Format}InputFormatter` / `{Format}OutputFormatter`** — only for formats needing HTTP content negotiation; derive from the host app's formatter base classes. Not every format ships these — check pub/sub-only vs. also-HTTP before assuming both.
- **`DependencyInjection`** — static class, one `Add{Format}SubscriptionMessageFormatter` extension: registers the subscription formatter as an enumerable service, configures MVC options where MVC formatters exist.

One existing format carries a local, duplicated serializer instead of fully delegating upstream — legacy exception to fix opportunistically, not a template to copy.
