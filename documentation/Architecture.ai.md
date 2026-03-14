# Architecture

This document describes the repository architecture using C4-style models.

Scope:

- Solution: `source/Atmoos.Quantities.sln`
- Projects modelled: all C# projects currently included in the solution

## C4 Level 1: System Context

```mermaid
flowchart LR
    user[Library Consumer<br/>Application developer]
    contributor[Project Contributor<br/>Maintainer]
    ci[CI Pipeline]

    subgraph sys[Atmoos.Quantities Repository]
        coreSystem[Atmoos.Quantities ecosystem<br/>Type-safe quantities, units, serialization, tests, and benchmarks]
    end

    nuget[NuGet.org]
    dotnet[.NET SDK and Runtime]
    json[JSON serializers<br/>System.Text.Json and Newtonsoft.Json]
    bdn[BenchmarkDotNet]
    xunit[xUnit]

    user -->|Uses published packages| coreSystem
    contributor -->|Develops and tests| coreSystem
    ci -->|Builds, tests, packs| coreSystem

    coreSystem -->|Publishes packages| nuget
    coreSystem -->|Targets| dotnet
    coreSystem -->|Integrates with| json
    coreSystem -->|Performance measurements via| bdn
    coreSystem -->|Automated tests via| xunit
```

## C4 Level 2: Container View (Projects)

Each C# project is modelled as a container.

```mermaid
flowchart TB
    subgraph runtime[Runtime and extension containers]
        q[Atmoos.Quantities<br/>Core quantity engine]
        u[Atmoos.Quantities.Units<br/>Additional unit catalogue]
        stj[Atmoos.Quantities.Serialization.Text.Json<br/>System.Text.Json adapter]
        nsj[Atmoos.Quantities.Serialization.Newtonsoft<br/>Newtonsoft.Json adapter]
    end

    subgraph quality[Verification and support containers]
        tt[Atmoos.Quantities.TestTools<br/>Shared test helpers]
        qt[Atmoos.Quantities.Test<br/>Core tests]
        ut[Atmoos.Quantities.Units.Test<br/>Units tests]
        stjt[Atmoos.Quantities.Serialization.Text.Json.Test<br/>System.Text.Json tests]
        nsjt[Atmoos.Quantities.Serialization.Newtonsoft.Test<br/>Newtonsoft tests]
    end

    subgraph perf[Performance container]
        b[Atmoos.Quantities.Benchmark<br/>Benchmark harness]
    end

    q --> dotnet[.NET]
    nsj --> newtonsoft[Newtonsoft.Json]
    stj --> stjlib[System.Text.Json]
    qt --> xunit[xUnit]
    ut --> xunit
    stjt --> xunit
    nsjt --> xunit
    b --> bdn[BenchmarkDotNet]

    u --> q
    stj --> q
    nsj --> q

    tt --> q
    qt --> q
    qt --> tt

    ut --> u
    ut --> tt

    stjt --> u
    stjt --> stj

    nsjt --> u
    nsjt --> nsj

    b --> u
    b --> stj
    b --> nsj
```

## C4 Level 3: Component Views

## Atmoos.Quantities (Core)

Primary components are represented by top-level folders and public entry points.

```mermaid
flowchart LR
    api[Public API<br/>Systems.cs, Operators.cs, Extensions.cs]
    core[Core<br/>Quantity, Measure, numerics]
    dims[Dimensions<br/>Dimension model and contracts]
    measures[Measures<br/>Scalar, square, cubic, aliases]
    quantities[Quantities<br/>Strongly typed quantity structs]
    units[Units contracts<br/>IUnit families]
    parsing[Parsing<br/>Text to quantity]
    physics[Physics<br/>Formula and domain operations]
    prefixes[Prefixes<br/>Metric and binary prefixes]
    creation[Creation<br/>Factory helpers]
    common[Common<br/>Shared infrastructure]

    api --> creation
    api --> measures
    api --> quantities
    quantities --> core
    quantities --> dims
    quantities --> units
    measures --> core
    measures --> dims
    parsing --> quantities
    parsing --> measures
    physics --> quantities
    prefixes --> measures
    creation --> core
    common --> core
```

## Atmoos.Quantities.Units

```mermaid
flowchart LR
    si[SI units]
    imperial[Imperial units]
    nonstandard[Non-standard units]
    unitscore[Atmoos.Quantities<br/>Core abstractions]

    si --> unitscore
    imperial --> unitscore
    nonstandard --> unitscore
```

## Serialization Adapters

```mermaid
flowchart LR
    subgraph stjproj[Atmoos.Quantities.Serialization.Text.Json]
        stjconv[QuantityConverter]
        stjser[QuantitySerialization]
        stjdes[Deserializer]
        stjext[Extensions]
    end

    subgraph nsjproj[Atmoos.Quantities.Serialization.Newtonsoft]
        nsjconv[QuantityConverter]
        nsjser[QuantitySerialization]
        nsjdes[Deserializer]
        nsjext[Extensions]
    end

    qcore[Atmoos.Quantities]
    stjsdk[System.Text.Json]
    nsjsdk[Newtonsoft.Json]

    stjconv --> qcore
    stjser --> qcore
    stjdes --> qcore
    stjext --> stjsdk

    nsjconv --> qcore
    nsjser --> qcore
    nsjdes --> qcore
    nsjext --> nsjsdk
```

## Verification and Benchmarks

```mermaid
flowchart TB
    tt[Atmoos.Quantities.TestTools<br/>Assertions, convenience helpers]

    qt[Atmoos.Quantities.Test]
    ut[Atmoos.Quantities.Units.Test]
    stjt[Atmoos.Quantities.Serialization.Text.Json.Test]
    nsjt[Atmoos.Quantities.Serialization.Newtonsoft.Test]
    b[Atmoos.Quantities.Benchmark]

    core[Atmoos.Quantities]
    units[Atmoos.Quantities.Units]
    stj[Serialization.Text.Json]
    nsj[Serialization.Newtonsoft]

    qt --> core
    qt --> tt

    ut --> units
    ut --> tt

    stjt --> stj
    stjt --> units

    nsjt --> nsj
    nsjt --> units

    b --> units
    b --> stj
    b --> nsj
```

## Project Inventory

- Atmoos.Quantities: core runtime container
- Atmoos.Quantities.Units: extension unit catalogue container
- Atmoos.Quantities.Serialization.Text.Json: System.Text.Json adapter container
- Atmoos.Quantities.Serialization.Newtonsoft: Newtonsoft.Json adapter container
- Atmoos.Quantities.TestTools: shared testing support container
- Atmoos.Quantities.Test: core behaviour and regression tests container
- Atmoos.Quantities.Units.Test: units coverage tests container
- Atmoos.Quantities.Serialization.Text.Json.Test: System.Text.Json adapter tests container
- Atmoos.Quantities.Serialization.Newtonsoft.Test: Newtonsoft adapter tests container
- Atmoos.Quantities.Benchmark: performance measurement container

## Notes

- The container relationships are based on current project references in the solution.
- Component decomposition is intentionally coarse-grained at folder and entry-point level to remain maintainable as code evolves.
