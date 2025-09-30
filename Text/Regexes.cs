namespace Text
{
    using System.Text.RegularExpressions;

    public static class Regexes
    {
        // Меняет все email-адреса из текста на replaceStr
        public static string? EmailReplace(string text, string replacesStr)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            if (replacesStr == null)
                replacesStr = string.Empty;

            return Regex.Replace(text, @"\b\S+?@\w+?\.\w+?\b", replacesStr);
        }

        // Парсит из текста все href-ссылки и их содержимое, возвращает коллекцию найденных элементов
        public static MatchCollection? HrefParse(string? html)
        {
            if (string.IsNullOrEmpty(html))
                return null;

            return Regex.Matches(
                html,
                @"href\s?=\s?[""'](?<href>.+?)[""'].*?>(?<text>.+?)<\s?/\s?a\s?>",
                RegexOptions.Compiled | RegexOptions.IgnoreCase,
                TimeSpan.FromMilliseconds(100));
        }

        // Очищает строку от тегов
        public static string TagClear(string input) =>
            Regex.Replace(
                input,
                @"</?.+?>",
                "",
                RegexOptions.Compiled | RegexOptions.Singleline,
                TimeSpan.FromMilliseconds(100));

        // Достаёт из строки все номера телефонов в формате числа long (без первых "8" или "+7")
        public static long PhoneParse(string number)
        {
            if (string.IsNullOrEmpty(number))
                return 0;

            if (long.TryParse(Regex.Replace(number, @"^\+7|^8|\D", ""), out long result))
                return result;
            else
                return 0;
        }
    }
}
