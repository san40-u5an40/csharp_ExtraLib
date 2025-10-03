using System;

namespace Std;

/// <summary>
/// Класс для создания счётчика
/// </summary>
public class Counter
{
    private int counter;
    private string name;

    /// <summary>
    /// Создаёт объект счётчика с указанным начальным значением и названием
    /// </summary>
    /// <param name="startValue">Начальное значение счётчика</param>
    /// <param name="name">Название счётчика (по умолчанию: "Default")</param>
    public Counter(int startValue, string name = "Default") =>
        (this.counter, this.name) = (startValue, name);

    /// <summary>
    /// Значение счётчика
    /// </summary>
    public int Value => counter;

    /// <summary>
    /// Название счётчика
    /// </summary>
    public string Name => name;

    /// <summary>
    /// Метод, инкрементирующий значение счётчика, доступен к переопределению
    /// </summary>
    public virtual void Increment() => counter++;

    // Группа переопределённых методов Object
    public override string ToString() => $"{name}: {counter}";
    public sealed override bool Equals(object? obj)
    {
        if (obj is Counter cnt)
            return (this.Value, this.Name) == (cnt.Value, cnt.Name);

        return false;
    }
    public sealed override int GetHashCode() => (Value, Name).GetHashCode();

    // Группа переопределённых операторов
    public static bool operator ==(Counter c1, Counter c2) => c1.Equals(c2);
    public static bool operator !=(Counter c1, Counter c2) => !c1.Equals(c2);
    public static Counter operator ++(Counter c1)
    {
        c1!.Increment();
        return c1!;
    }

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
