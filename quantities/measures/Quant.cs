namespace Quantities.Measures;
public readonly struct Quant
{
    private readonly Map map;
    public Double Value { get; }
    internal Quant(in Double value, Map map)
    {
        this.map = map;
        this.Value = value;
    }
    public static Quant operator +(Quant left, Quant right)
    {
        var rightValue = left.map.Project(right.map, right.Value);
        return new(left.Value + rightValue, left.map);
    }
    public static Quant operator -(Quant left, Quant right)
    {
        var rightValue = left.map.Project(right.map, right.Value);
        return new(left.Value - rightValue, left.map);
    }
    public static Quant operator *(Double scalar, Quant right)
    {
        return new(scalar * right.Value, right.map);
    }
    public static Quant operator *(Quant left, Double scalar)
    {
        return new(scalar * left.Value, left.map);
    }
    public static Quant operator /(Quant left, Double scalar)
    {
        return new(left.Value / scalar, left.map);
    }
    public static Double operator /(Quant left, Quant right)
    {
        var rightValue = left.map.Project(right.map, right.Value);
        return left.Value / rightValue;
    }
}