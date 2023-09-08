namespace Quantities;

public interface IWriter
{
    void Start(String propertyName);
    void Write(String name, Double value);
    void Write(String name, String value);
    void End();
}
