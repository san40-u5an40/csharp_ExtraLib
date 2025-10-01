using System;
using System.Text;

namespace Std;

public static class Display
{
    // Настройки окна консоли:
    //   - Установка заголовка
    //   - Установка ширины (длина дисплея + небольшое пространство)
    //   - Отключение видимости курсора
    //   - Установка юникода
    public static void WindowSetup(byte displayLength, byte displaySpace, string programName = "Default Program")
    {
        Console.Title = programName;
        Console.WindowWidth = displayLength + displaySpace;
        Console.CursorVisible = false;
        Console.InputEncoding = Encoding.Unicode;
    }

    public enum Type { Start, End, Center }

    // Вывод дисплея с поставленными переносами строки, в зависимости от типа дисплея: начальный, серединный, заключительный
    public static void Print(Type typeDisp, int length, char charDisp, params string[]? msgs)
    {
        if(msgs == null)
            return;

        Console.ForegroundColor = ConsoleColor.DarkRed;

        switch (typeDisp)
        {
            case Type.Start:
                PrintDisp(length, charDisp, msgs);
                Console.Write('\n');
                break;
            case Type.End:
                Console.Write('\n');
                PrintDisp(length, charDisp, msgs);
                break;
            case Type.Center:
                Console.Write('\n');
                PrintDisp(length, charDisp, msgs);
                Console.Write('\n');
                break;
        };

        Console.ResetColor();
    }

    // Выводит саму дисплейную плашку, состоящую из:
    //   - Одной строки с пустой надписью
    //   - Строк, которые мы передали в параметр
    //   - Одной строки с пустой надписью
    private static void PrintDisp(int length, char charDisp, params string[] msgs)
    {
        PrintString(length, charDisp, string.Empty);
        foreach (string msg in msgs)
            PrintString(length, charDisp, msg);
        PrintString(length, charDisp, string.Empty);
    }

    // Выводит дисплейную строку:
    //   - В начале и конце ставятся "/*" и "*/" соответственно, в стилистике инлайн комментирования
    //   - В середине вставляется надпись
    //   - Пустое пространство между надписью и краями строки заполняется символом из параметра
    private static void PrintString(int length, char charDisp, string msg)
    {
        if (length < msg.Length + 2)
            return;

        var resultString = new StringBuilder();
        int charsLength = length - msg.Length;

        resultString
            .Append("/*")
            .Append(new string(charDisp, charsLength / 2))
            .Append(msg)
            .Append(new string(charDisp, charsLength / 2));

        if (charsLength % 2 != 0)
            resultString.Append(charDisp);

        resultString.Append("*/");

        Console.WriteLine(resultString.ToString());
    }
}