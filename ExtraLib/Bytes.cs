using System;

namespace ExtraLib;

/// <summary>
/// Класс для конвертирования байтов
/// </summary>
public static class Bytes
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

    /// <summary>
    /// Метод для перевода байт в гигабайты
    /// </summary>
    /// <param name="bytes">Количество байт</param>
    /// <returns>Количество гигабайт</returns>
    public static long ToGb(long bytes) => 
        (long)Math.Round(bytes / Math.Pow(2, 30));

    /// <summary>
    /// Метод для перевода байт в мегабайты
    /// </summary>
    /// <param name="bytes">Количество байт</param>
    /// <returns>Количество мегабайт</returns>
    public static long ToMb(long bytes) =>
        (long)Math.Round(bytes / Math.Pow(2, 20));

    /// <summary>
    /// Метод для перевода байт в килобайты
    /// </summary>
    /// <param name="bytes">Количество байт</param>
    /// <returns>Количество килобайт</returns>
    public static long ToKb(long bytes) => 
        (long)Math.Round(bytes / Math.Pow(2, 10));
}

/// <summary>
/// Структура для хранения результата перевода байт в набор гигабайт, мегабайт, килобайт и байт
/// </summary>
public readonly record struct Size(long GByte, long MByte, long KByte, long Byte)
{
    public override string ToString() => $"{GByte} Гбайт, {MByte} Мбайт, {KByte} Кбайт, {Byte} байт";
}