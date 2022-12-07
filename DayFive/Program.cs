using System.Text.RegularExpressions;

var input = File.ReadAllLines("PuzzleInput.txt");
var moveInstructions = ParseMoveInstructions(input);

/* Part one */
// Read in data
var cargoStack = ParseCargoStack(input);
// Move
foreach (var instruction in moveInstructions)
{
    for (int i = 0; i < instruction.QuantityToMove; i++)
    {
        cargoStack[instruction.MoveTo].Push(cargoStack[instruction.MoveFrom].Pop());
    }
}
// Print answer
var answer = "";
foreach ((var k, var stack) in cargoStack)
{
    answer += stack.Pop();
}
Console.WriteLine($"Part one: {answer}");

/* Part two */
// Read in data
var cargoList = ParseCargoToList(input);
// Move
foreach (var instruction in moveInstructions)
{
    cargoList[instruction.MoveTo].AddRange(cargoList[instruction.MoveFrom].TakeLast(instruction.QuantityToMove));
    cargoList[instruction.MoveFrom].RemoveRange((cargoList[instruction.MoveFrom].Count - instruction.QuantityToMove), instruction.QuantityToMove);
}
// Print answer
Console.WriteLine($"Part two: {string.Join("", cargoList.Values.Select(x => x.Last()))}");


Dictionary<int, Stack<string>> ParseCargoStack(string[] input)
{
    var cargoRaw = input.Where(x => !x.StartsWith("move") && !string.IsNullOrEmpty(x)).Reverse().ToArray();
    var cargoStack = new Dictionary<int, Stack<string>>();
    for (int i = 1; i < cargoRaw[0].Length; i += 4)
    {

        var f = cargoRaw.Select(x => x[i].ToString()).ToList();
        cargoStack.Add(int.Parse(f.First().ToString()), new Stack<string>(f.Skip(1).Where(x => x != " ")));
    }
    return cargoStack;
}

Dictionary<int, List<string>> ParseCargoToList(string[] input)
{
    var cargoRaw = input.Where(x => !x.StartsWith("move") && !string.IsNullOrEmpty(x)).Reverse().ToArray();
    var cargoStack = new Dictionary<int, List<string>>();
    for (int i = 1; i < cargoRaw[0].Length; i += 4)
    {

        var f = cargoRaw.Select(x => x[i].ToString()).ToList();
        cargoStack.Add(int.Parse(f.First().ToString()), f.Skip(1).Where(x => x != " ").ToList());
    }
    return cargoStack;
}


IEnumerable<MoveModel> ParseMoveInstructions(string[] input)
{
    var regex = new Regex(@"move\s(?<quantity_to_move>\d+)\sfrom\s(?<move_from>\d+)\sto\s(?<move_to>\d+)");
    return input.Where(x => x.StartsWith("move")).Select(x => regex.Match(x)).Select(x => new MoveModel()
    {
        QuantityToMove = int.Parse(x.Groups["quantity_to_move"].Value),
        MoveFrom = int.Parse(x.Groups["move_from"].Value),
        MoveTo = int.Parse(x.Groups["move_to"].Value)
    });
}