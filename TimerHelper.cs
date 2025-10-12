using System;
using System.Threading;

namespace ExtraLib;

/// <summary>
/// Класс предназначенный для использования экспоненциальных таймеров
/// </summary>
public static class TimerHelper
{
    /// <summary>
    /// Функция вызова экспоненциального таймера блокирующего поток
    /// </summary>
    /// <param name="iteration">Номер итерации в цикле</param>
    /// <param name="startValue">Стартовое значение таймера (рекомендуется 6.5)</param>
    /// <param name="step">Шаг изменения значения таймера между итерациями (рекомендуется 0.1)</param>
    public static void ExpWait(int iteration, double startValue = 6.5, double step = 0.1) => 
        Thread.Sleep((int)Math.Exp(startValue - step * iteration));
}