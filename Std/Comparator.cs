using System;
using System.Collections.Generic;

namespace Std;

// Статический класс для создания объектов IComparer по селектору объекта
// Пример использования: Array.Sort(array, Comparator.GetComparator<User, string>(p => p.Name));
public static class Comparator
{
    public static IComparer<TSource> GetComparator<TSource, TKey>(Func<TSource, TKey?> keySelector) =>
        new KeySelectorComparer<TSource, TKey?>(keySelector);
}

file class KeySelectorComparer<TSource, TKey> : IComparer<TSource>
{
    private Func<TSource, TKey?> keySelector;
    private Comparer<TKey?> comparer = Comparer<TKey?>.Default;

    internal KeySelectorComparer(Func<TSource, TKey?> keySelector) => this.keySelector = keySelector;

    public int Compare(TSource? obj1, TSource? obj2)
    {
        if (obj1 == null || obj2 == null)
            throw new ArgumentException("Некорректные значения для сравнения");

        return comparer.Compare(keySelector(obj1), keySelector(obj2));
    }
}

