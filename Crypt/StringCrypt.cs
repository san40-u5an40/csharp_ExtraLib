namespace Crypt
{
    using System.Linq;

    public static class StringCrypter
    {
        public enum Type { Hex, Std }

        public static string Crypt(this string input, int key, Type cryptType = Type.Std)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            switch (cryptType)
            {
                case Type.Hex:
                    var cryptedCharsHex = input
                        .Select(p => $"{p + key:X}")
                        .ToArray<string>();
                    return string.Join(' ', cryptedCharsHex);

                default:
                    return ShiftChars(input, key);
            }
        }

        public static string Decrypt(this string input, int key, Type cryptType = Type.Std)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            switch (cryptType)
            {
                case Type.Hex:
                    var decryptedCharsHex = input
                        .Split(' ')
                        .Select(p => (char)(int.Parse(p, System.Globalization.NumberStyles.HexNumber) + key))
                        .ToArray<char>();
                    return new string(decryptedCharsHex);

                default:
                    return ShiftChars(input, key);
            }
        }

        private static string ShiftChars(string input, int key)
        {
            var shiftedChars = input
                        .Select(p => (char)(p + key))
                        .ToArray<char>();
            return new string(shiftedChars);
        }
    }
}