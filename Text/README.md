# ExtraLib.Text
## Regexes
### Назначение:
Статический класс с возможностью различного преобразования текста при помощи регулярных выражений.

### Структура:
Статические методы:
 - EmailReplace - Заменяет все email-адреса из текста на указанное выражение.
 - HrefParse - Ищет в тексте все ссылки из атрибута `href` и текст этих ссылок. Возвращает коллекцию MatchCollection с группами `href` и `text`.
 - TagClear - Удаляет из текста найденные теги.
 - PhoneParse - Извлекает из строки с указанным номером число в формате `long`, также удаляя начальные "+7" или "8".

Все методы используют регулярные выражения с флагом `RegexOptions.Compiled`, что замедляет сборку, но увеличивает скорость работы приложения.

### Примеры кода:
EmailReplace
```C#
string text = "По всем вопросам обращайтесь на почту: alexandr.dev2011@gmail.com";

string newText = Regexes.EmailReplace(text, "{email}");
// По всем вопросам обращайтесь на почту: {email}
```

HrefParse
```C#
string html = ...;

foreach (Match match in Regexes.HrefParse(html))
	Console.WriteLine($"Href: {match.Groups["href"].Value, -30} Text: {match.Groups["text"].Value}");  

// Пример вывода:
// Href: /science/seminar/inclusion     Text: Инклюзия
// Href: /science/seminar/security      Text: Безопасность
```

TagClear
```C#
string html = ...;
string newHtml = Regexes.TagClear(html);

// Пример вывода:
// Инклюзия Безопасность
```

PhoneParse
```C#
var phones = new string[]
{
    "88005553535",
    "+78005553535",
    "+7 (800) 555-35-35",
    "7 800 555 35 35",
    "8 (800) 555 35-35"
};

foreach(var phone in phones)
{
    long numbers = Regexes.PhoneParse(phone);
    Console.WriteLine($"{numbers:+7 (###) ###-##-##}");  // Все в одном формате
}

// Вывод:
// +7 (800) 555 - 35 - 35
// +7 (800) 555 - 35 - 35
// +7 (800) 555 - 35 - 35
// +7 (800) 555 - 35 - 35
// +7 (800) 555 - 35 - 35
```

## StringCrypt
### Назначение:
Статический класс для кодирования и декодирования текста при помощи сдвигового шифрования, с поддержкой переформатирования текста в HEX.

### Структура:
Методы расширения:
 - Crypt - Кодирует текст.
 - Decrypt - Декодирует текст.

Первым параметром указывается числовой ключ для сдвига. Вторым необязательным параметром указывается формат кодирования, представленный перечислением StringCrypter.Type:
 - Hex - Кодирует текст в HEX, и декодирует HEX-формат в обычный текст.
 - Std - Кодирует строку только с помощью сдвиговой шифрации.

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