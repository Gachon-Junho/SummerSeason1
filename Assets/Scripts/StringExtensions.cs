using System.Linq;

public static class StringExtensions
{
    public static bool AlmostEquals(this string str1, string str2)
    {
        return str1.Intersect(str2).Count() > 0;
    }
}