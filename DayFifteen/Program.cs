using System.Drawing;
using System.Text.RegularExpressions;

var regex = new Regex(@"Sensor at x=(?<x1>-?\d+), y=(?<y1>-?\d+): closest beacon is at x=(?<x2>-?\d+), y=(?<y2>-?\d+)");

var allSensors = File.ReadAllLines("PuzzleInput.txt")
    .Select(x => regex.Match(x).Groups)
    .Select(x => new SensorBeaconPair()
    {
        Sensor = new Point(int.Parse(x["x1"].Value), int.Parse(x["y1"].Value)),
        Beacon = new Point(int.Parse(x["x2"].Value), int.Parse(x["y2"].Value)),
        ManhattanDistance = ManhattanDistance(new Point(int.Parse(x["x1"].Value), int.Parse(x["y1"].Value)), new Point(int.Parse(x["x2"].Value), int.Parse(x["y2"].Value)))
    });

var maxY = 2000000;

var interestingSensors = allSensors
    .Where(x => (x.Sensor.Y <= maxY && (x.Sensor.Y + x.ManhattanDistance >= maxY))
             || (x.Sensor.Y >= maxY && (x.Sensor.Y - x.ManhattanDistance <= maxY)));

// Work out points along maxY axis that would be covered by each sensor/beacon
var notSensors = new HashSet<Point>();
foreach (var sensor in interestingSensors)
{
    var sens = sensor.Sensor;
    // Go directly above
    var above = new Point(sens.X, maxY);
    var manhattenDistance = ManhattanDistance(above, sens);
    while (manhattenDistance <= sensor.ManhattanDistance)
    {
        notSensors.Add(above);
        above.X--;
        manhattenDistance = ManhattanDistance(above, sens);

    }

    above = new Point(sens.X, maxY);
    manhattenDistance = ManhattanDistance(above, sens);
    while (manhattenDistance <= sensor.ManhattanDistance)
    {
        notSensors.Add(above);
        above.X++;
        manhattenDistance = ManhattanDistance(above, sens);
    }
}

var count = notSensors.Where(x => !interestingSensors.Select(x => x.Beacon).Contains(x)).Count();
Console.WriteLine(count);



static int ManhattanDistance(Point one, Point two)
{
    return Math.Abs(one.X - two.X) + Math.Abs(one.Y - two.Y);
}

class SensorBeaconPair
{
    public Point Sensor;
    public Point Beacon;
    public int ManhattanDistance;
}

