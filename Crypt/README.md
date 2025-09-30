# ExtraLib.Crypt
### Назначение:
Кодирование и декодирование текста при помощи сдвигового шифрования, с поддержкой переформатирования текста в HEX.

### Структура:
Методы расширения из класса StringCrypter:
 - Crypt - кодирует текст
 - Decrypt - декодирует текст

Первым параметром указывается числовой ключ для сдвига. Вторым необязательным параметром указывается формат кодирования, представленный перечислением StringCrypter.Type:
 - Hex - кодирует текст в HEX, и декодирует HEX-формат в обычный текст.
 - Std - кодирует строку только с помощью сдвиговой шифрации.

По умолчанию во втором параметре указан тип: Std.

### Примеры кода:
Со стандартным форматированием:
```C#
string text = "Simple string";

string crypted = text.Crypt(-10);
// I_cfb[▬ijh_d]

string decrypted = crypted.Decrypt(10);
// Simple string
```

С форматированием в HEX:
```C#
string text = "Simple string";

string crypted = text.Crypt(-10, StringCrypter.Type.Hex);
// 49 5F 63 66 62 5B 16 69 6A 68 5F 64 5D

string decrypted = crypted.Decrypt(10, StringCrypter.Type.Hex);
// Simple string
```