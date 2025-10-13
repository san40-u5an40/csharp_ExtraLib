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
        var res = userArr.OrderBy(p => p.Age).ToArray();
    }

    [Benchmark]
    public void List_Comparator()
    {
        userList.Sort(Comparator.GetComparator<User, int>(p => p.Age));
    }

    [Benchmark]
    public void List_OrderBy()
    {
        var res = userList.OrderBy(p => p.Age).ToList();
    }
}

public record User(string Name, string Company, int Age);

// Результат:
// | Method           | Mean     | Error    | StdDev   | Rank | Gen0    | Allocated |
// |----------------- |---------:|---------:|---------:|-----:|--------:|----------:|
// | Array_Comparator | 54.68 us | 0.946 us | 0.885 us |    1 |       - |      80 B |
// | Array_OrderBy    | 65.73 us | 1.226 us | 1.087 us |    2 | 11.4746 |   24288 B |
// | List_Comparator  | 56.02 us | 0.609 us | 0.540 us |    1 |       - |      80 B |
// | List_OrderBy     | 64.37 us | 0.900 us | 0.798 us |    2 | 11.5967 |   24320 B |