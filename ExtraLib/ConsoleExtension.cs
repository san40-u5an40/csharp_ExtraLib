using System;

namespace ExtraLib;

/// <summary>
/// Класс со вспомогательными методами по работе в консоли
/// </summary>
public static class ConsoleExtension
{
    /// <summary>
    /// Метод, похожий на подобные try-методы, пробующий записать строку из консоли
    /// </summary>
    /// <param name="input">out-параметр для хранения результирующей строки</param>
    /// <returns>Успешность записи строки из консоли</returns>
    public static bool TryReadLine(out string input)
    {
        string? temp = Console.ReadLine();
        if (string.IsNullOrEmpty(temp))
        {
            input = string.Empty;
            return false;
        }
        else
        {
            input = temp;
            return true;
        }
    }

    /// <summary>
    /// Выводит переданное сообщение в цветном формате без '\n'
    /// </summary>
    /// <param name="msg">Сообщение для вывода</param>
    /// <param name="color">Цвет вывода</param>
    public static void WriteColor(string? msg, ConsoleColor color)
    {
        if (string.IsNullOrEmpty(msg))
            return;

        Console.ForegroundColor = color;
        Console.Write(msg);
        Console.ResetColor();
    }

    /// <summary>
    /// Очищает строку от текста
    /// </summary>
    public static void CleanLine() =>
        Console.Write('\r' + new string(' ', Console.WindowWidth) + '\r');
}