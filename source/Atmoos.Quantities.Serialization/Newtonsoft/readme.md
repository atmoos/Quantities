# Serialization of Quantities using Newtonsoft.Json

Enables serialisation of [Atmoos.Quantities](https://www.nuget.org/packages/Atmoos.Quantities/) using `Newtonsoft.Json`.

## Usage

Use the extension method:

```csharp
using Quantities.Serialization.Newtonsoft;

/* ... */

public static void Configure(Newtonsoft.Json.JsonSerializerSettings settings)
{
    // good for the most basic units.
    settings.EnableQuantities();

    // optionally, register other assemblies that define more units.
    settings.EnableQuantities(typeof(MyType).Assembly);
}
```
