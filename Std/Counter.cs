namespace Std
{
    using System;

    public class Counter
    {
        public static Func<int> Create(int startValue)
        {
            int cnt = startValue;
            return () => cnt++;
        }
    }
}
