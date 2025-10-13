using BenchmarkDotNet.Attributes;
using ExtraLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests;

[MemoryDiagnoser]
[RankColumn]
public class ComparatorTests
{
    private const int N = 1_000;
    private const int MAX_RND_VALUE = 100;
    private const int MIN_RND_VALUE = 0;

    private User[] userArr = new User[N];
    private List<User> userList = new List<User>(N);
    private Random rnd;

    public ComparatorTests()
    {
        rnd = new Random(DateTime.Now.Microsecond);

        for (int i = 0; i < N; i++)
        {
            var rndValue = rnd.Next(MIN_RND_VALUE, MAX_RND_VALUE);
            var user = new User("Vasya", "OOO", rndValue);

            userArr[i] = user;
            userList.Add(user);
        }
    }

    [Benchmark]
    public void Array_Comparator()
    {
        Array.Sort(userArr, Comparator.GetComparator<User, int>(p => p.Age));
    }

    [Benchmark]
    public void Array_OrderBy()
    {
        userArr = userArr.OrderBy(p => p.Age).ToArray();
    }

    [Benchmark]
    public void List_Comparator()
    {
        userList.Sort(Comparator.GetComparator<User, int>(p => p.Age));
    }

    [Benchmark]
    public void List_OrderBy()
    {
        userList = userList.OrderBy(p => p.Age).ToList();
    }
}

public record User(string Name, string Company, int Age);

// Результат:
// | Method           | Mean     | Error    | StdDev   | Rank | Gen0    | Allocated |
// |----------------- |---------:|---------:|---------:|-----:|--------:|----------:|
// | Array_Comparator | 33.21 us | 0.643 us | 0.602 us |    2 |       - |      96 B |
// | Array_OrderBy    | 20.46 us | 0.249 us | 0.220 us |    1 | 11.5662 |   24304 B |
// | List_Comparator  | 34.93 us | 0.681 us | 0.976 us |    2 |       - |      96 B |
// | List_OrderBy     | 20.19 us | 0.228 us | 0.202 us |    1 | 11.6272 |   24336 B |