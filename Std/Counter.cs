using System;

namespace Std;

public class Counter
{
    public static Func<int> Create(int startValue)
    {
        int cnt = startValue;
        return () => cnt++;
    }
}
