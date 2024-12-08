using System.Collections.Generic;

public class IntArrayComparer : IEqualityComparer<int[]>
{
    public bool Equals(int[] x, int[] y)
    {
        if (x == null || y == null)
            return false;
        if (x.Length != y.Length)
            return false;
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] != y[i])
                return false;
        }
        return true;
    }

    public int GetHashCode(int[] obj)
    {
        if (obj == null)
            return 0;
        int hash = 17;
        foreach (int element in obj)
        {
            hash = hash * 31 + element;
        }
        return hash;
    }
}
