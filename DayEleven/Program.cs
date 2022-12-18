using System.Data.SqlTypes;
using System.Numerics;

var monkeyList = new List<Monkey>();

foreach (var block in File.ReadAllText("PuzzleInput.txt").Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
{
    var m = new Monkey();
    foreach (var line in block.Split(Environment.NewLine).Select(x => x.Trim()))
    {
        if (line.StartsWith("Monkey"))
        {
            m.Id = int.Parse((line.Split(" ")[1]).Replace(":", ""));
        }
        else if (line.StartsWith("Starting items"))
        {
            m.StartingItems = line.Split(":")[1].Split(", ").Select(x => BigInteger.Parse(x.Trim())).ToList();
        }
        else if (line.StartsWith("Operation"))
        {
            var a = line.Split("Operation: new = old")[1].Trim();
            m.Operation = new Operation()
            {
                Name = a.Split(" ")[0] == "*" ? Operator.Multiply : Operator.Add,
                Value = a.Split(" ")[1]
            };
        }
        else if (line.StartsWith("Test: divisible by"))
        {
            m.Test = int.Parse(line.Split("Test: divisible by")[1].Trim());
        }
        else if (line.StartsWith("If true"))
        {
            m.TrueCondition = int.Parse(line.Split("If true: throw to monkey")[1].Trim());
        }
        else if (line.StartsWith("If false"))
        {
            m.FalseCondition = int.Parse(line.Split("If false: throw to monkey")[1].Trim());
        }
    }
    monkeyList.Add(m);
}


var numRounds = 1000; // 2000;
for (int i = 0; i < numRounds; i++)
{
    foreach (var monkey in monkeyList)
    {
        // if (monkey.StartingItems.Count == 0)
        // {
        //     continue;
        // }
        monkey.ItemsInspected += monkey.StartingItems.Count;
        foreach (var b in monkey.StartingItems)
        {
            var operationValue = (monkey.Operation.Value == "old") ? b : int.Parse(monkey.Operation.Value);
            BigInteger worryLevel = ((monkey.Operation.Name == Operator.Multiply) ? b * operationValue : b + operationValue); // / 3;
            worryLevel %= (2 * 3 * 5 * 7 * 11 * 13 * 17 * 19);
            if (worryLevel % monkey.Test == 0)
            {
                monkeyList.First(x => x.Id == monkey.TrueCondition).StartingItems.Add(worryLevel);
            }
            else
            {

                monkeyList.First(x => x.Id == monkey.FalseCondition).StartingItems.Add(worryLevel);
            }
        }

        monkey.StartingItems.Clear();
    }
}

// Need to deal with overflow

var monkeyBusiness = monkeyList.Select(x => x.ItemsInspected).OrderByDescending(x => x).Take(2).ToList().Aggregate((x, y) => x * y);
Console.WriteLine(monkeyBusiness);



// Is 21 + 11 divible by 4



class Monkey
{
    public int Id;
    public List<BigInteger> StartingItems;
    public Operation Operation;
    public int Test;
    public int TrueCondition;
    public int FalseCondition;
    public long ItemsInspected = 0;

}

class Operation
{
    public Operator Name;
    public string Value;
}

public enum Operator
{
    Add,
    Multiply,
}