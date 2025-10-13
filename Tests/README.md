# Tests
## Comparator
### Результат:
```CMD
| Method           | Mean     | Error    | StdDev   | Rank | Gen0    | Allocated |
|----------------- |---------:|---------:|---------:|-----:|--------:|----------:|
| Array_Comparator | 33.21 us | 0.643 us | 0.602 us |    2 |       - |      96 B |
| Array_OrderBy    | 20.46 us | 0.249 us | 0.220 us |    1 | 11.5662 |   24304 B |
| List_Comparator  | 34.93 us | 0.681 us | 0.976 us |    2 |       - |      96 B |
| List_OrderBy     | 20.19 us | 0.228 us | 0.202 us |    1 | 11.6272 |   24336 B |
```

### Обсуждение
Сравнивались следующие методы:
 - `Array.Sort(userArr, Comparator.GetComparator<User, int>(p => p.Age));`
 - `userArr = userArr.OrderBy(p => p.Age).ToArray();`
 - `userList.Sort(Comparator.GetComparator<User, int>(p => p.Age));`
 - `userList = userList.OrderBy(p => p.Age).ToList();`

Наибольшую производительность показали методы System.Linq.OrderBy, но наименьшее использование памяти показали методы, использующие Comparator.GetComparator.