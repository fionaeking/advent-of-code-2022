using System.Drawing;

// TODO I only got part 1 working here

var jetPattern = File.ReadAllText("PuzzleInput.txt").ToCharArray();
const int width = 7;
const int rockLeftMargin = 2;
const int rockBottomMargin = 3;

const long maxRockCount = 1000000000000; // 2022;

long currRockCount = 0;


var allRocks = new Dictionary<int, List<Point>>()
{
    {0,  new List<Point>() { new Point(2, 0), new Point(3, 0), new Point(4, 0), new Point(5, 0) }},
    {1, new List<Point>() { new Point(3, 0), new Point(2, 1), new Point(3, 1), new Point(4, 1), new Point(3, 2) } },
    {2, new List<Point>() { new Point(4, 0), new Point(4, 1), new Point(4, 2), new Point(3, 2), new Point(2, 2) } },
    {3, new List<Point>() { new Point(2, 0), new Point(2, 1), new Point(2, 2), new Point(2, 3) } },
    {4, new List<Point>() { new Point(2, 0), new Point(2, 1), new Point(3, 0), new Point(3, 1) } },
};


var currRock = allRocks[0].Select(x => x).ToList();

var baseY = currRock.Select(x => x.Y).Max() + 4;

var bottom = new List<Point>()
{
    new Point(0, baseY), new Point(1, baseY), new Point(2, baseY), new Point(3, baseY), new Point(4, baseY), new Point(5, baseY), new Point(6, baseY),
};
long baseTracker = baseY;
while (currRockCount < maxRockCount)
{
    foreach (var direction in jetPattern)
    {
        // Right or left first
        if (direction == '>' && currRock.Select(x => x.X).Max() < (7 - 1) && !currRock.Any(y => bottom.Where(x => x.Y == y.Y).Any(x => y.X + 1 == x.X)))
        {
            currRock = currRock.Select(x => new Point(x.X + 1, x.Y)).ToList();
        }
        else if (direction == '<' && currRock.Select(x => x.X).Min() > 0 && !currRock.Any(y => bottom.Where(x => x.Y == y.Y).Any(x => y.X - 1 == x.X)))
        {
            currRock = currRock.Select(x => new Point(x.X - 1, x.Y)).ToList();
        }

        // THen Check if we should go down

        if (currRock.Any(y => bottom.Where(x => x.X == y.X).Any(x => x.Y == y.Y + 1)))
        {
            // we were already at bottom
            foreach (var point in currRock.OrderByDescending(x => x.Y))
            {
                bottom.Add(point);
                // If this is true, no rocks can get past here
                if (bottom.Where(x => x.Y == point.Y || x.Y == point.Y - 1).Select(x => x.X).Distinct().Count() == 7)
                {
                    bottom = bottom.Where(x => x.Y <= point.Y).ToList();
                }
                else if (bottom.Where(x => x.Y == point.Y || x.Y == point.Y + 1).Select(x => x.X).Distinct().Count() == 7)
                {
                    bottom = bottom.Where(x => x.Y <= point.Y + 1).ToList();
                }
            }



            // The topmost bottom point always needs to be 3 below the lowest point on the next rock

            currRockCount++;
            if (currRockCount == maxRockCount)
            {
                break;
            }

            int index = (int)(currRockCount % 5);

            currRock = allRocks[index].Select(x => x).ToList();



            // not true Only need to keep topmost points
            //bottom = bottom.GroupBy(x => x.X).Select(x => new Point(x.Key, x.Min(y => y.Y))).ToList();
            // only keep points which aren't blocked off 

            // find min y value which has all x points
            //var minY = bottom.GroupBy(x => x.Y).Where(x => x.Count() == 7);
            //if (minY.Any())
            //{
            //    bottom = bottom.Where(x => x.Y <= minY.Min(x => x.Key)).ToList();
            //}

            // e.g. 2, 3 0> 6 3 algrothm     3 - (2 - 3) + 2
            // E.G. 4, 2  >5,2 algo Y +     3 - (X - Y) + X = 3 - x + y + x = 3 + y
            var minDiff = bottom.Select(x => x.Y).Min() - currRock.Select(x => x.Y).Max();
            if (minDiff < 4)
            {
                bottom = bottom.Select(x => new Point(x.X, x.Y + 4 - minDiff)).ToList();
                baseTracker += (4 - minDiff);
            }
            else
            {
                currRock = currRock.Select(x => new Point(x.X, x.Y + minDiff - 4)).ToList();
            }

            if (bottom.Count == 7)
            {
                Console.WriteLine("here");
            }

            //Console.Clear();
            //for (int i = 0; i < bottom.Select(x => x.Y).Max(); i++)
            //{
            //    var strToWrite = "";
            //    for (int j = 0; j < 7; j++)
            //    {
            //
            //        if (bottom.Any(x => x.X == j && x.Y == i))
            //        {
            //            strToWrite = strToWrite + '#';
            //        }
            //        else if (currRock.Any(x => x.X == j && x.Y == i))
            //        {
            //            strToWrite = strToWrite + '@';
            //        }
            //        else
            //        {
            //            strToWrite = strToWrite + '.';
            //        }
            //    }
            //
            //    Console.WriteLine(strToWrite);
            //}

        }
        else
        {
            currRock = currRock.Select(x => new Point(x.X, x.Y + 1)).ToList();
        }

    }
}
// -1 for the bottom layer we added initially
Console.WriteLine(baseTracker - bottom.Select(x => x.Y).Min());  // bottom.Select(x => x.Y).Max()