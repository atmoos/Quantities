# Next 10 Quantities Implementation Plan

Create a concrete implementation plan for adding the next 10 quantity types to Atmoos.Quantities.

## Goal

Produce an implementation plan that prioritises frequently used quantities and only introduces quantities whose dimensions can be composed from dimensions and patterns already present in the codebase.

Do not propose new foundational dimension systems. Build exclusively on existing base and derived dimensions already available in the repository.

## Quantities To Plan

Plan in this exact order:

1. AmountOfSubstance
2. LuminousIntensity
3. ElectricCharge
4. Angle
5. Density
6. VolumetricFlowRate
7. MassFlowRate
8. Momentum
9. Impulse
10. SpecificEnergy

## Dimensional Definitions

Use these dimensions in the plan:

- AmountOfSubstance: $N$
- LuminousIntensity: $J$
- ElectricCharge: $I \cdot T$
- Angle: $L \cdot L^{-1}$
- Density: $M \cdot L^{-3}$
- VolumetricFlowRate: $L^{3} \cdot T^{-1}$
- MassFlowRate: $M \cdot T^{-1}$
- Momentum: $M \cdot L \cdot T^{-1}$
- Impulse: $M \cdot L \cdot T^{-1}$
- SpecificEnergy: $L^{2} \cdot T^{-2}$

## Planning Constraints

- Follow the coding and style rules in .github/copilot-instructions.md.
- Use en-GB spelling in all generated content.
- Use existing quantity category patterns (scalar, quotient, quotient with powered denominator, etc.) and match current repository conventions.
- Include required operator integration points under source/Atmoos.Quantities/Physics.
- Include test strategy for core behaviour, conversions, parsing, and serialisation where relevant.
- Include AI transparency requirements:
  - If a full file is AI-generated, use the .ai. infix in the filename.
  - Annotate AI-generated types and public methods with AiAttribute where required.
- Keep the plan incremental and low-risk.

## Required Output Structure

Return the plan in these sections:

1. Scope and Assumptions
2. Dependency Order and Rationale
3. Phase Plan (4 waves)
4. Per-Quantity Implementation Checklist
5. Operator Additions Matrix
6. Test Plan
7. Delivery Plan (PR slicing)
8. Risks and Mitigations
9. Definition of Done

## Wave Structure

Use this wave grouping:

- Wave 1: AmountOfSubstance, LuminousIntensity
- Wave 2: ElectricCharge, Angle
- Wave 3: Density, VolumetricFlowRate, MassFlowRate
- Wave 4: Momentum, Impulse, SpecificEnergy

## Per-Quantity Checklist Template

For each quantity, include:

- Category and target interfaces
- Files to add/update:
  - source/Atmoos.Quantities/Dimensions/...
  - source/Atmoos.Quantities/Quantities/...
  - source/Atmoos.Quantities/Physics/...
  - source/Atmoos.Quantities.Test/...
  - source/Atmoos.Quantities.Units/ or source/Atmoos.Quantities.Units.Test/ if new units/tests are needed
- Minimum operator set to add
- Required tests
- Estimated complexity (S/M/L)

## Operator Matrix Minimums

Ensure the plan includes, where dimensionally valid:

- ElectricCurrent * Time = ElectricCharge
- Mass / Volume = Density
- Density * Volume = Mass
- Volume / Time = VolumetricFlowRate
- Mass / Time = MassFlowRate
- Mass * Velocity = Momentum
- Force * Time = Impulse
- Energy / Mass = SpecificEnergy

## Validation Commands

Plan should explicitly include running:

```bash
dotnet build source/Atmoos.Quantities.sln
dotnet test source/Atmoos.Quantities.sln --settings source/.runsettings
```

If suggesting coverage checks, include:

```bash
dotnet tool restore
dotnet tool run reportgenerator "-reports:source/**/TestResults/**/coverage.cobertura.xml" "-targetdir:source/coveragereport" "-reporttypes:Html;HtmlSummary"
```

## Important

- Focus on planning quality and implementability.
- Avoid speculative API redesign.
- Prefer consistency with existing quantity implementations over novelty.
