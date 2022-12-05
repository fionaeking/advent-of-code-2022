// Should have utilised Linq more here...
var delimiters = new char[] { ',', '-' };
var partOneCount = 0;
var partTwoCount = 0;
foreach (var line in File.ReadLines("PuzzleInput.txt").Select(x => x.Split(delimiters)))
{
    var f = line.Select(x => Int64.Parse(x)).ToArray();
    if ((f[0] == f[2]) || (f[1] == f[3]) || ((f[0] > f[2]) && (f[1] < f[3])) || ((f[0] < f[2]) && (f[1] > f[3])))
    {
        partOneCount++;
    }
    else if ((f[0] == f[2]) || (f[0] == f[3]) || (f[1] == f[2]) || (f[1] == f[3]) || ((f[1] > f[2]) && (f[3] > f[0])))
    {
        partTwoCount++;
    }
}
partTwoCount += partOneCount;
Console.WriteLine(partOneCount);
Console.WriteLine(partTwoCount);