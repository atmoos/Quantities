# Serialization of Quantities using System.Text.Json

Enables serialisation of [Atmoos.Quantities](https://www.nuget.org/packages/Atmoos.Quantities/) using `System.Text.Json`.

## Usage

Use the extension method:

```csharp
using Quantities.Serialization.Text.Json;

/* ... */

public static void Configure(System.Text.Json.JsonSerializerOptions options)
{
    // good for the most basic units.
    options.EnableQuantities();

    // optionally, register other assemblies that define more units.
    options.EnableQuantities(typeof(MyType).Assembly);
}
```
