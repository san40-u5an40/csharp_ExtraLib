# ExtraLib
В этом репозитории представлена библиотека классов, которая была сформирована в ходе моего обучения разработке на .NET/C#. Какие-то классы могут быть использованы в широком круге задач, какие-то специфичны для консольной разработки или же конкретной задачи приложения.

## Примеры кода с их участием:
```C#
Array.Sort(array, Comparator.GetComparator<User, string>(p => p.Name));
// Сортировка массива объектов "User" по имени
```

```C#
var GitHub = "77 65 72 38 34 31 79 39 65 72 38 34 24 3E 2D".Decrypt(-4, StringCrypter.Type.Hex);
Console.WriteLine(GitHub);
// Дешифрует с помощью сдвига набор символов, представленный в HEX-формате

// Вывод:
// san40-u5an40 :)
```

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
    long numbers = Regexes.PhoneParse(phone);            // Парсит все номера в число long, убирая первые "+7" или "8"
    Console.WriteLine($"{numbers:+7 (###) ###-##-##}");  // Все в одном формате
}

// Вывод:
// +7 (800) 555-35-35
// +7 (800) 555-35-35
// +7 (800) 555-35-35
// +7 (800) 555-35-35
// +7 (800) 555-35-35
```

```C#
Display.Print(Display.Type.Center, 40, '-', "Первая фраза", "Вторая фраза");

// Вывод:
// /*----------------------------------------*/
// /*--------------Первая фраза--------------*/
// /*--------------Вторая фраза--------------*/
// /*----------------------------------------*/
```

## Оглавление:
Полезны в широком круге задач:
- [Regexes]()
- [Bytes]()
- [Comparator]()
- [TimerHelper]()

Полезны при консольной разработке:
- [ConsoleExtension]()
- [Display]()

Для специфичных задач:
- [StringCrypt]()
- [Reflection]()

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

## Bytes
### Назначение:
Статический класс для конвертировании байтов.

### Структура:
Статические методы:
 - ToSize(Long) - Переводит полученное количество байт в набор гигабайт, мегабайт, килобайт и байт, представленный структурой Size.
 - ToGb(Long) - Переводит байты в гигабайты с математическим округлением.
 - ToMb(Long) - Переводит байты в мегабайты с математическим округлением.
 - ToKb(Long) - Переводит байты в килобайты с математическим округлением.

Свойства структуры Size:
 - long GByte - Количество гигабайт.
 - long MByte - Количество мегабайт.
 - long KByte - Количество килобайт.
 - long Byte - Количество оставшихся байт.

### Примеры кода:
```C#
var drives = DriveInfo.GetDrives();

foreach (var drive in drives)
{
    var str = new StringBuilder()
        .AppendLine("Имя диска: " + drive.Name)
        .AppendLine("Метка диска: " + drive.VolumeLabel)
        .AppendLine("Общий размер: " + Bytes.ToSize(drive.TotalSize));

    Console.WriteLine(str);
}

// Вывод:
// 
// Имя диска: C:\
// Метка диска:
// Общий размер: 100 Гбайт, 207 Мбайт, 1008 Кбайт 0 байт
// 
// Имя диска: D:\
// Метка диска: Data
// Общий размер: 171 Гбайт, 115 Мбайт, 1020 Кбайт 0 байт
```

## Comparator
### Назначение:
Статический класс, который возвращает объект IComparer позволяющий сравнивать пользовательские типы по указанному параметру.

### Структура:
Статические методы:
 - GetComparator\<TSource, TKey> - Возвращает созданный объект для сравнения по переданной лямбде. Первым Generic-параметром указывается элемент коллекции, вторым возвращаемый тип данных.

### Примеры кода:
```C#
Array.Sort(array, Comparator.GetComparator<User, string>(p => p.Name));
// Сортировка массива объектов "User" по имени
// string - т.к. этим типом представлено свойство Name
```

## TimerHelper
### Назначение:
Статический класс предназначенный для использования экспоненциальных таймеров.

### Структура:
Статические методы:
 - ExpWait - Осуществляет ожидание в зависимости от экспоненциального значения таймера, рассчитанного на основе указанной итерации цикла.

### Примеры кода:
Без необязательных параметров:
```C#
for(int i = 0; i < int.MaxValue; i++)
{
    Console.Write($"\rИтерация: {i}");
    TimerHelper.ExpWait(i);
    // В зависимости от итерации блокирует поток на разное количество времени
}
```

С указанием начального значения таймера и шага:
```C#
for(int i = 0; i < int.MaxValue; i++)
{
    Console.Write($"\rИтерация: {i}");
    TimerHelper.ExpWait(i, 6.5, 0.1);
}
```

## ConsoleExtension
### Назначение:
Статический класс с дополнительными методами для консольного ввода-вывода.

### Структура:
Статические методы:
 - TryReadLine - Работает также как и схожие методы: записывает в out-параметр результат чтения из консоли, возвращая содержит ли этот результат символы.
 - WriteColor - Выводит в консоль текст указанного цвета.

### Примеры кода:
TryReadLine
```C#
while (!ConsoleHelper.TryReadLine(out string? input))
	Console.WriteLine("Некорректный ввод. Повторите попытку.");
```

WriteColor
```C#
internal static void PrintWelcome(string name)
{
    Console.Write("Добро пожаловать в программу, ");
    ConsoleHelper.WriteColor(name, ConsoleColor.Red);
    Console.Write("!\n");
}
```

## Display
### Назначение:
Статический класс для настройки консольного окна, а также вывода дисплея. Содержит перечисление `Type` для указания в параметре типа дисплея.

### Структура:
Статические методы:
 - WindowSetup - Устанавливает заголовок консоли, её ширину, убирает видимость курсора и устанавливает кодировку UTF-16.
 - Print - Выводит дисплей в консоль, принимает в качестве параметров: тип дисплея, его длину, символ дисплея и сообщения для отображения в нём.

Типы дисплея (из enum Type):
 - Start - После плашки дисплея добавляется `\n`.
 - End - `\n` добавляется перед дисплеем.
 - Center - `\n` добавляется как до, так и после дисплея.

### Примеры кода:
WindowSetup
```C#
Display.WindowSetup(displayLength: 100, displaySpace: 5, programName: "Программка");
// Установит ширину консоли на 105 (displayLength + displaySpace)
// Установит заголовок окна: Программка
// Отключит видимость курсора
// Установит кодировку UTF-16
```

Print
```C#
Display.Print(Display.Type.Center, 40, '-', "Первая фраза", "Вторая фраза");

// Вывод:
// /*----------------------------------------*/
// /*--------------Первая фраза--------------*/
// /*--------------Вторая фраза--------------*/
// /*----------------------------------------*/
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

## Reflection
### Назначение:
Статический класс, предназначенный для исследования пользовательских типов.

### Структура:
Атрибуты:
 - ReflectionAttribute(BindingFlags(optional)) - Атрибут для отметки класса о необходимости осуществления его рефлексивного анализа методом Reflection.Print();

Статические методы:
 - Print() - Выводит информацию о всех пользовательских типах, имеющих атрибут ReflectionAttribute (только этот метод связан с использованием атрибута);
 - Print(Type, BindingFlags(optional)) - Выводит рефлексивную информацию о переданном в параметр типе согласно указанным флагам.
 - Print(Assembly, BindingFlags(optional)) - Выводит рефлексивную информацию о переданной в параметр сборке.
 - Print(String, BindingFlags(optional)) - Выводит рефлексивную информацию о сборке, путь которой передан в параметр метода.
 - Print(String, String, BindingFlags(optional)) - Выводит рефлексивную информацию о сборке, чей путь указан, и содержащимся в ней типе, название которого указано вторым параметром.

### Примеры кода:
Использование через атрибут:
```C#
// Main:
Reflection.Print();

// Тестовый класс для анализа
[Reflection]
public class ClassTest
{
    private static int count = 0;
    private string? name;
    private Func<int>? getNumber;

    public string? Name => name;

    public ClassTest(string name) { }

    public ClassTest() { }

    public event Func<int> GetNumber
    {
        add
        {
            if (value == null)
                return;

            getNumber += value;
        }
        remove
        {
            if (value == null || getNumber == null)
                return;

            if (!getNumber.GetInvocationList().Contains(value))
                return;

            getNumber -= value;
        }
    }

    internal void Print(string mess, string mess2) {  }
}

// Вывод:
// Состав сборки "SandBox.dll":
//
//   Класс "ClassTest":
//   |
//   |  Поля:
//   |  |
//   |  |  Имя: name                 | Тип: System.String                            | Атрибуты: Private
//   |  |  Имя: getNumber            | Тип: System.Func`1[System.Int32]              | Атрибуты: Private
//   |  |  Имя: count                | Тип: System.Int32                             | Атрибуты: Private, Static
//   |  |
//   |  Общее количество: 3
//   |
//   |  Методы:
//   |  |
//   |  |  Имя: get_Name
//   |  |     Атрибуты: Public, HideBySig, SpecialName
//   |  |     Возвращаемый тип: System.String
//   |  |
//   |  |  Имя: add_GetNumber
//   |  |     Атрибуты: Public, HideBySig, SpecialName
//   |  |     Возвращаемый тип: Void
//   |  |     Параметры: (System.Func`1[System.Int32]) value
//   |  |
//   |  |  Имя: remove_GetNumber
//   |  |     Атрибуты: Public, HideBySig, SpecialName
//   |  |     Возвращаемый тип: Void
//   |  |     Параметры: (System.Func`1[System.Int32]) value
//   |  |
//   |  |  Имя: Print
//   |  |     Атрибуты: Assembly, HideBySig
//   |  |     Возвращаемый тип: Void
//   |  |     Параметры: (System.String) mess, (System.String) mess2
//   |  |
//   |  Общее количество: 4
//   |
//   |  Конструкторы:
//   |  |
//   |  |  Имя: .ctor
//   |  |     Атрибуты: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     Параметры: (System.String) name
//   |  |
//   |  |  Имя: .ctor
//   |  |     Атрибуты: Public, HideBySig, SpecialName, RTSpecialName
//   |  |
//   |  Общее количество: 2
//   |
//   Конец класса
//
// Конец сборки
```

Использование по ссылке на тип (поисследуем класс String):
```C#
// Main:
Reflection.Print(typeof(System.String));

// Вывод:
// Состав сборки "System.Private.CoreLib.dll":
//
//   Класс "String":
//   |
//   |  Поля:
//   |  |
//   |  |  Имя: _stringLength        | Тип: System.Int32                             | Атрибуты: Private, InitOnly, NotSerialized
//   |  |  Имя: _firstChar           | Тип: System.Char                              | Атрибуты: Private, NotSerialized
//   |  |  Имя: Empty                | Тип: System.String                            | Атрибуты: Public, Static, InitOnly
//   |  |
//   |  Общее количество: 3
//   |
//   |  Методы:
//   |  |
//   |  |  Имя: FastAllocateString
//   |  |     Атрибуты: Assembly, Static, HideBySig
//   |  |     Возвращаемый тип: System.String
//   |  |     Параметры: (System.Int32) length
//   |  |

// Тут очень много методов, можете при желании поисследовать сами

//   |  |
//   |  |  Имя: LastIndexOf
//   |  |     Атрибуты: Public, HideBySig
//   |  |     Возвращаемый тип: Int32
//   |  |     Параметры: (System.String) value, (System.Int32) startIndex, (System.Int32) count, (System.StringComparison) comparisonType
//   |  |
//   |  Общее количество: 267
//   |
//   |  Конструкторы:
//   |  |
//   |  |  Имя: .ctor
//   |  |     Атрибуты: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     Параметры: (System.Char[]) value
//   |  |
//   |  |  Имя: .ctor
//   |  |     Атрибуты: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     Параметры: (System.Char[]) value, (System.Int32) startIndex, (System.Int32) length
//   |  |
//   |  |  Имя: .ctor
//   |  |     Атрибуты: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     Параметры: (System.Char*) value
//   |  |
//   |  |  Имя: .ctor
//   |  |     Атрибуты: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     Параметры: (System.Char*) value, (System.Int32) startIndex, (System.Int32) length
//   |  |
//   |  |  Имя: .ctor
//   |  |     Атрибуты: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     Параметры: (System.SByte*) value
//   |  |
//   |  |  Имя: .ctor
//   |  |     Атрибуты: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     Параметры: (System.SByte*) value, (System.Int32) startIndex, (System.Int32) length
//   |  |
//   |  |  Имя: .ctor
//   |  |     Атрибуты: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     Параметры: (System.SByte*) value, (System.Int32) startIndex, (System.Int32) length, (System.Text.Encoding) enc
//   |  |
//   |  |  Имя: .ctor
//   |  |     Атрибуты: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     Параметры: (System.Char) c, (System.Int32) count
//   |  |
//   |  |  Имя: .ctor
//   |  |     Атрибуты: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     Параметры: (System.ReadOnlySpan`1[System.Char]) value
//   |  |
//   |  Общее количество: 9
//   |
//   Конец класса
//
// Конец сборки
```

Использование по ссылке на сборку:
```C#
Reflection.Print(Assembly.GetAssembly(typeof(Regex)));
// Выведет все классы, входящие в состав "System.Text.RegularExpressions.dll"
```

Использование через указание пути сборки:
```C#
                         ↓ - экранизация знака "\"
string pathAssembly = "D:\\csharp\\projects\\ExtraLib\\bin\\Debug\\net9.0\\Text.dll";
Reflection.Print(pathAssembly);
```

Использование через указание пути сборки и названия класса:
```C#
                         ↓ - экранизация знака "\"
string pathAssembly = "D:\\csharp\\projects\\ExtraLib\\bin\\Debug\\net9.0\\Std.dll";

Reflection.Print(pathAssembly, "Comparator");
// Или по полному названию
Reflection.Print(pathAssembly, "Std.Comparator");

// Если полученную сборку не удастся открыть, будет выведено сообщение из Exception
// Could not load file or assembly 'D:\csharp\projects\ExtraLib\bin\Debug\net9.0\Std.dll'. Системе не удается найти указанный путь.
```