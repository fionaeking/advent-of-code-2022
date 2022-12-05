// TODO - very messy and inefficient!

var rpsMapping = new Dictionary<RPS, int>()
{
    { RPS.X, 1},
    { RPS.Y, 2},
    { RPS.Z, 3},
};

const int winScore = 6;
const int drawScore = 3;

var partOneScore = 0;
var PartTwoScore = 0;
foreach (var line in File.ReadAllLines("PuzzleInput.txt"))
{
    var split = line.Split(" ", StringSplitOptions.TrimEntries);
    if (split.Length != 2)
    {
        Console.WriteLine($"Unexpected line: {line}");
        continue;
    }
    var opponent = (RPS)Enum.Parse(typeof(RPS), split[0]);
    var you = (RPS)Enum.Parse(typeof(RPS), split[1]);

    // Rock defeats Scissors, Scissors defeats Paper, and Paper defeats Rock. 
    partOneScore += PartOne(opponent, you);
    PartTwoScore += PartTwo(opponent, you);
}

Console.WriteLine($"Final score: {partOneScore}");
Console.WriteLine($"Final score: {PartTwoScore}");


int PartOne(RPS opponent, RPS you)
{
    var score = rpsMapping[you];
    if (opponent == RPS.A)
    {
        if (you == RPS.Y)
        {
            score += winScore;
        }
        else if (you == RPS.X)
        {
            score += drawScore;
        }
    }
    else if (opponent == RPS.B)
    {
        if (you == RPS.Z)
        {
            score += winScore;
        }
        else if (you == RPS.Y)
        {
            score += drawScore;
        }
    }
    else
    {
        if (you == RPS.X)
        {
            score += winScore;
        }
        else if (you == RPS.Z)
        {
            score += drawScore;
        }
    }
    return score;
}

int PartTwo(RPS opponent, RPS you)
{
    var score = 0;
    if (you == RPS.X)
    {
        //Lose
        if (opponent == RPS.A)
        {
            // Rock - to lose, need to use scissors
            score += rpsMapping[RPS.Z];
        }
        else if (opponent == RPS.B)
        {
            score += rpsMapping[RPS.X];
        }
        else
        {
            score += rpsMapping[RPS.Y];
        }
    }
    else if (you == RPS.Y)
    {
        //Draw
        score += drawScore;
        if (opponent == RPS.A)
        {
            score += rpsMapping[RPS.X];
        }
        else if (opponent == RPS.B)
        {
            score += rpsMapping[RPS.Y];
        }
        else
        {
            score += rpsMapping[RPS.Z];
        }
    }
    else
    {
        //Win
        score += winScore;
        if (opponent == RPS.A)
        {
            score += rpsMapping[RPS.Y];
        }
        else if (opponent == RPS.B)
        {
            score += rpsMapping[RPS.Z];
        }
        else
        {
            score += rpsMapping[RPS.X];
        }
    }
    return score;
}


enum RPS
{
    A,
    B,
    C,
    X,
    Y,
    Z
}