using System.Linq;

namespace Text;

/// <summary>
/// Строковый кодировщик
/// </summary>
public static class StringCrypter
{
    /// <summary>
    /// Тип кодирования:
    /// <list type="bullet">
    ///    <item>
    ///        <term>Hex</term>
    ///        <description>Кодирует обычную строку в HEX-формат с указанным смещением.</description>
    ///    </item>
    ///    <item>
    ///        <term>Std</term>
    ///        <description>Кодирует строку при помощи обычного смещения.</description>
    ///    </item>
    /// </list>
    /// </summary>
    public enum Type { Hex, Std }

    /// <summary>
    /// Метод для кодирования строк
    /// </summary>
    /// <param name="key">Ключ для смещения шифровки</param>
    /// <param name="cryptType">Тип кодирования: указывается перечислением StringCrypter.Type</param>
    /// <returns>Закодированная строка</returns>
    public static string Crypt(this string input, int key, Type cryptType = Type.Std)
    {
        if (string.IsNullOrEmpty(input))
            return "";

        if(cryptType == Type.Hex)
        {
            var cryptedCharsHex = input
                    .Select(p => $"{p + key:X}")
                    .ToArray<string>();
            return string.Join(' ', cryptedCharsHex);
        }

        return ShiftChars(input, key);
    }

    /// <summary>
    /// Метод для декодирования строк
    /// </summary>
    /// <param name="key">Ключ для смещения шифровки: необходимо указать противоположное значение от кодирующего</param>
    /// <param name="cryptType">Тип кодирования: указывается перечислением StringCrypter.Type</param>
    /// <returns>Декодированная строка</returns>
    public static string Decrypt(this string input, int key, Type cryptType = Type.Std)
    {
        if (string.IsNullOrEmpty(input))
            return "";

        if (cryptType == Type.Hex)
        {
            var decryptedCharsHex = input
                    .Split(' ')
                    .Select(p => (char)(int.Parse(p, System.Globalization.NumberStyles.HexNumber) + key))
                    .ToArray<char>();
            return new string(decryptedCharsHex);
        }

        return ShiftChars(input, key);
    }

    private static string ShiftChars(string input, int key)
    {
        var shiftedChars = input
                    .Select(p => (char)(p + key))
                    .ToArray<char>();
        return new string(shiftedChars);
    }
}