using System.Runtime.CompilerServices;

namespace Quantities;

internal sealed class ShouldBeUnusedException : InvalidOperationException
{
    public ShouldBeUnusedException(Object callingClass, [CallerMemberName] String memberName = "")
        : base(CreateMessage(callingClass.GetType().Name, memberName))
    {
    }

    private static String CreateMessage(String caller, String memberName)
    {
        return $"Please don't call the member '{caller}.{memberName}'. It should remain unused.";
    }
}
