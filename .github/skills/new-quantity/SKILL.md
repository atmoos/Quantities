---
name: new-quantity
description: 'Generate a new physical quantity type (struct) for the Atmoos.Quantities library, including its dimension interface, quantity struct under Quantities/, and cross-quantity operators under Physics/. Use when asked to: add a new quantity, create a new physical quantity type, model a new physical dimension such as Momentum, Torque, Density, or Charge.'
user-invocable: false
---

# New Quantity

Delegates to the `New Quantity` custom agent, which knows how to classify a quantity by its SI dimensional formula (Scalar, PowerOf, Quotient, Quotient with powered denominator, or Product) and generate the corresponding dimension interface, quantity struct, and cross-quantity operators.

## When to Use

- The user asks to add or create a new physical quantity type for the library.
- The user describes a new dimensional quantity (e.g. "add Momentum", "we need a Torque quantity").

## Procedure

Invoke the `New Quantity` subagent (`runSubagent` with `agentName: "New Quantity"`), forwarding the user's request verbatim. That agent contains the full category templates, coding-convention references, and change-classification rules — do not duplicate them here.
