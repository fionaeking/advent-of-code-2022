using System.Text.RegularExpressions;

var regex = new Regex(@"Valve (?<valve>\w+) has flow rate=(?<flow_rate>\d+); tunnels? leads? to valves? (?<tunnel_valves>.*)");

var rawValves = File.ReadAllLines("PuzzleInput.txt")
    .Select(x => regex.Match(x).Groups)
    .Select(x => new ValveInfo()
    {
        Value = x["valve"].Value,
        FlowRate = int.Parse(x["flow_rate"].Value),
        TunnelValves = x["tunnel_valves"].Value.Split(", ").ToList()
    });

var currentMin = 1;

var firstValve = rawValves.First(x => x.Value == "AA");


var v = new List<Test>
{
    new Test() { Valve = firstValve, OpenedValves = rawValves.Where(x => x.FlowRate == 0).Select(x => new KeyValuePair<string, int>(x.Value, x.FlowRate)).ToHashSet() }
};

var nextValves = new List<Test>();

var maxFlowRate = rawValves.Select(x => x.FlowRate).Sum();

while (currentMin <= 30)
{
    foreach (var currentValve in v)
    {
        if (currentValve.OpenedValves.Select(x => x.Value).Sum() == maxFlowRate)
        {
            nextValves.Add(new Test()
            {
                Valve = currentValve.Valve,
                Sum = currentValve.Sum + currentValve.OpenedValves.Select(x => x.Value).Sum(),
                OpenedValves = currentValve.OpenedValves
            });
            continue;
        }

        if (!currentValve.OpenedValves.Select(x => x.Key).Contains(currentValve.Valve.Value))
        {
            var newOpenedValves = new HashSet<KeyValuePair<string, int>>();
            foreach (var b in currentValve.OpenedValves)
            {
                newOpenedValves.Add(b);
            }
            newOpenedValves.Add(new KeyValuePair<string, int>(currentValve.Valve.Value, currentValve.Valve.FlowRate));
            nextValves.Add(new Test()
            {
                Valve = currentValve.Valve,
                Sum = currentValve.Sum + currentValve.OpenedValves.Select(x => x.Value).Sum(),
                OpenedValves = newOpenedValves
            });

            // Also model the case where we didn't open it and just moved to the next stage
        }
        foreach (var g in rawValves.Where(x => currentValve.Valve.TunnelValves.Contains(x.Value)))
        {
            nextValves.Add(new Test()
            {
                Valve = g,
                Sum = currentValve.Sum + currentValve.OpenedValves.Select(x => x.Value).Sum(),
                OpenedValves = currentValve.OpenedValves
            });
        }
    }

    v = nextValves.Select(x => new Test()
    {
        Valve = x.Valve,
        Sum = x.Sum,
        OpenedValves = x.OpenedValves
    }).GroupBy(x => x.Valve.Value).Select(x => x.OrderByDescending(y => y.Sum).First())
    .ToList();
    nextValves.Clear();
    currentMin++;
}
Console.WriteLine(v.Select(x => x.Sum).Max());

class ValveInfo
{
    public List<string> TunnelValves { get; set; } = new List<string>();
    public string Value { get; set; }
    public int FlowRate { get; set; }
}

//class ValveTwo : CommonValve
//{
//    public List<Guid> TunnelValves { get; set; } = new List<Guid>();
//
//}

class Test
{
    public ValveInfo Valve { get; set; }
    public int Sum { get; set; } = 0;
    public HashSet<KeyValuePair<string, int>> OpenedValves = new HashSet<KeyValuePair<string, int>>();
}