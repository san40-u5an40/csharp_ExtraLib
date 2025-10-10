//#define TESTS
//#define REFLECTION
//#define ISOLATION

namespace SandBox;

public class Program
{
#if !ISOLATION
    private const string programName = "*НАЗВАНИЕ ПРОГРАММЫ*";
    private const byte displayLength = 130;
    private const char displayChar = '-';

    internal static void Sandbox(string[] args)
    {
        Start(out Stopwatch timer);



        End(ref timer);
    }



    //Настройка окна консоли и вывод начального дисплея программы
    private static void Start(out Stopwatch timer)
    {
        byte displaySpace = 5;

        Display.WindowSetup(displayLength, displaySpace, programName);

        Display.Print
        (
            Display.Type.Start,
            displayLength,
            displayChar,
            "Добро пожаловать в программу",
            programName
        );

        timer = Stopwatch.StartNew();

#if RELEASE
        // Для релиза удобно начинать программу по нажатию
        Console.Write("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
        Console.Write('\r' + new string(' ', displayLength) + '\r');
#endif
    }

    // Вывод заключительного дисплея программы
    private static void End(ref Stopwatch timer)
    {
        timer.Stop();

#if DEBUG
        // В режиме DEBUG удобно иметь информацию об аллокациях
        long totalBytesAlloc = GC.GetTotalAllocatedBytes();
        var size = Bytes.ToSize(totalBytesAlloc);
#endif

        Display.Print
        (
            Display.Type.End,
            displayLength,
            displayChar,
            "Программа завершена"

#if DEBUG
            // Вывод информации о времени выполнения программ и аллокациях в DEBUG
            , $"Время выполнения: {timer.ElapsedMilliseconds} мс. или {timer.ElapsedMilliseconds / 1000} сек."
            , $"Использовано памяти: {size.GByte} ГБ. {size.MByte} МБ. {size.KByte} КБ. {size.Byte} Б."
#endif
        );

#if RELEASE
        // Завершать программу в RELEASE тоже удобно по нажатию клавиши
        Console.ReadKey();
#endif
    }
#endif // ← ISOLATION

    // В зависимости от режима в main активируются разные блоки кода:
    // - Обычный режим для тестирования библиотек в методе SandBox
    // - Режим для тестирования библиотек
    // - Режим для вывода рефлексивной информации о пользовательских типах, помеченных атрибутом Reflection
    public static void Main(string[] args)
    {
#if !TESTS && !REFLECTION && !ISOLATION
        // Обычный режим функционирования программы
        Sandbox(args);
#endif

#if TESTS && RELEASE
        // В режиме TESTS реализует только запуск теста или рефлексии, функционал обычного режима отбрасывается
        Tests.Runner.Run();
#endif

#if REFLECTION
        // В режиме REFLECTION аналогично режиму TESTS
        Reflection.Print();
#endif

#if ISOLATION
        // В режиме изоляции работает только этот блок (режим для компиляции приложений)
        
#endif
    }
}


