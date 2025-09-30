namespace Std
{
    using System;
    using System.Collections.Generic;

    public static class NumHelper
    {
        private enum Num
        {
            ноль,
            один,
            два,
            три,
            четыре,
            пять,
            шесть,
            семь,
            восемь,
            девять,
            десять,
            одиннадцать,
            двенадцать,
            тринадцать,
            четырнадцать,
            пятнадцать,
            шестнадцать,
            семнадцать,
            восемнадцать,
            девятнадцать,
            двадцать,
            тридцать = 30,
            сорок = 40,
            пятьдесят = 50,
            шестьдесят = 60,
            семьдесят = 70,
            восемьдесят = 80,
            девяносто = 90
        }

        public static string InWord(this int i)
        {
            // todo: Добавить поддержку большего диапазона чисел

            if (i < 0 || i >= 100)
                throw new ArgumentException("Числа из данного диапазона не поддерживаются");
            else if (i < 20 || i % 10 == 0)
                return $"{(Num)i}";
            else
                return $"{(Num)(i - i % 10) + " " + (Num)(i % 10)}";
        }

        public static IEnumerator<int> GetEnumerator(this int number)
        {
            int k = number > 0 ? number : 0;
            for (int i = number - k; i <= k; i++) yield return i;
        }
    }
}