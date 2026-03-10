namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public sealed class AiAttribute : Attribute
{
    public String Model { get; set; } = String.Empty;
    public String Variant { get; set; } = String.Empty;
    public String Version { get; set; } = String.Empty;
}
