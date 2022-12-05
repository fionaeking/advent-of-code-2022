var sum = 0;
foreach (var line in File.ReadAllLines("PuzzleInput.txt"))
{
    sum += ConvertCharToInt(
              line[..(line.Length / 2)].Distinct()
              .Concat(line[(line.Length / 2)..].Distinct())
              .GroupBy(x => x)
              .OrderByDescending(x => x.Count())
              .First().Key.Invert()
            );
}
Console.WriteLine($"Part one answer: {sum}");

static int ConvertCharToInt(char c)
{
    return (char.IsLower(c)) ? (int)c - 70 : (int)c - 64;
}
