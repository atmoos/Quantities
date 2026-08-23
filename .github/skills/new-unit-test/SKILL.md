---
name: new-unit-test
description: 'Generate xUnit tests for newly added units (measures) in Atmoos.Quantities.Units.Test, covering formatting, SI round-trip, cross-system conversion, and definition equivalence. Use when asked to: add tests for a new unit, test a new measure, write unit tests for a unit conversion, verify a unit definition.'
user-invocable: false
---

# New Unit Test

Delegates to the `New Unit Test` custom agent, which knows the test project structure and the established xUnit test patterns (formatting, SI round-trip, cross-system conversion, definition equivalence, intra-system conversion) for units in `Atmoos.Quantities.Units.Test`.

## When to Use

- The user asks to add or generate tests for a newly added unit.
- The user asks to verify a unit's conversion or definition with tests.

## Procedure

Invoke the `New Unit Test` subagent (`runSubagent` with `agentName: "New Unit Test"`), forwarding the user's request verbatim. That agent contains the full test-category patterns and project structure reference — do not duplicate them here.
