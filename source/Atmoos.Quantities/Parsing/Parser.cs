using Atmoos.Quantities.Core.Construction;
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Parsing;

public interface IParser<T>
    where T : struct, IQuantity<T>, IDimension
{
    T Parse(String input)
    {
        if (this.TryParse(input, out var result)) {
            return result;
        }

        throw new FormatException($"The input '{input}' is not a valid representation of a quantity of '{typeof(T).Name}'.");
    }
    T Parse(Double value, String unit)
    {
        if (this.TryParse(value, unit, out var result)) {
            return result;
        }

        throw new FormatException($"The unit '{unit}' is not a valid unit for a quantity of '{typeof(T).Name}'.");
    }
    Boolean TryParse(String input, out T result)
    {
        result = default;
        var parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (parts.Length != 2 || !Double.TryParse(parts[0], out var value)) {
            return false;
        }

        return this.TryParse(value, parts[1], out result);
    }
    Boolean TryParse(Double value, String unit, out T result);
}

public static class Parser
{
    public static IParser<T> Create<T>(UnitRepository repository)
        where T : struct, IQuantity<T>, IDimension
        => new Parser<T>(repository, new ModelParser(repository.Subset<T>()));
}

internal sealed class Parser<T>(UnitRepository repository, ModelParser parser) : IParser<T>
    where T : struct, IQuantity<T>, IDimension
{
    public Boolean TryParse(Double value, String unit, out T result)
    {
        List<QuantityModel> models = [.. parser.Parse(unit)];
        try {
            (var success, result) = models switch {
                [] => (false, default),
                [var single] => (true, QuantityFactory<T>.Create(repository, single).Build(value)),
                [_, _] => (true, QuantityFactory<T>.Create(repository, models).Build(value)),
                _ => (false, default),
            };
            return success;
        }
        // ToDo: Refine exception handling, i.e. add a TryCreate method to QuantityFactory
        catch (InvalidOperationException) {
        }
        catch (NotSupportedException) {
        }
        result = default;
        return false;
    }
}

