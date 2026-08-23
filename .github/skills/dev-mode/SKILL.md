---
name: dev-mode
description: 'Toggle the Atmoos.Quantities repository between development mode (ProjectReference) and release mode (PackageReference) for the downstream Units and Serialization projects. Use when asked to: switch to development mode, switch to release mode, link projects directly instead of via NuGet, restore package references, toggle project vs package references.'
user-invocable: false
---

# Dev Mode

Delegates to the `Dev Mode` custom agent, which knows how to toggle `Atmoos.Quantities.Units`, `Atmoos.Quantities.Serialization.Text.Json`, and `Atmoos.Quantities.Serialization.Newtonsoft` between `ProjectReference` and `PackageReference` to `Atmoos.Quantities`.

## When to Use

- The user asks to switch the repository to development mode or release mode.
- The user wants downstream projects to reference `Atmoos.Quantities` via project reference (for rapid iteration on unreleased changes) or via package reference (for clean packaging/publishing).

## Procedure

Invoke the `Dev Mode` subagent (`runSubagent` with `agentName: "Dev Mode"`), forwarding the user's request verbatim. That agent contains the full replacement rules, safety constraints, and build-verification steps — do not duplicate them here.
