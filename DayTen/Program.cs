var types = new List<CycleTime>()
{
    new CycleTime()
    {
        Instruction = CpuInstruction.noop,
        Time = 1
    },
    new CycleTime()
    {
        Instruction = CpuInstruction.addx,
        Time = 2
    }
};

var cycle = 0;
var registerValue = 1;
var nextCycleReading = 20;
var sum = 0;
foreach (var i in File.ReadAllLines("PuzzleInput.txt")
    .Select(x => new InstructionValue()
    {
        Name = (CpuInstruction)Enum.Parse(typeof(CpuInstruction), x.Split(" ")[0]),
        Value = (x.Split(" ").Length > 1) ? int.Parse(x.Split(" ")[1]) : 0
    }))
{
    var time = types.First(x => x.Instruction == i.Name).Time;
    cycle += time;

    if (cycle >= nextCycleReading)
    {
        sum += nextCycleReading * registerValue;
        nextCycleReading += 40;
    }

    if (i.Name == CpuInstruction.addx)
    {
        registerValue += i.Value;
    }

}
Console.WriteLine(sum);
