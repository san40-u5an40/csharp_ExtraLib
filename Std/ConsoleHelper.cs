namespace Std
{
    using System;

    public static class ConsoleHelper
    {
        public static bool TryReadLine(out string? input)
        {
            string? temp = Console.ReadLine();
            if (string.IsNullOrEmpty(temp))
            {
                input = null;
                return false;
            }
            else
            {
                input = temp;
                return true;
            }
        }

        public static void WriteColor(string? input, ConsoleColor color)
        {
            if (string.IsNullOrEmpty(input))
                return;

            Console.ForegroundColor = color;
            Console.Write(input);
            Console.ResetColor();
        }
    }
}