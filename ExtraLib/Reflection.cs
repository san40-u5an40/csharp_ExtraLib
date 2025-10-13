using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ExtraLib;

/// <summary>
/// Атрибут для отметки пользовательского типа, требующего рефлексивный анализ
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
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
/// Класс для вывода рефлексивной информации об отмеченных пользовательского типа
/// </summary>
public static class Reflection
{
    // Визуальная иерархия информации
    private const string zeroLevel = "|   ";
    private const string firstLevel = "|   |  ";
    private const string secondLevel = "|   |  |  ";
    private const string thirdLevel = "|   |  |     ";

    // Разделитель информации о fields
    private const string separator = " | ";

    // Вывод информации о полях пользовательского типа
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

    // Вывод информации о методах пользовательского типа
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

    // Вывод информации о конструкторах пользовательского типа
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

    // Вывод информации об одном пользовательском типе
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

        // Вывод заголовка пользовательского типа
        Console.WriteLine(zeroLevel + "Тип: \"" + type.Name + "\":");
        Console.WriteLine(firstLevel);

        // Если в пользовательском типе есть поля, вывод информации о них
        if (cntFields > 0)
            PrintFieldsInfo(type, flags, fields, cntFields);

        // Если в пользовательском типе есть методы, вывод информации о них
        if (cntMethods > 0)
            PrintMethodsInfo(type, flags, methods, cntMethods);

        // Если в пользовательском типе есть конструкторы, вывод информации о них
        if (cntConstructors > 0)
            PrintConstructorsInfo(type, flags, constructors, cntConstructors);

        // Завершение блока информации о пользовательском типе
        Console.WriteLine(zeroLevel + "Конец типа");
        Console.WriteLine(zeroLevel);
    }

    /// <summary>
    /// Выводит рефлексивную информацию о всех пользовательских типах, отмеченных атрибутом <c>[Reflection]</c>, в данном решении
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
            // Фильтрация пользовательских типов, не имеющих атрибут рефлексии
            var types = GetTypesWithAttribute(assembly);

            // Если в сборке таких пользовательских типов нет, то продолжение перебора сборок
            if (types.Count() <= 0)
                continue;

            // Если такие пользовательские типы есть, то вывод информации о них и о сборке, которая их содержит
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

    // Вывод информации о сборке и её пользовательск(ом/их) тип(е/ах)
    private static void PrintInfo(Type? type, Assembly? assembly, BindingFlags flags)
    {
        // Определение типа выводимой информации
        var typePrintInfo = (type, assembly) switch
        {
            (null, null) => TypePrintInfo.Zero,
            (not null, null) => TypePrintInfo.OnlyType,
            (null, not null) => TypePrintInfo.OnlyAssembly,
            (not null, not null) => TypePrintInfo.FullInfo
        };

        // Проверка на null
        if (typePrintInfo == TypePrintInfo.Zero)
            return;

        // Получение сборки по типу (если она не была указана)
        if (typePrintInfo == TypePrintInfo.OnlyType)
            assembly = type!.Assembly;
        
        // Получение имени сборки
        string? assemblyName = assembly!.GetName()!.Name;

        // Установка цвета вывода
        Console.ForegroundColor = ConsoleColor.DarkGreen;

        // Вывод заголовка сборки
        Console.WriteLine("\nСостав сборки \"" + assemblyName + ".dll\":");
        Console.WriteLine(zeroLevel);

        // Вывод информации об одном типе (либо если он указан вместе со сборкой, либо если он указан один)
        if (typePrintInfo == TypePrintInfo.OnlyType || typePrintInfo == TypePrintInfo.FullInfo)
            PrintTypeInfo(type!, flags);
        // Если же указана только сборка, то вывод информации о всех её типах
        else
        {
            foreach (var type_ in assembly!.GetTypes())
                PrintTypeInfo(type_!, flags);
        }

        // Завершение блока информации о сборке и её тип(е/ах)
        Console.WriteLine("Конец сборки");

        // Сброс цвета
        Console.ResetColor();
    }

    // Перечисление для определения типа вывода
    private enum TypePrintInfo
    {
        Zero = 0,
        OnlyType = 1,
        OnlyAssembly = 2,
        FullInfo = 3
    }

    /// <summary>
    /// Выводит рефлексивную информацию о переданном в параметр типе
    /// </summary>
    /// <param name="type">Тип для рефлексивного анализа</param>
    /// /// <param name="flags">Флаги получения полей, методов и конструкторов</param>
    public static void Print(Type? type, BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly)
    {
        // Проверка параметра на null
        if (type == null)
            return;

        // Вывод информации об одном типе
        PrintInfo(type, null, flags);
    }

    /// <summary>
    /// Выводит рефлексивную информацию о всех типах внутри переданной сборки
    /// </summary>
    /// <param name="assembly">Ссылка на сборку</param>
    /// <param name="flags">Флаги получения полей, методов и конструкторов</param>
    public static void Print(Assembly? assembly, BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly)
    {
        // Проверка на наличие сборки для анализа
        if (assembly == null)
            return;

        // Вывод информации о всех типах в сборке
        PrintInfo(null, assembly, flags);
    }

    // Попытка получить тип по его названию из указанной сборки
    private static bool TryGetTypeFromAssembly(Assembly assembly, string typeName, out Type? type)
    {
        // Попытка получить тип из сборки не меняя имя
        if ((type = assembly.GetType(typeName, false)) == null)
        {
            // Если у typeName отсутствует namespace, добавляем вручную и пробуем снова получить тип по имени
            typeName = assembly.GetName().Name + '.' + typeName;
            if ((type = assembly.GetType(typeName, false)) == null)
                return false;
        }

        // При успехе
        return true;
    }
    
    // Попытка открыть сборку по указанному пути
    private static bool TryOpenAssembly(string path, out string? errorInfo, out Assembly? assembly)
    {
        try
        {
            // Попытка открытия сборки
            assembly = Assembly.LoadFrom(path);

            // Если сборка открылась корректно
            errorInfo = null;
            return true;
        }
        catch (BadImageFormatException ex)
        {
            (assembly, errorInfo) = (null, ex.Message);
        }
        catch (System.IO.FileNotFoundException ex)
        {
            (assembly, errorInfo) = (null, ex.Message);
        }
        catch (System.IO.PathTooLongException ex)
        {
            (assembly, errorInfo) = (null, ex.Message);
        }
        catch (System.Security.SecurityException ex)
        {
            (assembly, errorInfo) = (null, ex.Message);
        }
        // Неизвестная ошибка
        catch (Exception ex)
        {
            var report = new StringBuilder()
                .AppendLine(ex.Message)
                .AppendLine("Источник: " + ex.Source)
                .AppendLine(ex.StackTrace);

            (assembly, errorInfo) = (null, report.ToString());
        }

        // Если не удалось открыть сборку
        return false;
    }

    /// <summary>
    /// Выводит рефлексивную информацию о всех типах в сборке, путь которой указан в параметре
    /// </summary>
    /// <param name="path">Путь до сборки</param>
    /// <param name="flags">Флаги получения полей, методов и конструкторов</param>
    public static void Print(string? path, BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly)
    {
        // Проверка на наличие переданного пути
        if (string.IsNullOrEmpty(path))
            return;

        // Если получилось открыть сборку, то выводим информацию о ней, если не получилось, то вывод ошибки
        if (TryOpenAssembly(path, out string? error, out Assembly? assembly))
            PrintInfo(null, assembly, flags);
        else
            Console.WriteLine(error);
    }

    /// <summary>
    /// Выводит рефлексивную информацию о пользовательском типе, который содержится в сборке по указанному пути
    /// </summary>
    /// <param name="path">Путь до сборки</param>
    /// <param name="typeName">Название пользовательского типа</param>
    /// <param name="flags">Флаги получения полей, методов и конструкторов</param>
    public static void Print(string? path, string? typeName, BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly)
    {
        // Проверка на наличие переданного пути и названия тип
        if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(typeName))
            return;

        // Если получилось открыть сборку и получить тип из указанного имени, то выводим информацию об этом типе
        if (TryOpenAssembly(path, out string? error, out Assembly? assembly) && TryGetTypeFromAssembly(assembly!, typeName, out Type? type))
            PrintInfo(type, assembly, flags);
        else
            Console.WriteLine(error);
    }
}