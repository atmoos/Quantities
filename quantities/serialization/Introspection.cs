namespace Quantities.Serialization;

internal static class Introspection
{
    public static Type MostDerivedOf<TInterface>(this Type type)
    {
        var toplevel = typeof(TInterface);
        return type.GetInterfaces().Where(i => i.IsAssignableTo(toplevel)).OrderByDescending(i => i.GetInterfaces().Where(ii => ii.IsAssignableTo(toplevel)).Count()).First();
    }
    public static Type[] InnerTypes(this Type type, Type generic)
    {
        var genericInterface = type.GetInterfaces().Where(i => i.IsConstructedGenericType && i.GetGenericTypeDefinition() == generic).First();
        return genericInterface.GetGenericArguments();
    }
}
