using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Std;

/// <summary>
/// Атрибут для отметки класса, требующего рефлексивный анализ
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ReflectionAttribute : Attribute
{
    /// <summary>
    /// Флаги для рефлексивного поиска
    /// </summary>
    public BindingFlags Flags { get; private set; }

    /// <summary>
    /// Конструктор атрибута
    /// </summary>
    /// <param name="flags">Флаги для поиска полей, методов и конструкторов</param>
    public ReflectionAttribute(BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly) =>
        Flags = flags;
}

/// <summary>
/// Класс для вывода рефлексивной информации об отмеченных классах
/// </summary>
public static class Reflection
{
    // Визуальная иерархия информации
    private const string zeroLevel = "   ";
    private const string firstLevel = "   |  ";
    private const string secondLevel = "   |  |  ";
    private const string thirdLevel = "   |  |     ";

    // Разделитель информации о fields
    private const string separator = " | ";

    /// <summary>
    /// Основной метод для вывода информации из классов, отмеченных атрибутами
    /// </summary>
    public static void Print()
    {
        // Установка цвета вывода
        Console.ForegroundColor = ConsoleColor.DarkGreen;

        // Получение всех сборок решения
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        // Перебор всех сборок
        foreach (var assembly in assemblies)
        {
            // Фильтрация классов, не имеющих атрибут рефлексии
            var types = GetTypesWithAttribute(assembly);

            // Если в сборке таких классов нет, то продолжение перебора сборок
            if (types.Count() <= 0)
                continue;

            // Если такие классы есть, то вывод информации о них и о сборке, которая их содержит
            Console.WriteLine("\nСостав сборки \"" + assembly.GetName().Name + ".dll\":");
            Console.WriteLine(zeroLevel);

            // Информацию о типах, содержащих атрибут
            PrintTypesInfo(types);

            // Завершение блока информации о сборке
            Console.WriteLine("Конец сборки");
        }

        // Сброс цвета
        Console.ResetColor();

        // Локальная функция выбора типов, имеющих необходимый атрибут
        static Type[] GetTypesWithAttribute(Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(p => p.GetCustomAttribute(typeof(ReflectionAttribute)) != null)
                .ToArray<Type>();
        }

        // Локальная функция вывода информации о разных типах
        static void PrintTypesInfo(Type[] types)
        {
            foreach (var type in types)
            {
                var flags = GetAttributeFlags(type);
                PrintTypeInfo(type, flags);
            }  
        }

        // Локальная функция получения флагов из свойства атрибута
        static BindingFlags GetAttributeFlags(Type type)
        {
            ReflectionAttribute? statisticAtr = type.GetCustomAttribute(typeof(ReflectionAttribute)) as ReflectionAttribute;
            return statisticAtr!.Flags;
        }
    }

    // Вывод информации о классе
    private static void PrintTypeInfo(Type type, BindingFlags flags)
    {
        // Получение полей, методов и конструкторов типа
        var fields = type.GetFields(flags);
        var methods = type.GetMethods(flags);
        var constructors = type.GetConstructors(flags);

        // Подсчёт количества полей, методов и конструкторов
        int cntFields = fields.Count();
        int cntMethods = methods.Count();
        int cntConstructors = constructors.Count();

        // Вывод заголовка класса
        Console.WriteLine(zeroLevel + "Класс \"" + type.Name + "\":");
        Console.WriteLine(firstLevel);

        // Если в классе есть поля, вывод информации о них
        if (cntFields > 0)
            PrintFieldsInfo(type, flags, fields, cntFields);

        // Если в классе есть методы, вывод информации о них
        if (cntMethods > 0)
            PrintMethodsInfo(type, flags, methods, cntMethods);

        // Если в классе есть конструкторы, вывод информации о них
        if (cntConstructors > 0)
            PrintConstructorsInfo(type, flags, constructors, cntConstructors);

        // Завершение блока информации о классе
        Console.WriteLine(zeroLevel + "Конец класса");
        Console.WriteLine(zeroLevel);
    }

    // Вывод информации о полях класса
    private static void PrintFieldsInfo(Type type, BindingFlags flags, FieldInfo[] fields, int cntFields)
    {
        Console.WriteLine(firstLevel + "Поля:");
        Console.WriteLine(secondLevel);

        foreach (var field in fields)
        {
            var fieldInfo = new StringBuilder()
                .Append(secondLevel)
                .Append("Имя: ")
                .Append(field.Name.PadRight(20))
                .Append(separator)
                .Append("Тип: ")
                .Append(field.FieldType.ToString().PadRight(40))
                .Append(separator)
                .Append("Атрибуты: ")
                .Append(field.Attributes);
            Console.WriteLine(fieldInfo);
        }

        Console.WriteLine(secondLevel);
        Console.WriteLine(firstLevel + "Общее количество: " + cntFields);
        Console.WriteLine(firstLevel);
    }

    // Вывод информации о методах класса
    private static void PrintMethodsInfo(Type type, BindingFlags flags, MethodInfo[] methods, int cntMethods)
    {
        Console.WriteLine(firstLevel + "Методы:");
        Console.WriteLine(secondLevel);

        foreach (var method in methods)
        {
            Console.WriteLine(secondLevel + "Имя: " + method.Name);
            Console.WriteLine(thirdLevel + "Атрибуты: " + method.Attributes);
            Console.WriteLine(thirdLevel + "Возвращаемый тип: " + method.ReturnParameter);

            if (method.GetParameters().Count() > 0)
            {
                string[] paramsInfo = method
                    .GetParameters()
                    .Select(p => "(" + p.ParameterType + ") " + p.Name)
                    .ToArray<string>();

                Console.WriteLine(thirdLevel + "Параметры: " + string.Join(", ", paramsInfo));
            }

            Console.WriteLine(secondLevel);
        }

        Console.WriteLine(firstLevel + "Общее количество: " + cntMethods);
        Console.WriteLine(firstLevel);
    }

    // Вывод информации о конструкторах класса
    private static void PrintConstructorsInfo(Type type, BindingFlags flags, ConstructorInfo[] constructors, int cntConstructors)
    {
        Console.WriteLine(firstLevel + "Конструкторы:");
        Console.WriteLine(secondLevel);

        foreach (var constructor in constructors)
        {
            Console.WriteLine(secondLevel + "Имя: " + constructor.Name);
            Console.WriteLine(thirdLevel + "Атрибуты: " + constructor.Attributes);

            if (constructor.GetParameters().Count() > 0)
            {
                string[] paramsInfo = constructor
                    .GetParameters()
                    .Select(p => "(" + p.ParameterType + ") " + p.Name)
                    .ToArray<string>();

                Console.WriteLine(thirdLevel + "Параметры: " + string.Join(", ", paramsInfo));
            }

            Console.WriteLine(secondLevel);
        }

        Console.WriteLine(firstLevel + "Общее количество: " + cntConstructors);
        Console.WriteLine(firstLevel);
    }

    /// <summary>
    /// Метод для вывода рефлексивной информации о переданном в параметр типе
    /// </summary>
    /// <param name="obj">Тип для рефлексивного анализа</param>
    public static void Print(Type? type, BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly)
    {
        // Проверка параметра на null
        if (type == null)
            return;

        // Установка цвета вывода
        Console.ForegroundColor = ConsoleColor.DarkRed;

        // Получение сборки типа и её имени
        var assembly = Assembly.GetAssembly(type);
        var assemblyName = assembly?.GetName().Name ?? "Unknown";

        // Вывод заголовка сборки
        Console.WriteLine("\nСостав сборки \"" + assemblyName + ".dll\":");
        Console.WriteLine(zeroLevel);

        // Вывод информации о типе
        PrintTypeInfo(type, flags);

        // Завершение блока информации о сборке
        Console.WriteLine("Конец сборки");

        // Сброс цвета
        Console.ResetColor();
    }
}