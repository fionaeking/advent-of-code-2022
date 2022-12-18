using DayThirteen;

var index = 0;
var sum = 0;
var input = File.ReadAllText("PuzzleInput.txt");
var partOne = input.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
var comparer = new SignalComparer();
foreach (var pair in partOne)
{
    index++;
    var one = pair.Split(Environment.NewLine).ToList();
    var two = pair.Split(Environment.NewLine).ToList();
    two.Sort(comparer);
    if (two.SequenceEqual(one)) sum += index;
}

Console.WriteLine($"Part one: {sum}");

const string dividerOne = "[[2]]";
const string dividerTwo = "[[6]]";
var partTwo = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Append(dividerOne).Append(dividerTwo).ToList();
partTwo.Sort(comparer);

Console.WriteLine($"Part two: {(partTwo.IndexOf(dividerOne) + 1) * (partTwo.IndexOf(dividerTwo) + 1)}");


