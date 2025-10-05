# ExtraLib.Std
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

## ConsoleHelper
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

## Counter
### Назначение:
Класс для создания счетчиков. Также есть статический метод возвращающий замкнутую функцию с заданным начальным значением.

### Структура:
В конструктор передаётся начальное значение счётчика и необязательным параметром его имя (по умолчанию стоит "Default").

Методы и свойства:
 - void Increment() - Инкрементирует значение.
 - int Value - Возвращает значение.
 - string Name - Возвращает название.

Статические методы:
 - Func\<int> Create(Int32) - Принимает начальное значение и возвращает функцию счётчика.

Дополнительная информация:
У класса переопределена группа методов Object: ToString, Equals и GetHashCode. Сравнение и получение хеш-кода осуществляется на основе значений счётчика и его имени.\
Также у данного класса определены следующие операторы: ==, !=, ++.

### Примеры кода:
Использование через экземпляр Counter:
```C#
var cnt = new Counter(100);
for (int i = 0; i < 10; i++)
    cnt++;

Console.WriteLine(cnt);

// Вывод:
// Default: 110
```

Использование через замкнутую функцию:
```C#
var cnt = Counter.Create(100);
for (int i = 0; i < 3; i++)
    Console.WriteLine($"Счётчик: {cnt()}");

// Вывод:
// Счётчик: 100
// Счётчик: 101
// Счётчик: 102
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
## NumHelper
### Назначение:
Статический класс с методами расширения для работы с числами.

### Структура:
Методы расширения:
 - InWord - Преобразует число из диапазона от 0 до 100 (пока что) в строковое представление (именительный падеж).
 - GetEnumerator - Возвращает объект IEnumerator для перебора числа в цикле foreach.

### Примеры кода:
InWord
```C#
Console.WriteLine(25.InWord());
// двадцать пять
```

GetEnumerator
```C#
foreach (int num in -4)
    Console.WriteLine(num);

// Вывод
// -4
// -3
// -2
// -1
// 0
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

## Reflection
### Назначение:
Статический класс, предназначенный для исследования пользовательских типов.

### Структура:
Атрибуты:
 - ReflectionAttribute(BindingFlags(optional)) - Атрибут для отметки класса о необходимости осуществления его рефлексивного анализа методом Reflection.Print();

Статические методы:
 - Print() - Выводит информацию о всех классах, имеющих атрибут ReflectionAttribute;
 - Print(Type, BindingFlags(optional)) - Выводит рефлексивную информацию о переданном в параметр типе согласно указанным флагам (не отображает информацию о классах, имеющих атрибуты). 
 - Print(String, BindingFlags(optional)) - Выводит рефлексивную информацию о сборке, путь которой передан в параметр метода.

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

Использование без указания атрибута (поисследуем класс String):
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

Использование через указание пути сборки:
```C#
                              ↓ - экранизация знака "\"
string path = "*Какой-то путь*\\*Название сборки*.dll";
    Reflection.Print(path);
```