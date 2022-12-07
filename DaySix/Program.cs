var inputString = File.ReadLines("PuzzleInput.txt").First();
Console.WriteLine(GetEndOfMarkerIndex(inputString.ToList(), 4));
Console.WriteLine(GetEndOfMarkerIndex(inputString.ToList(), 14));

static int GetEndOfMarkerIndex(List<char> input, int numOfDistinctChars)
{
    var count = 0;
    while (input.Count > (numOfDistinctChars - 1) + count)
    {
        if (input.Skip(count).Take(numOfDistinctChars).Distinct().Count() == numOfDistinctChars)
        {
            return count + numOfDistinctChars;
        }
        count++;
    }
    throw new Exception("No markers found in string");
}