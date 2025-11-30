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

        throw new FormatException($"The input '{input}' is not a valid representation of a quantity of type '{typeof(T)}'.");
    }
    Boolean TryParse(String input, out T result)
    {
        result = default;
        var parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (parts.Length != 2 || !Double.TryParse(parts[0], out var value)) {
            return false;
        }

        var units = parts[1];
        return this.TryParse(value, units, out result);
    }
    Boolean TryParse(Double value, String units, out T result);
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
    public Boolean TryParse(Double value, String units, out T result)
    {
        List<QuantityModel> models = [.. parser.Parse(units)];
        try {
            (var outcome, result) = models switch {
                [] => (false, default),
                [var single] => (true, QuantityFactory<T>.Create(repository, single).Build(value)),
                [_, _] => (true, QuantityFactory<T>.Create(repository, models).Build(value)),
                _ => (false, default),
            };
            return outcome;
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

