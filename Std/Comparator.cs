using System;
using System.Collections.Generic;

namespace Std;

/// <summary>
/// Статический класс для создания объектов IComparer по селектору объекта<br/>
/// </summary>
public static class Comparator
{
    /// <summary>
    /// Метод создания объекта IComparer<br/>
    /// Пример использования: <example>Array.Sort(array, Comparator.GetComparator&lt;User, string&gt;(p => p.Name));</example><br/>
    /// (Сортировка массива по имени User'ов)
    /// </summary>
    /// <typeparam name="TSource">Объект коллекции</typeparam>
    /// <typeparam name="TKey">Тип, которым представлен атрибут объекта коллекции</typeparam>
    /// <param name="keySelector">Указатель на атрибут для сортировки коллекции</param>
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

