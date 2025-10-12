using System;
using System.Text.RegularExpressions;

namespace ExtraLib;

/// <summary>
/// Класс для шаблонных преобразований текста с помощью регулярных выражений
/// </summary>
public static class Regexes
{
    /// <summary>
    /// Меняет все email-адреса из текста на указанное значение
    /// </summary>
    /// <param name="text">Исходный текст для поиска и замены email-адресов</param>
    /// <param name="replacesStr">Значение, на которое будет заменён найденный email</param>
    /// <returns>Текст с заменёнными адресами</returns>
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

    /// <summary>
    /// Парсит из текста все href-ссылки и их содержимое
    /// </summary>
    /// <param name="html">Исходный текст для поиска href-тегов и их содержимого</param>
    /// <returns>Коллекция найденных элементов MatchCollection? с группами "href" и "text"</returns>
    public static MatchCollection? HrefParse(string? html)
    {
        if (string.IsNullOrEmpty(html))
            return null;

        return Regex.Matches(html,
                             @"href\s?=\s?[""'](?<href>.+?)[""'].*?>(?<text>.+?)<\s?/\s?a\s?>",
                             RegexOptions.Compiled | RegexOptions.Multiline,
                             TimeSpan.FromMilliseconds(1000));
    }

    /// <summary>
    /// Очищает строку от тегов
    /// </summary>
    /// <param name="input">Исходный текст для очистки от тегов</param>
    /// <returns>Очищенный текст</returns>
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

    /// <summary>
    /// Достаёт из строки номер телефона в формате long<br/>
    /// Удобно использовать вместе с форматирование подобного вида: <c>$"{longNumber:+7 (###) ###-##-##}"</c>
    /// </summary>
    /// <param name="phone">Номер в строковом формате</param>
    /// <returns>Указанный номер формате числа long без первых "+7" или "8"</returns>
    public static long PhoneParse(string? phone)
    {
        if (string.IsNullOrEmpty(phone))
            return 0;

        string numbers = Regex.Replace(phone.Trim(),
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