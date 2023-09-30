using System.Reflection;

namespace Quantities.Serialization;

internal static class Reflection
{
    private static readonly MethodInfo getRepresentation = GetGenericMethod(typeof(Reflection), nameof(GetRepresentation), 1);
    public static String GetRepresentation(Type type)
    {
        var representation = getRepresentation.MakeGenericMethod(type);
        return (representation?.Invoke(null, null)) as String ?? throw new InvalidOperationException($"Failed getting representation for: {type.Name}");
    }
    private static String GetRepresentation<T>() where T : IRepresentable => T.Representation;
    public static MethodInfo GetGenericMethod(Type declaringType, String name, Int32 typeArgumentCount)
    {
        var genericMethod = declaringType.GetMethod(name, typeArgumentCount, BindingFlags.Static | BindingFlags.NonPublic, null, CallingConventions.Standard, Type.EmptyTypes, null);
        return genericMethod ?? throw new InvalidOperationException($"Method '{name}' not found");
    }

}
