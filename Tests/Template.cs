using BenchmarkDotNet.Attributes;
using System;

namespace Tests;

//[MemoryDiagnoser]
//[RankColumn]
public class Test_Template
{
    private const int N = 1_000;

    //private StringBuilder anySymbols = new StringBuilder(N);
    //private int[] anyNumbers = new int[N];
    
    private Random rnd = new Random(DateTime.Now.Microsecond);
    private const int maxRndValue = 100;
    private const int minRndValue = 0;

    public Test_Template()
    {
        var rnd = new Random();

        for(int i = 0; i < N; i++)
        {
            var rndValue = rnd.Next(0, 100);

            // Add something into StringBuilder or int[]

        }
    }

    //[Benchmark]
    public void DoSomething() { /* DoSomething with added data */ }
}

