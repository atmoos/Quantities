namespace Atmoos.Quantities;

public sealed class AiAttribute : Attribute
{
    public String Model { get; set; } = String.Empty;
    public String Variant { get; set; } = String.Empty;
    public String Version { get; set; } = String.Empty;
}
