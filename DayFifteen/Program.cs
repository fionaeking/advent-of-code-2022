using System.Diagnostics.Metrics;
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


// PartOne(allSensors);

// TODO I only got part 1 working here

PartTwo(allSensors);

static void PartOne(IEnumerable<SensorBeaconPair> allSensors)
{
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
}

void PartTwo(IEnumerable<SensorBeaconPair> allSensors)
{
    var max = 4000000;
    var min = 0;

    // All sensors where either the sensor is within the zone, or the sensor plus the manhattan distance is in the zone
    var interestingSensors = allSensors
        .Where(x => (x.Sensor.X <= max && x.Sensor.X >= min && x.Sensor.Y <= max && x.Sensor.Y >= min)
            || ((x.Sensor.X + x.ManhattanDistance <= max) && (x.Sensor.X + x.ManhattanDistance >= min))
            || ((x.Sensor.Y + x.ManhattanDistance <= max) && (x.Sensor.Y + x.ManhattanDistance >= min)));

    var notSensors = new HashSet<Point>();
    // Go through each possible point and see if it is in manhattan distance

    for (int x = 0; x < max; x++)
    {
        for (int y = 0; y < max; y++)
        {
            if (interestingSensors.Any(z => ((z.Sensor.X == x) && (z.Sensor.Y == y)) || ((z.Beacon.X == x) && (z.Beacon.Y == y))))
            {
                continue;
            }
            if (!Test(interestingSensors, x, y))
            {
                //Console.WriteLine($"{x} {y}");
                long h = ((long)x * 4000000) + y;
                Console.WriteLine(h);
                return;
            }
        }
    }

}

bool Test(IEnumerable<SensorBeaconPair> interestingSensors, int x, int y)
{
    foreach (var sensor in interestingSensors)
    {
        var point = new Point(x, y);
        var manhattenDistance = ManhattanDistance(point, sensor.Sensor);
        if (manhattenDistance <= sensor.ManhattanDistance) return true;
    }
    return false;
}

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

