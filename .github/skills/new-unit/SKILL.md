---
name: new-unit
description: 'Generate a new unit of measurement for the Atmoos.Quantities library (SI, Metric, Imperial, or NonStandard), including its ToSi conversion. Use when asked to: add a new unit, create a new unit of measurement, add a conversion for a unit, support a new measure such as a currency-agnostic length, mass, or other unit.'
user-invocable: false
---

# New Unit

Delegates to the `New Unit` custom agent, which knows how to classify a unit by its system marker interface (`ISiUnit`, `IMetricUnit`, `IImperialUnit`, `INonStandardUnit`), including special patterns for alias, invertible, and compound-quantity units, and generate the corresponding struct with its `ToSi` conversion.

## When to Use

- The user asks to add or create a new unit of measurement for the library.
- The user describes a new unit and its conversion factor (e.g. "add the Stone unit", "we need Fahrenheit").

## Procedure

Invoke the `New Unit` subagent (`runSubagent` with `agentName: "New Unit"`), forwarding the user's request verbatim. That agent contains the full system templates, conversion-expression helpers, and special-pattern guidance — do not duplicate them here.
