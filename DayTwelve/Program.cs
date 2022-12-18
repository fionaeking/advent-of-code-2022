var puzzle = File.ReadAllLines("PuzzleInput.txt").Select(x => x.ToCharArray().Select(x => LowerCharToDigit(x)).ToArray()).ToArray();

// Tuple<int, int>? startPoint = null;
// to do error prone
var endPoint = new Tuple<int, int>(0, 0);

var options = new List<Location>();

for (int i = 0; i < puzzle.Length; i++)
{
    for (int j = 0; j < puzzle[i].Length; j++)
    {
        //if (startPoint is null)
        //{
        //    if (puzzle[i][j] == LowerCharToDigit('S'))
        //    {
        //        startPoint = new Tuple<int, int>(j, i);
        //        puzzle[i][j] = LowerCharToDigit('a');
        //    }
        //}

        if (puzzle[i][j] == LowerCharToDigit('E'))
        {
            endPoint = new Tuple<int, int>(j, i);
            puzzle[i][j] = LowerCharToDigit('z');
        }
        if (puzzle[i][j] == LowerCharToDigit('a'))
        {
            options.Add(new Location()
            {
                X = j,
                Y = i
            });
        }
    }
}


var chainCount = 0;
while (chainCount == 0)
{
    options = options.GroupBy(x => new { x.X, x.Y }).ToList().Select(x => new Location() { X = x.Key.X, Y = x.Key.Y, ChainCount = x.OrderBy(y => y.ChainCount).First().ChainCount }).ToList();
    //options = options.Select(x => new Location { X = x.X, Y = x.Y, ChainCount = x.ChainCount }).Distinct().ToList();
    var cloneOptions = new List<Location>(options);
    foreach (var option in cloneOptions)
    {
        if (endPoint.Item2 == option.Y && endPoint.Item1 == option.X)
        {
            Console.WriteLine($"Here! {option.X} {option.Y} {option.ChainCount} ");
            chainCount = option.ChainCount;
        }

        if ((option.X + 1) < puzzle[0].Length)
        {
            // right
            var right = puzzle[option.Y][option.X + 1];
            if (right <= puzzle[option.Y][option.X] + 1)
            {
                options.Add(new Location()
                {
                    X = option.X + 1,
                    Y = option.Y,
                    ChainCount = option.ChainCount + 1
                });
            }
        }
        if ((option.X - 1) >= 0)
        {
            // Left
            var left = puzzle[option.Y][option.X - 1];
            if (left <= puzzle[option.Y][option.X] + 1)
            {
                options.Add(new Location()
                {
                    X = option.X - 1,
                    Y = option.Y,
                    ChainCount = option.ChainCount + 1
                });
            }
        }

        if ((option.Y + 1) < puzzle.Length)
        {
            // down
            var down = puzzle[option.Y + 1][option.X];
            if (down <= puzzle[option.Y][option.X] + 1)
            {
                options.Add(new Location()
                {
                    X = option.X,
                    Y = option.Y + 1,
                    ChainCount = option.ChainCount + 1
                });
            }
        }
        if ((option.Y - 1) >= 0)
        {
            // up
            var up = puzzle[option.Y - 1][option.X];
            if (up <= puzzle[option.Y][option.X] + 1)
            {
                options.Add(new Location()
                {
                    X = option.X,
                    Y = option.Y - 1,
                    ChainCount = option.ChainCount + 1
                });
            }
        }


        options.Remove(option);
    }
}


Console.WriteLine(chainCount);


static int LowerCharToDigit(char character)
{
    if (character == 'S') character = 'a';
    return character - 96;
}

class Location
{
    /// TODO - better to have awareness of whether we are just going in circles or not! add previous mountain's coordinates
    public int X;
    public int Y;
    public int ChainCount = 0;
}