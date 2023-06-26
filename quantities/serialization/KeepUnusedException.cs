using System.Runtime.CompilerServices;

namespace Quantities.Serialization;

public sealed class KeepUnusedException : InvalidOperationException
{
    public KeepUnusedException(Object callingClass, [CallerMemberName] String memberName = "")
        : base(CreateMessage(callingClass.GetType().Name, memberName))
    {
    }

    private static String CreateMessage(String caller, String memberName)
    {
        return $"Please don't call the member '{caller}.{memberName}'. It should remain unused.";
    }
}
