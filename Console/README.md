# ExtraLib.ConsoleHelper
Библиотека классов, расширающих возможности написания консольных приложений.

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