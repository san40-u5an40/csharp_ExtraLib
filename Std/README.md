# ExtraLib.Std
## Comparator
### ����������:
����������� �����, ������� ���������� ������ IComparer ����������� ���������� ���������������� ���� �� ���������� ���������.

### ���������:
����������� ������:
 - GetComparator\<TSource, TKey> - ���������� ��������� ������ ��� ��������� �� ���������� ������. ������ Generic-���������� ����������� ������� ���������, ������ ������������ ��� ������.

### ������� ����:
```C#
Array.Sort(array, Comparator.GetComparator<User, string>(p => p.Name));
// ���������� ������� �������� "User" �� �����
// string - �.�. ���� ����� ������������ �������� Name
```

## ConsoleHelper
### ����������:
����������� ����� � ��������������� �������� ��� ����������� �����-������.

### ���������:
����������� ������:
 - TryReadLine - �������� ����� ��� � ������ ������: ���������� � out-�������� ��������� ������ �� �������, ��������� �������� �� ���� ��������� �������.
 - WriteColor - ������� � ������� ����� ���������� �����.

### ������� ����:
TryReadLine
```C#
while (!ConsoleHelper.TryReadLine(out string? input))
	Console.WriteLine("������������ ����. ��������� �������.");
```

WriteColor
```C#
internal static void PrintWelcome(string name)
{
    Console.Write("����� ���������� � ���������, ");
    ConsoleHelper.WriteColor(name, ConsoleColor.Red);
    Console.Write("!\n");
}
```

## Counter
### ����������:
����� ��� �������� ���������. ����� ���� ����������� ����� ������������ ��������� ������� � �������� ��������� ���������.

### ���������:
� ����������� ��������� ��������� �������� �������� � �������������� ���������� ��� ��� (�� ��������� ����� "Default").

������ � ��������:
 - void Increment() - �������������� ��������.
 - int Value - ���������� ��������.
 - string Name - ���������� ��������.

����������� ������:
 - Func\<int> Create(Int32) - ��������� ��������� �������� � ���������� ������� ��������.

�������������� ����������:
� ������ �������������� ������ ������� Object: ToString, Equals � GetHashCode. ��������� � ��������� ���-���� �������������� �� ������ �������� �������� � ��� �����.\
����� � ������� ������ ���������� ��������� ���������: ==, !=, ++.

### ������� ����:
������������� ����� ��������� Counter:
```C#
var cnt = new Counter(100);
for (int i = 0; i < 10; i++)
    cnt++;

Console.WriteLine(cnt);

// �����:
// Default: 110
```

������������� ����� ��������� �������:
```C#
var cnt = Counter.Create(100);
for (int i = 0; i < 3; i++)
    Console.WriteLine($"�������: {cnt()}");

// �����:
// �������: 100
// �������: 101
// �������: 102
```

## Display
### ����������:
����������� ����� ��� ��������� ����������� ����, � ����� ������ �������. �������� ������������ `Type` ��� �������� � ��������� ���� �������.

### ���������:
����������� ������:
 - WindowSetup - ������������� ��������� �������, � ������, ������� ��������� ������� � ������������� ��������� UTF-16.
 - Print - ������� ������� � �������, ��������� � �������� ����������: ��� �������, ��� �����, ������ ������� � ��������� ��� ����������� � ��.

���� ������� (�� enum Type):
 - Start - ����� ������ ������� ����������� `\n`.
 - End - `\n` ����������� ����� ��������.
 - Center - `\n` ����������� ��� ��, ��� � ����� �������.

### ������� ����:
WindowSetup
```C#
Display.WindowSetup(displayLength: 100, displaySpace: 5, programName: "����������");
// ��������� ������ ������� �� 105 (displayLength + displaySpace)
// ��������� ��������� ����: ����������
// �������� ��������� �������
// ��������� ��������� UTF-16
```

Print
```C#
Display.Print(Display.Type.Center, 40, '-', "������ �����", "������ �����");

// �����:
// /*----------------------------------------*/
// /*--------------������ �����--------------*/
// /*--------------������ �����--------------*/
// /*----------------------------------------*/
```
## NumHelper
### ����������:
����������� ����� � �������� ���������� ��� ������ � �������.

### ���������:
������ ����������:
 - InWord - ����������� ����� �� ��������� �� 0 �� 100 (���� ���) � ��������� ������������� (������������ �����).
 - GetEnumerator - ���������� ������ IEnumerator ��� �������� ����� � ����� foreach.

### ������� ����:
InWord
```C#
Console.WriteLine(25.InWord());
// �������� ����
```

GetEnumerator
```C#
foreach (int num in -4)
    Console.WriteLine(num);

// �����
// -4
// -3
// -2
// -1
// 0
```

## TimerHelper
### ����������:
����������� ����� ��������������� ��� ������������� ���������������� ��������.

### ���������:
����������� ������:
 - ExpWait - ������������ �������� � ����������� �� ����������������� �������� �������, ������������� �� ������ ��������� �������� �����.

### ������� ����:
��� �������������� ����������:
```C#
for(int i = 0; i < int.MaxValue; i++)
{
    Console.Write($"\r��������: {i}");
    TimerHelper.ExpWait(i);
    // � ����������� �� �������� ��������� ����� �� ������ ���������� �������
}
```

� ��������� ���������� �������� ������� � ����:
```C#
for(int i = 0; i < int.MaxValue; i++)
{
    Console.Write($"\r��������: {i}");
    TimerHelper.ExpWait(i, 6.5, 0.1);
}
```

## Reflection
### ����������:
����������� �����, ��������������� ��� ������������ ���������������� �����.

### ���������:
��������:
 - ReflectionAttribute(BindingFlags(optional)) - ������� ��� ������� ������ � ������������� ������������� ��� ������������� ������� ������� Reflection.Print();

����������� ������:
 - Print() - ������� ���������� � ���� �������, ������� ������� ReflectionAttribute;
 - Print(Type, BindingFlags(optional)) - ������� ������������ ���������� � ���������� � �������� ���� �������� ��������� ������ (�� ���������� ���������� � �������, ������� ��������). 

### ������� ����:
������������� ����� �������:
```C#
// Main:
Reflection.Print();

// �������� ����� ��� �������
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

// �����:
// ������ ������ "SandBox.dll":
//
//   ����� "ClassTest":
//   |
//   |  ����:
//   |  |
//   |  |  ���: name                 | ���: System.String                            | ��������: Private
//   |  |  ���: getNumber            | ���: System.Func`1[System.Int32]              | ��������: Private
//   |  |  ���: count                | ���: System.Int32                             | ��������: Private, Static
//   |  |
//   |  ����� ����������: 3
//   |
//   |  ������:
//   |  |
//   |  |  ���: get_Name
//   |  |     ��������: Public, HideBySig, SpecialName
//   |  |     ������������ ���: System.String
//   |  |
//   |  |  ���: add_GetNumber
//   |  |     ��������: Public, HideBySig, SpecialName
//   |  |     ������������ ���: Void
//   |  |     ���������: (System.Func`1[System.Int32]) value
//   |  |
//   |  |  ���: remove_GetNumber
//   |  |     ��������: Public, HideBySig, SpecialName
//   |  |     ������������ ���: Void
//   |  |     ���������: (System.Func`1[System.Int32]) value
//   |  |
//   |  |  ���: Print
//   |  |     ��������: Assembly, HideBySig
//   |  |     ������������ ���: Void
//   |  |     ���������: (System.String) mess, (System.String) mess2
//   |  |
//   |  ����� ����������: 4
//   |
//   |  ������������:
//   |  |
//   |  |  ���: .ctor
//   |  |     ��������: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     ���������: (System.String) name
//   |  |
//   |  |  ���: .ctor
//   |  |     ��������: Public, HideBySig, SpecialName, RTSpecialName
//   |  |
//   |  ����� ����������: 2
//   |
//   ����� ������
//
// ����� ������
```

������������� ��� �������� �������� (����������� ����� String):
```C#
// Main:
Reflection.Print(typeof(System.String));

// �����:
// ������ ������ "System.Private.CoreLib.dll":
//
//   ����� "String":
//   |
//   |  ����:
//   |  |
//   |  |  ���: _stringLength        | ���: System.Int32                             | ��������: Private, InitOnly, NotSerialized
//   |  |  ���: _firstChar           | ���: System.Char                              | ��������: Private, NotSerialized
//   |  |  ���: Empty                | ���: System.String                            | ��������: Public, Static, InitOnly
//   |  |
//   |  ����� ����������: 3
//   |
//   |  ������:
//   |  |
//   |  |  ���: FastAllocateString
//   |  |     ��������: Assembly, Static, HideBySig
//   |  |     ������������ ���: System.String
//   |  |     ���������: (System.Int32) length
//   |  |

// ��� ����� ����� �������, ������ ��� ������� ������������� ����

//   |  |
//   |  |  ���: LastIndexOf
//   |  |     ��������: Public, HideBySig
//   |  |     ������������ ���: Int32
//   |  |     ���������: (System.String) value, (System.Int32) startIndex, (System.Int32) count, (System.StringComparison) comparisonType
//   |  |
//   |  ����� ����������: 267
//   |
//   |  ������������:
//   |  |
//   |  |  ���: .ctor
//   |  |     ��������: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     ���������: (System.Char[]) value
//   |  |
//   |  |  ���: .ctor
//   |  |     ��������: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     ���������: (System.Char[]) value, (System.Int32) startIndex, (System.Int32) length
//   |  |
//   |  |  ���: .ctor
//   |  |     ��������: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     ���������: (System.Char*) value
//   |  |
//   |  |  ���: .ctor
//   |  |     ��������: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     ���������: (System.Char*) value, (System.Int32) startIndex, (System.Int32) length
//   |  |
//   |  |  ���: .ctor
//   |  |     ��������: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     ���������: (System.SByte*) value
//   |  |
//   |  |  ���: .ctor
//   |  |     ��������: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     ���������: (System.SByte*) value, (System.Int32) startIndex, (System.Int32) length
//   |  |
//   |  |  ���: .ctor
//   |  |     ��������: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     ���������: (System.SByte*) value, (System.Int32) startIndex, (System.Int32) length, (System.Text.Encoding) enc
//   |  |
//   |  |  ���: .ctor
//   |  |     ��������: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     ���������: (System.Char) c, (System.Int32) count
//   |  |
//   |  |  ���: .ctor
//   |  |     ��������: Public, HideBySig, SpecialName, RTSpecialName
//   |  |     ���������: (System.ReadOnlySpan`1[System.Char]) value
//   |  |
//   |  ����� ����������: 9
//   |
//   ����� ������
//
// ����� ������
```