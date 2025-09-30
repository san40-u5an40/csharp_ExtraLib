namespace Text
{
    using System.Text.RegularExpressions;

    public static class Regexes
    {
        // Меняет все email-адреса из текста на replaceStr
        public static string EmailReplace(string? text, string? replacesStr)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (replacesStr == null)
                replacesStr = string.Empty;

            return Regex.Replace(text,
                                 @"\b\S+?@\w+?\.\w+?\b",
                                 replacesStr,
                                 RegexOptions.Compiled,
                                 TimeSpan.FromMilliseconds(100));
        }

        // Парсит из текста все href-ссылки и их содержимое, возвращает коллекцию найденных элементов
        public static MatchCollection? HrefParse(string? html)
        {
            if (string.IsNullOrEmpty(html))
                return null;

            return Regex.Matches(html,
                                 @"href\s?=\s?[""'](?<href>.+?)[""'].*?>(?<text>.+?)<\s?/\s?a\s?>",
                                 RegexOptions.Compiled | RegexOptions.Multiline,
                                 TimeSpan.FromMilliseconds(1000));
        }

        // Очищает строку от тегов
        public static string TagClear(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return Regex.Replace(input,
                                 @"</?.+?>",
                                 string.Empty,
                                 RegexOptions.Compiled,
                                 TimeSpan.FromMilliseconds(100));
        }

        // Достаёт из строки все номера телефонов в формате числа long (без первых "8" или "+7")
        public static long PhoneParse(string? number)
        {
            if (string.IsNullOrEmpty(number))
                return 0;

            string numbers = Regex.Replace(number,
                                           @"^\+?\s?7|^8|\D",
                                           string.Empty,
                                           RegexOptions.Compiled,
                                           TimeSpan.FromMilliseconds(100));

            if (long.TryParse(numbers, out long result))
                return result;
            else
                return 0;
        }
    }
}
