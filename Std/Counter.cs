using System;

namespace Std;

/// <summary>
/// Класс для создания счётчика
/// </summary>
public class Counter
{
    /// <summary>
    /// Метод создания счётчика путём замыкания
    /// </summary>
    /// <param name="startValue">Стартовое значение счётчика</param>
    /// <returns>Функция, по которой доступен вызов счётчика</returns>
    public static Func<int> Create(int startValue)
    {
        int cnt = startValue;
        return () => cnt++;
    }
}
