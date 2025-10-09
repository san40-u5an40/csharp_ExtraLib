using System;

namespace Std;

/// <summary>
/// Класс для конвертирования различных единиц исчисления
/// </summary>
public static class Converter
{
    /// <summary>
    /// Метод для перевода байт в набор гигабайт, мегабайт, килобайт и байт
    /// </summary>
    /// <param name="bytes">Количество байт</param>
    /// <returns>Структура Std.Size, хранящая результат конвертации</returns>
    public static Size ToSize(long bytes)
    {
        long gByte = Math.DivRem(bytes, (long)Math.Pow(2, 30), out bytes);
        long mByte = Math.DivRem(bytes, (long)Math.Pow(2, 20), out bytes);
        long kByte = Math.DivRem(bytes, (long)Math.Pow(2, 10), out bytes);

        return new Size(gByte, mByte, kByte, bytes);
    }
}

/// <summary>
/// Структура для хранения результата перевода байт в набор гигабайт, мегабайт, килобайт и байт
/// </summary>
public readonly record struct Size(long GByte, long MByte, long KByte, long Byte)
{
    public override string ToString() => $"{GByte} Гбайт, {MByte} Мбайт, {KByte} Кбайт, {Byte} байт";
}