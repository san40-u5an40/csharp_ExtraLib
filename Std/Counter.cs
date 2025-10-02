using System;

namespace Std;

/// <summary>
/// Класс для создания счётчика
/// </summary>
public class Counter
{
    private int counter;

    /// <summary>
    /// Создаёт объект счётчика с указанным начальным значением
    /// </summary>
    /// <param name="startValue">Начальное значение счётчика</param>
    public Counter(int startValue) => counter = startValue;

    /// <summary>
    /// Значение счётчика
    /// </summary>
    public int Value => counter;

    /// <summary>
    /// Метод, инкрементирующий значение счётчика
    /// </summary>
    public void Increment() => counter++;

    /// <summary>
    /// Возвращает функцию вызова счётчика с заданным начальным значением, реализованный при помощи замыкания
    /// </summary>
    /// <param name="startValue">Начальное значение счётчика</param>
    /// <returns>Функция, по которой доступен счётчика</returns>
    public static Func<int> Create(int startValue)
    {
        int cnt = startValue;
        return () => cnt++;
    }
}
