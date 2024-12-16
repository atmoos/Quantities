namespace Atmoos.Quantities.Core.Serialization;

public interface IWriter
{
    void Start();
    void Start(String propertyName);
    void StartArray(String propertyName);
    void Write(String name, Double value);
    void Write(String name, String value);
    void EndArray();
    void End();
}
