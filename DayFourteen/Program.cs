
using System.Drawing;
var sandCoords = new HashSet<Point>();

foreach (var line in File.ReadAllLines("PuzzleInput.txt"))
{
    var pointArray = line.Split(" -> ").Select(x => new Point(int.Parse(x.Split(',')[0]), int.Parse(x.Split(',')[1]))).ToArray();
    for (int i = 0; i < pointArray.Length - 1; i++)
    {
        var diff_X = pointArray[i + 1].X - pointArray[i].X;
        var diff_Y = pointArray[i + 1].Y - pointArray[i].Y;
        sandCoords.Add(pointArray[i]);
        sandCoords.Add(pointArray[i + 1]);
        for (int j = 0; j < Math.Abs(diff_X); j++)
        {
            sandCoords.Add(new Point(((diff_X < 0) ? pointArray[i + 1].X : pointArray[i].X) + j, pointArray[i].Y));
        }
        for (int k = 0; k < Math.Abs(diff_Y); k++)
        {
            sandCoords.Add(new Point(pointArray[i].X, ((diff_Y < 0) ? pointArray[i + 1].Y : pointArray[i].Y) + k));
        }
    }
}

var maxY = sandCoords.Select(x => x.Y).OrderBy(x => x).Last();
var count = 0;
while (true)
{
    var sandPoint = GetNextSandPoint(sandCoords, maxY);
    if (sandPoint is null) break;
    else sandCoords.Add((Point)sandPoint);
    count++;
}

Console.WriteLine(count);

static Point? GetNextSandPoint(HashSet<Point> sandCoords, int maxY)
{
    var startPoint = new Point(500, 0);
    while (true)
    {
        var nextPoint = startPoint + new Size(0, 1);
        if (sandCoords.Contains(nextPoint))
        {
            nextPoint = startPoint + new Size(-1, 1);
            if (sandCoords.Contains(nextPoint))
            {
                nextPoint = startPoint + new Size(1, 1);
                if (sandCoords.Contains(nextPoint))
                {
                    return startPoint;
                }
            }
        }
        if (nextPoint.Y > maxY)
        {
            return null;
        }
        startPoint = nextPoint;
    }
}