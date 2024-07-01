using System.Collections.Generic;

public class KeywordEqualityComparer : IEqualityComparer<string>
{
    public bool Equals(string x, string y)
    {
        return y.Contains(x);
    }

    public int GetHashCode(string obj) => 0;
}
