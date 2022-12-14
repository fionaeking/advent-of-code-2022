var tailPointsVisited = new HashSet<Tuple<int, int>>();

var instructions = File.ReadAllLines("PuzzleInput.txt").ToList().Select(x => new Instruction()
{
    Direction = char.Parse(x.Split(" ")[0]),
    Distance = int.Parse(x.Split(" ")[1]),
});

var headPoint = new Tuple<int, int>(0, 0);
var tailPoint = new Tuple<int, int>(0, 0);
tailPointsVisited.Add(tailPoint);
foreach (var instruction in instructions)
{
    if (instruction.Direction == 'L')
    {
        for (int i = 0; i < instruction.Distance; i++)
        {
            headPoint = new Tuple<int, int>(headPoint.Item1 - 1, headPoint.Item2);
            if ((Math.Abs(tailPoint.Item1 - headPoint.Item1) > 1) || (Math.Abs(tailPoint.Item2 - headPoint.Item2) > 1))
            {
                // Move tail
                if (tailPoint.Item2 == headPoint.Item2)
                {
                    tailPoint = new Tuple<int, int>(tailPoint.Item1 - 1, tailPoint.Item2);
                }
                else
                {
                    // Diagonal
                    tailPoint = (headPoint.Item2 > tailPoint.Item2)
                        ? new Tuple<int, int>(tailPoint.Item1 - 1, tailPoint.Item2 + 1)
                        : new Tuple<int, int>(tailPoint.Item1 - 1, tailPoint.Item2 - 1);
                }
                tailPointsVisited.Add(tailPoint);
            }
        }
    }
    if (instruction.Direction == 'R')
    {
        for (int i = 0; i < instruction.Distance; i++)
        {
            headPoint = new Tuple<int, int>(headPoint.Item1 + 1, headPoint.Item2);
            if ((Math.Abs(tailPoint.Item1 - headPoint.Item1) > 1) || (Math.Abs(tailPoint.Item2 - headPoint.Item2) > 1))
            {
                // Move tail
                if (tailPoint.Item2 == headPoint.Item2)
                {
                    tailPoint = new Tuple<int, int>(tailPoint.Item1 + 1, tailPoint.Item2);
                }
                else
                {
                    // Diagonal
                    tailPoint = (headPoint.Item2 > tailPoint.Item2)
                        ? new Tuple<int, int>(tailPoint.Item1 + 1, tailPoint.Item2 + 1)
                        : new Tuple<int, int>(tailPoint.Item1 + 1, tailPoint.Item2 - 1);
                }
                tailPointsVisited.Add(tailPoint);
            }

        }

    }
    if (instruction.Direction == 'U')
    {
        for (int i = 0; i < instruction.Distance; i++)
        {
            headPoint = new Tuple<int, int>(headPoint.Item1, headPoint.Item2 + 1);
            if ((Math.Abs(tailPoint.Item1 - headPoint.Item1) > 1) || (Math.Abs(tailPoint.Item2 - headPoint.Item2) > 1))
            {
                // Move tail
                if (tailPoint.Item1 == headPoint.Item1)
                {
                    tailPoint = new Tuple<int, int>(tailPoint.Item1, tailPoint.Item2 + 1);
                }
                else
                {
                    // Diagonal
                    tailPoint = (headPoint.Item1 > tailPoint.Item1)
                        ? new Tuple<int, int>(tailPoint.Item1 + 1, tailPoint.Item2 + 1)
                        : new Tuple<int, int>(tailPoint.Item1 - 1, tailPoint.Item2 + 1);
                }
                tailPointsVisited.Add(tailPoint);
            }
        }
    }
    if (instruction.Direction == 'D')
    {
        for (int i = 0; i < instruction.Distance; i++)
        {
            headPoint = new Tuple<int, int>(headPoint.Item1, headPoint.Item2 - 1);
            if ((Math.Abs(tailPoint.Item1 - headPoint.Item1) > 1) || (Math.Abs(tailPoint.Item2 - headPoint.Item2) > 1))
            {
                // Move tail
                if (tailPoint.Item1 == headPoint.Item1)
                {
                    tailPoint = new Tuple<int, int>(tailPoint.Item1, tailPoint.Item2 - 1);
                }
                else
                {
                    // Diagonal
                    tailPoint = (headPoint.Item1 > tailPoint.Item1)
                        ? new Tuple<int, int>(tailPoint.Item1 + 1, tailPoint.Item2 - 1)
                        : new Tuple<int, int>(tailPoint.Item1 - 1, tailPoint.Item2 - 1);
                }
                tailPointsVisited.Add(tailPoint);
            }
        }
    }
}
Console.WriteLine($"Part one count: {tailPointsVisited.Count}");


public class Instruction
{
    public char Direction;
    public int Distance;
}