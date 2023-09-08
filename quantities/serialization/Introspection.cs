namespace Quantities.Serialization;

internal static class Introspection
{
    public static Type MostDerivedOf<TInterface>(this Type type)
    {
        var toplevel = typeof(TInterface);
        return type.GetInterfaces().Where(i => i.IsAssignableTo(toplevel)).OrderByDescending(i => i.GetInterfaces().Where(ii => ii.IsAssignableTo(toplevel)).Count()).First();
    }
    public static Type InnerType(this Type type, Type generic)
    {
        var innerTypes = type.InnerTypes(generic);
        return innerTypes.Length == 1 ? innerTypes[0] : throw new InvalidOperationException($"Expected '{type.Name}' to have exactly one generic argument on the generic interface '{generic.Name}', but found {innerTypes.Length}.");
    }
    public static Type[] InnerTypes(this Type type, Type generic)
    {
        var genericInterface = type.GetInterfaces().Where(i => i.IsConstructedGenericType && i.GetGenericTypeDefinition() == generic).FirstOrDefault();
        return genericInterface?.GetGenericArguments() ?? Array.Empty<Type>();
    }
}
