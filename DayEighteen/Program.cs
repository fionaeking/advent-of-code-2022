var input = File.ReadAllLines("PuzzleInput.txt").Select(x => x.Split(',').Select(y => int.Parse(y)).ToArray());
var sideCount = 0;

// TODO I only got part 1 working here

foreach (var item in input)
{
    var sides = 6;
    foreach (var otherItem in input.Where(x => x != item))
    {
        if (Compare(item, otherItem))
        {
            sides--;
        }
    }
    sideCount += sides;
}

Console.Write(sideCount);

static bool Compare(int[] a, int[] b)
{
    return Math.Abs(a[0] - b[0]) + Math.Abs(a[1] - b[1]) + Math.Abs(a[2] - b[2]) == 1;

}