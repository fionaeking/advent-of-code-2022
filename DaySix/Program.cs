var inputString = File.ReadLines("PuzzleInput.txt").First();
Console.WriteLine(GetEndOfMarkerIndex(inputString.ToList()));


int GetEndOfMarkerIndex(List<char> input)
{
    var count = 0;
    while (input.Count > 3 + count)
    {
        if (input.Skip(count).Take(4).Distinct().Count() == 4)
        {
            return count + 4;
        }
        count++;
    }
    throw new Exception("No markers found in string");
}