namespace Atmoos.Quantities.Common;

internal static class Introspection
{
    public static Type MostDerivedOf(this Type type, Type toplevel)
    {
        return type.GetInterfaces().Where(i => i.IsAssignableTo(toplevel)).OrderByDescending(i => i.GetInterfaces().Where(ii => ii.IsAssignableTo(toplevel)).Count()).First();
    }
    public static Type MostDerivedOf<TInterface>(this Type type) => type.MostDerivedOf(typeof(TInterface));
    public static Type InnerType(this Type type, Type generic)
    {
        var innerTypes = type.InnerTypes(generic);
        return innerTypes.Length == 1 ? innerTypes[0] : throw new InvalidOperationException($"Expected '{type.Name}' to have exactly one generic argument on the generic interface '{generic.Name}', but found {innerTypes.Length}.");
    }
    public static Type[] InnerTypes(this Type type, Type generic)
    {
        var interfaces = type.IsInterface ? [type] : type.GetInterfaces();
        var genericInterface = interfaces.Where(i => i.ImplementsGeneric(generic)).FirstOrDefault();
        return genericInterface?.GetGenericArguments() ?? [];
    }
    public static Boolean Implements(this Type type, Type interfaceType) => type.IsAssignableTo(interfaceType) || type.GetInterfaces().Any(i => i == interfaceType);
    public static Boolean Implements<TInterface>(this Type type) where TInterface : class => type.Implements(typeof(TInterface));
    public static Boolean ImplementsGeneric(this Type type, Type openGeneric) => type.IsConstructedGenericType && type.GetGenericTypeDefinition() == openGeneric;
}
