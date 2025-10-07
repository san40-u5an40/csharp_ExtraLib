using BenchmarkDotNet.Running;

namespace Tests;

/// <summary>
/// Класс для запуска тестов из namespace Tests
/// </summary>
public static class Runner
{
    /// <summary>
    /// Основной метод для запуска тестов из namespace Tests
    /// </summary>
    public static void Run() => BenchmarkRunner.Run(typeof(Runner).Assembly);
}
