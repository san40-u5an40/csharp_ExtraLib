//#define TESTS
//#define REFLECTION

public static class Program
{
    private const string programName = "*НАЗВАНИЕ ПРОГРАММЫ*";
    private const byte displayLength = 130;
    private const char displayChar = '-';

    internal static void Sandbox(string[] args)
    {
        Start(out Stopwatch timer);



        End(ref timer);
    }



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

    private static void End(ref Stopwatch timer)
    {
        timer.Stop();

#if DEBUG
        // В режиме DEBUG удобно иметь информацию об аллокациях

        (long mByteAlloc, long kByteAlloc, long byteAlloc) = GetTotalAlloc();

        static (long mByteAlloc, long kByteAlloc, long byteAlloc) GetTotalAlloc()
        {
            long totalBytesAlloc = GC.GetTotalAllocatedBytes();

            long mByteAlloc = Math.DivRem(totalBytesAlloc, (long)Math.Pow(2, 20), out totalBytesAlloc);
            long kByteAlloc = Math.DivRem(totalBytesAlloc, (long)Math.Pow(2, 10), out totalBytesAlloc);

            return (mByteAlloc, kByteAlloc, totalBytesAlloc);
        }
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
            , $"Использовано памяти: {mByteAlloc} МБ. {kByteAlloc} КБ. {byteAlloc} Б."
#endif
        );

#if RELEASE
        // Завершать программу в RELEASE тоже удобно по нажатию клавиши
        Console.ReadKey();
#endif
    }

    public static void Main(string[] args)
    {
// Обычный режим функционирования программы
#if !TESTS && !REFLECTION
        Sandbox(args);
#endif

// В режиме TESTS реализует только запуск теста, функционал обычного режима отбрасывается
#if TESTS && RELEASE
        Tests.Runner.Run();
#endif

// В режиме REFLECTION аналогично режиму TESTS
#if REFLECTION
        Reflection.Print();
#endif
    }
}


