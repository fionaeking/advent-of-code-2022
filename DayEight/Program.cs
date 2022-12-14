var trees = File.ReadAllLines("PuzzleInput.txt").Select(x => x.ToCharArray().Select(x => Int32.Parse(x.ToString())).ToArray()).ToArray();
var count = new HashSet<Tuple<int, int>>();
count.UnionWith(GetVisibleTrees(trees));
count.UnionWith(GetVisibleTrees(ArrayExtensions<int>.Transpose(trees), true));
Console.WriteLine(count.Count);

// Part 2
var sumMax = 0;
for (int x = 1; x < trees.Length - 1; x++)
{
    // Ignore perimeter as will be 0

    for (int j = 1; j < trees.Length - 1; j++)
    {
        var centre = trees[x][j];

        // Left
        var leftIndex = x;
        var left = 0;
        while (leftIndex > 0)
        {
            left++;
            leftIndex--;
            if (trees[leftIndex][j] >= centre)
            {
                break;
            }
        }

        // Right
        var rightIndex = x;
        var right = 0;
        while (rightIndex + right < trees.Length - 1)
        {
            right++;
            rightIndex++;
            if (trees[rightIndex][j] >= centre)
            {
                break;
            }
        }
        // Up
        var upIndex = j;
        var up = 0;
        while (upIndex > 0)
        {
            up++;
            upIndex--;
            if (trees[x][upIndex] >= centre)
            {
                break;
            }
        }
        // Down
        var downIndex = j;
        var down = 0;
        while (downIndex < trees.Length - 1)
        {
            down++;
            downIndex++;
            if (trees[x][downIndex] >= centre)
            {
                break;
            }
        }

        var sum = down * up * left * right;
        if (sum > sumMax)
        {
            sumMax = sum;
        }
    }
}
Console.WriteLine($"Part two: {sumMax}");




// Part 1 - todo - alternative method - is tree surrounded by all taller trees? if so, ignore. Use part 2 above

static HashSet<Tuple<int, int>> GetVisibleTrees(int[][] trees, bool transposed = false)
{
    var countedTrees = new HashSet<Tuple<int, int>>();

    for (int i = 0; i < trees.Length; i++)
    {
        var max = -1;
        for (int j = 0; j < trees[i].Length; j++)
        {
            if (trees[i][j] > max)
            {
                max = trees[i][j];

                if (transposed)
                {
                    countedTrees.Add(new Tuple<int, int>(j, i));
                }
                else
                {
                    countedTrees.Add(new Tuple<int, int>(i, j));
                }
            }
        }

        max = -1;
        for (int j = trees[i].Length - 1; j >= 0; j--)
        {
            if (trees[i][j] > max)
            {
                max = trees[i][j];
                if (transposed)
                {
                    countedTrees.Add(new Tuple<int, int>(j, i));
                }
                else
                {
                    countedTrees.Add(new Tuple<int, int>(i, j));
                }
            }
        }
    }
    return countedTrees;
}
