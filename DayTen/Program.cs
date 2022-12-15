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
var nextNewline = 40;
var sum = 0;

const int crt = 40;

var answer = "";

foreach (var instruction in File.ReadAllLines("PuzzleInput.txt")
    .Select(x => new InstructionValue()
    {
        Name = (CpuInstruction)Enum.Parse(typeof(CpuInstruction), x.Split(" ")[0]),
        Value = (x.Split(" ").Length > 1) ? int.Parse(x.Split(" ")[1]) : 0
    }))
{
    for (int i = 0; i < types.First(x => x.Instruction == instruction.Name).Time; i++)
    {
        if (registerValue - 1 <= (cycle % crt) && (cycle % crt) <= registerValue + 1)
        {
            answer += "#";
        }
        else
        {
            answer += ".";
        }
        cycle++;

        if (cycle == nextNewline)
        {
            answer += "\n";
            nextNewline += crt;
        }
    }


    if (cycle >= nextCycleReading)
    {
        sum += nextCycleReading * registerValue;
        nextCycleReading += 40;
    }

    if (instruction.Name == CpuInstruction.addx)
    {
        registerValue += instruction.Value;
    }

}
Console.WriteLine(sum);

Console.WriteLine(answer);
