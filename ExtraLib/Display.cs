using System;
using System.Text;

namespace ExtraLib;

/// <summary>
/// Класс для настройки консольного окна, а также вывода дисплея
/// </summary>
public static class Display
{
    /// <summary>
    /// Настраивает окно консоли:
    /// - Устанавливает заголовок окна в соответствии с названием программы
    /// - Устанавливает ширину окна (длина дисплея + небольшое пространство)
    /// - Отключает видимость курсора
    /// - Устанавливает кодировку: юникод
    /// </summary>
    /// <param name="displayLength">Длина дисплея</param>
    /// <param name="displaySpace">Свободное пространство между дисплеем и краем окна (рекомендуется не меньше 5)</param>
    /// <param name="programName">Название программы</param>
    public static void WindowSetup(byte displayLength, byte displaySpace, string programName = "Default Program")
    {
        Console.Title = programName;
        Console.WindowWidth = displayLength + displaySpace;
        Console.CursorVisible = false;
        Console.InputEncoding = Encoding.Unicode;
    }

    /// <summary>
    /// Тип дисплея:
    /// <list type="bullet">
    ///    <item>
    ///        <term>Start</term>
    ///        <description>После плашки дисплея добавляется '\n'</description>
    ///    </item>
    ///    <item>
    ///        <term>End</term>
    ///        <description>'\n' добавляется перед дисплеем</description>
    ///    </item>
    ///    <item>
    ///        <term>Center</term>
    ///        <description>'\n' добавляется как до, так и после дисплея</description>
    ///    </item>
    /// </list>
    /// </summary>
    public enum Type { Start, End, Center }

    /// <summary>
    /// Выводит дисплей
    /// </summary>
    /// <param name="typeDisp">Тип дисплея представленный перечислением Display.Type</param>
    /// <param name="length">Длина дисплея (рекомендуемый размер от 60)</param>
    /// <param name="charDisp">Знак дисплея</param>
    /// <param name="msgs">Сообщения для отображения на дисплее</param>
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