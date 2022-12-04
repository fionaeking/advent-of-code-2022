var sums = new List<int>();
foreach (var groupedCalorieCounts in File.ReadAllText("PuzzleInput.txt")
    .Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
{
    var calorieCount = 0;
    foreach (var groupCalorieCount in groupedCalorieCounts.Split(Environment.NewLine))
    {
        if (Int32.TryParse(groupCalorieCount.Trim(), out int result))
        {
            calorieCount += result;
        }
        else
        {
            Console.WriteLine($"Error parsing {groupCalorieCount} to string");
        }
    }
    Console.WriteLine($"Total calorie count: {calorieCount}");
    sums.Add(calorieCount);
}
var orderedCalorieCounts = sums.OrderByDescending(x => x);
Console.WriteLine($"Max calorie count: {orderedCalorieCounts.First()}");
Console.WriteLine($"Sum of 3 highest calorie counts: {orderedCalorieCounts.Take(3).Sum()}");