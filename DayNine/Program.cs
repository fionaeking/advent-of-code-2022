using DayNine;

var tailPointsVisited = new HashSet<Tuple<int, int>>();
var ropePoints = new Dictionary<int, Tuple<int, int>> {
    {0, new Tuple<int, int>(0, 0)},
    {1,  new Tuple<int, int>(0, 0)},
    {2,  new Tuple<int, int>(0, 0)},
    {3,  new Tuple<int, int>(0, 0)},
    {4,  new Tuple<int, int>(0, 0)},
    {5,  new Tuple<int, int>(0, 0)},
    {6,  new Tuple<int, int>(0, 0)},
    {7,  new Tuple<int, int>(0, 0)},
    {8,  new Tuple<int, int>(0, 0)},
    {9,  new Tuple<int, int>(0, 0)}
};
tailPointsVisited.Add(ropePoints[9]);

foreach (var instruction in File.ReadAllLines("PuzzleInput.txt").ToList().Select(x => new Instruction()
{
    Direction = (Direction)Enum.Parse(typeof(Direction), x.Split(" ")[0].ToString()),
    Distance = int.Parse(x.Split(" ")[1]),
}))
{
    for (int i = 0; i < instruction.Distance; i++)
    {
        if (instruction.Direction == Direction.L)
        {
            ropePoints[0] = new Tuple<int, int>(ropePoints[0].Item1 - 1, ropePoints[0].Item2);
        }
        if (instruction.Direction == Direction.R)
        {
            ropePoints[0] = new Tuple<int, int>(ropePoints[0].Item1 + 1, ropePoints[0].Item2);
        }
        if (instruction.Direction == Direction.U)
        {
            ropePoints[0] = new Tuple<int, int>(ropePoints[0].Item1, ropePoints[0].Item2 + 1);
        }
        if (instruction.Direction == Direction.D)
        {
            ropePoints[0] = new Tuple<int, int>(ropePoints[0].Item1, ropePoints[0].Item2 - 1);
        }

        for (int j = 1; j < ropePoints.Count; j++)
        {
            ropePoints[j] = MoveTail(ropePoints[j], ropePoints[j - 1]);
            // TODO Check what direction last went in
        }
        tailPointsVisited.Add(ropePoints[9]);
    }
}
Console.WriteLine($"Count: {tailPointsVisited.Count}");

static Tuple<int, int> MoveTail(Tuple<int, int> tailPoint, Tuple<int, int> headPoint)
{
    if ((Math.Abs(tailPoint.Item1 - headPoint.Item1) > 1) || (Math.Abs(tailPoint.Item2 - headPoint.Item2) > 1))
    {
        // Move tail
        if (tailPoint.Item2 == headPoint.Item2)
        {
            tailPoint = (headPoint.Item1 < tailPoint.Item1)
                ? new Tuple<int, int>(tailPoint.Item1 - 1, tailPoint.Item2)
                : new Tuple<int, int>(tailPoint.Item1 + 1, tailPoint.Item2);
        }
        else if (tailPoint.Item1 == headPoint.Item1)
        {
            tailPoint = (headPoint.Item2 < tailPoint.Item2)
               ? new Tuple<int, int>(tailPoint.Item1, tailPoint.Item2 - 1)
               : new Tuple<int, int>(tailPoint.Item1, tailPoint.Item2 + 1);
        }
        else
        {
            // Diagonal
            var item1 = (headPoint.Item1 < tailPoint.Item1) ? tailPoint.Item1 - 1 : tailPoint.Item1 + 1;
            var item2 = (headPoint.Item2 < tailPoint.Item2) ? tailPoint.Item2 - 1 : tailPoint.Item2 + 1;
            tailPoint = new Tuple<int, int>(item1, item2);
        }
    }
    return tailPoint;
}
