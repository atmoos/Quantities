using System.Runtime.CompilerServices;

namespace Atmoos.Quantities.TestTools;

public static class Assert
{
    public static void Condition(Boolean condition, [CallerArgumentExpression(nameof(condition))] String expression = "")
        => Xunit.Assert.True(condition, $"Unmet condition: {expression}");
}

