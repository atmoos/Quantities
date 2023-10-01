# A Motivating Example

Let's look at a simple example of calculating a velocity:

```csharp
public Double CalculateVelocity(Double lengthInMeters, Double timeInSeconds) => lengthInMeters / timeInSeconds;
```

This method declares what units are expected in the names of the arguments and relies on any caller to heed the indicated units. Also, it assumes the caller has enough knowledge of physics that he or she will be able to infer that the returned units are *probably* "m/s", although there is *no guarantee*.

With this library the API would read much clearer:

```csharp
public Velocity CalculateVelocity(Length length, Time time) => length / time;
```

The units have become irrelevant, but the expected quantities are declared in a type-safe way. Also, the returned quantity is now obvious, it's `Velocity`. But "what about the units" you may be asking yourself? Well they are a priori and de facto irrelevant for the caller and the method implementation. However, both the caller and implementer have the power to explicity *choose* whatever makes most sense for their use case or domain.

Again, let's look at an example for the caller and assume she wants to print the result to the console. Let's assume she's a researcher so she'll likely be wanting the SI unit "m/s", which is what she's easily able to express:

```csharp
Velocity velocity = CalculateVelocity(/* any values */); // The units in which the calculation is performed are irrelevant.

Console.WriteLine($"The velocity is: {velocity.To.Si<Metre>().Per.Si<Second>()}"); // The velocity is: 42 m/s;
```

Let's look at an example for someone implementing the `CalculateVelocity` method. This time let's assume it's train manufacturer, so "Kilometres" and "Hours" might be most familiar to them. Let's further assume that complex logic is required that may even involve some other dependency which lets them choose to do the actual calculation using `double`:

```csharp
public Velocity CalculateVelocity(Length length, Time time)
{
  Double lengthInKilometres = length.To.Si<Kilo, Metre>();
  Double timeInHours = time.To.Metric<Hour>();

  Double velocityInKilometresPerHours = /* complex logic */;

  return Velocity.Of(velocityInKilometresPerHours).Si<Kilo, Metre>().Per.Metric<Hour>();
}
```

To maintainers of the above code snippet it will always be clear what units are in use in the implementation, as the scope is narrow. For callers the API is clear and expressive: the quantity is obvious (Velocity) and the units can be anything they chose.
