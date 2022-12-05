using DayTwo;

var models = new List<RPSModel>()
{
    new RPSModel()
    {
        Type = RPSType.Rock,
        LosesTo = RPSType.Paper,
        WinsAgainst = RPSType.Scissors,
        Score = 1,
        MapsFromOpponent = InputOptions.A,
        MapsFromYou = InputOptions.X
    },
    new RPSModel()
    {
        Type = RPSType.Paper,
        LosesTo = RPSType.Scissors,
        WinsAgainst = RPSType.Rock,
        Score = 2,
        MapsFromOpponent =InputOptions.B,
        MapsFromYou = InputOptions.Y
    },
    new RPSModel()
    {
        Type = RPSType.Scissors,
        LosesTo = RPSType.Rock,
        WinsAgainst = RPSType.Paper,
        Score = 3,
        MapsFromOpponent = InputOptions.C,
        MapsFromYou = InputOptions.Z
    },
};


const int winScore = 6;
const int drawScore = 3;

var partOneScore = 0;
var partTwoScore = 0;
foreach (var line in File.ReadAllLines("PuzzleInput.txt"))
{
    var split = line.Split(" ", StringSplitOptions.TrimEntries);
    if (split.Length != 2)
    {
        Console.WriteLine($"Unexpected line: {line}");
        continue;
    }
    var opponent = models.First(x => x.MapsFromOpponent == (InputOptions)Enum.Parse(typeof(InputOptions), split[0]));
    var you = models.First(x => x.MapsFromYou == (InputOptions)Enum.Parse(typeof(InputOptions), split[1]));

    // Rock defeats Scissors, Scissors defeats Paper, and Paper defeats Rock. 
    partOneScore += PartOne(opponent, you);
    partTwoScore += PartTwo(opponent, you);
}

Console.WriteLine($"Final score for part 1: {partOneScore}");
Console.WriteLine($"Final score for part 2: {partTwoScore}");


int PartOne(RPSModel opponent, RPSModel you)
{
    var score = you.Score;
    if (opponent.LosesTo == you.Type)
    {
        score += winScore;
    }
    else if (opponent.Type == you.Type)
    {
        score += drawScore;
    }
    return score;
}

int PartTwo(RPSModel opponent, RPSModel you)
{
    var score = 0;
    if (you.MapsFromYou == InputOptions.X)
    {
        score += models.First(x => x.Type == opponent.WinsAgainst).Score;
    }
    else if (you.MapsFromYou == InputOptions.Y)
    {
        score += drawScore;
        score += models.First(x => x.Type == opponent.Type).Score;
    }
    else
    {
        score += winScore;
        score += models.First(x => x.Type == opponent.LosesTo).Score;
    }
    return score;
}
