public static class Program
{
    private const string programName = "*НАЗВАНИЕ ПРОГРАММЫ*";
    private const byte displayLength = 130;
    private const char displayChar = '-';

    public static void Main(string[] args)
    {
        Start(out Stopwatch timer);
        //Console.ReadKey();

        

        End(ref timer);
        //Console.ReadKey();
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

        Display.Print
        (
            Display.Type.End,
            displayLength,
            displayChar,
            "Программа завершена"
#if DEBUG
            , $"Время выполнения: {timer.ElapsedMilliseconds} мс. или {timer.ElapsedMilliseconds / 1000} сек."
#endif
        );
    }
}

#if DEBUG
//[DebuggerDisplay("")]
#endif


