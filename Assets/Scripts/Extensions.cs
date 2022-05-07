

using System.Runtime.CompilerServices;

public static class Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Get<T>(this T[] array, int index1, int index2)
    {
        return array[index1 * Data.TotalPlayers + index2];
    }
}
