public static class Program
{
    private const string programName = "*НАЗВАНИЕ ПРОГРАММЫ*";
    private const byte displayLength = 130;
    private const char displayChar = '-';

    public static void Main(string[] args)
    {
        Start(out Stopwatch timer);
#if RELEASE
        Console.Write("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
        Console.Write('\r' + new string(' ', displayLength) + '\r');
#endif



        End(ref timer);
#if RELEASE
        Console.ReadKey();
#endif
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
    }

    private static void End(ref Stopwatch timer)
    {
        timer.Stop();
        (long mByteAlloc, long kByteAlloc, long byteAlloc) = GetTotalAlloc();

        Display.Print
        (
            Display.Type.End,
            displayLength,
            displayChar,
            "Программа завершена"
#if DEBUG
            , $"Время выполнения: {timer.ElapsedMilliseconds} мс. или {timer.ElapsedMilliseconds / 1000} сек."
            , $"Использовано памяти: {mByteAlloc} МБ. {kByteAlloc} КБ. {byteAlloc} Б."

        );

        Reflection.Print();
#else
        );
#endif

        static (long mByteAlloc, long kByteAlloc, long byteAlloc) GetTotalAlloc()
        {
            long totalBytesAlloc = GC.GetTotalAllocatedBytes();

            long mByteAlloc = Math.DivRem(totalBytesAlloc, (long)Math.Pow(2, 20), out totalBytesAlloc);
            long kByteAlloc = Math.DivRem(totalBytesAlloc, (long)Math.Pow(2, 10), out totalBytesAlloc);

            return (mByteAlloc, kByteAlloc, totalBytesAlloc);
        }
    }
}


