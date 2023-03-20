using System.Text.RegularExpressions;

// TODO this code is horrible and doesn't even work yet!

File.ReadAllLines("PuzzleInput.txt");

var oreRegex = new Regex(@"Each\sore\srobot\scosts\s(?<ore_cost>\d+)\sore");
var clayRegex = new Regex(@"Each\sclay\srobot\scosts\s(?<ore_cost>\d+)\sore");
var obsidianRegex = new Regex(@"Each\sobsidian\srobot\scosts\s(?<ore_cost>\d+)\sore\sand\s(?<clay_cost>\d+)\sclay");
var geodeRegex = new Regex(@"Each\sgeode\srobot\scosts\s(?<ore_cost>\d+)\sore\sand\s(?<obs_cost>\d+)\sobsidian");

var allBlueprints = new List<Blueprint>();
foreach (var line in File.ReadAllLines("PuzzleInput.txt"))
{
    var g = line.Split(":")[1].Split('.');
    var robots = new List<Robot>();
    foreach (var b in g)
    {
        if (oreRegex.IsMatch(b))
        {
            var matches = oreRegex.Match(b.Trim()).Groups;
            robots.Add(new Robot()
            {
                Type = RobotType.Ore,
                OreCost = int.Parse(matches["ore_cost"].Value)
            });
        }
        else if (clayRegex.IsMatch(b))
        {
            var matches = clayRegex.Match(b.Trim()).Groups;
            robots.Add(new Robot()
            {
                Type = RobotType.Clay,
                OreCost = int.Parse(matches["ore_cost"].Value)
            });
        }
        else if (obsidianRegex.IsMatch(b))
        {
            var matches = obsidianRegex.Match(b.Trim()).Groups;
            robots.Add(new Robot()
            {
                Type = RobotType.Obsidian,
                OreCost = int.Parse(matches["ore_cost"].Value),
                ClayCost = int.Parse(matches["clay_cost"].Value)
            });
        }
        else if (geodeRegex.IsMatch(b))
        {
            var matches = geodeRegex.Match(b.Trim()).Groups;
            robots.Add(new Robot()
            {
                Type = RobotType.Geode,
                OreCost = int.Parse(matches["ore_cost"].Value),
                ObsidianCost = int.Parse(matches["obs_cost"].Value)
            });
        }
    }
    allBlueprints.Add(new Blueprint()
    {
        Id = int.Parse(line.Split(":")[0].Split(" ")[1].Trim()),
        Robots = robots
    });
}


foreach (var blueprint in allBlueprints)
{
    var minutes = 0;
    var oreCount = 0;
    var clayCount = 0;
    var obsCount = 0;
    var geodeCount = 0;
    var currRobots = new List<RobotType>() { RobotType.Ore };

    while (minutes <= 24)
    {
        var robotsToAdd = new List<RobotType>();
        foreach (var availableRobot in blueprint.Robots)
        {
            if (availableRobot.OreCost <= oreCount && availableRobot.ClayCost <= clayCount && availableRobot.ObsidianCost <= obsCount)
            {
                robotsToAdd.Add(availableRobot.Type);
                oreCount -= availableRobot.OreCost;
                clayCount -= availableRobot.ClayCost;
                obsCount -= availableRobot.ObsidianCost;
            }
        }

        foreach (var robot in currRobots)
        {
            if (robot == RobotType.Ore) oreCount++;
            else if (robot == RobotType.Clay) clayCount++;
            else if (robot == RobotType.Obsidian) obsCount++;
            else if (robot == RobotType.Geode) geodeCount++;
        }

        currRobots.AddRange(robotsToAdd);

        minutes++;
    }
    Console.WriteLine($"{blueprint.Id}: {geodeCount}");
}



class Blueprint
{
    public int Id;
    public List<Robot> Robots = new();
}

class Robot
{
    public RobotType Type;
    public int OreCost = 0;
    public int ClayCost = 0;
    public int ObsidianCost = 0;
}

enum RobotType
{
    Ore,
    Clay,
    Obsidian,
    Geode
}