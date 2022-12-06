using DayThree;

var sum = 0;
foreach (var line in File.ReadAllLines("PuzzleInput.txt"))
{
    sum += ConvertCharToInt(
              line[..(line.Length / 2)].Distinct()
              .Concat(line[(line.Length / 2)..].Distinct())
              .MostCommonChar()
            );
}
Console.WriteLine($"Part one answer: {sum}");


var sumTwo = 0;
foreach (var elfGroup in File.ReadAllLines("PuzzleInput.txt").ToList().Partition(3))
{
    sumTwo += ConvertCharToInt(elfGroup.SelectMany(x => x.Distinct()).MostCommonChar());
}
Console.WriteLine($"Part two answer: {sumTwo}");


static int ConvertCharToInt(char c)
{
    return char.IsUpper(c) ? (int)c - 70 : (int)c - 64;
}
