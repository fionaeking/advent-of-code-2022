var delimiters = new char[] { ',', '-' };
var containsCount = 0;
foreach (var line in File.ReadLines("PuzzleInput.txt").Select(x => x.Split(delimiters)))
{
    var f = line.Select(x => Int64.Parse(x)).ToArray();
    if ((f[0] == f[2]) || (f[1] == f[3]) || ((f[0] > f[2]) && (f[1] < f[3])) || ((f[0] < f[2]) && (f[1] > f[3])))
    {
        containsCount++;
    }
}
Console.WriteLine(containsCount);