using DaySixteen;
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

var v = new HashSet<RouteTracker>
{
    new RouteTracker() { CurrValves = new List<ValveInfo>(){firstValve, firstValve } }
};

var nextValves = new List<RouteTracker>();

var maxFlowRate = rawValves.Select(x => x.FlowRate).Sum();

const int totalMins = 26; // 30;

var finished = false;

while (currentMin <= totalMins)
{
    foreach (var currentValve in v)
    {
        // 4 cases
        // 3. Human moves, elephant opens tunnel
        // 4. Elephant moves, human opens tunnel
        // 2. Human and elephant both move
        // 1. Human and elephant both open tunnel

        // CASE 1
        var newOpenedValves = new HashSet<KeyValuePair<string, int>>();
        foreach (var b in currentValve.OpenedValves)
        {
            newOpenedValves.Add(b);
        }

        foreach (var b in currentValve.CurrValves)
        {
            if (!currentValve.OpenedValves.Select(x => x.Key).Contains(b.Value) && b.FlowRate != 0)
            {
                newOpenedValves.Add(new KeyValuePair<string, int>(b.Value, b.FlowRate));
            }
        }
        if (newOpenedValves.Count == currentValve.OpenedValves.Count + 2 || newOpenedValves.Select(x => x.Value).Sum() == maxFlowRate)
            nextValves.Add(new RouteTracker()
            {
                CurrValves = currentValve.CurrValves,
                Sum = currentValve.Sum + currentValve.OpenedValves.Select(x => x.Value).Sum(),
                OpenedValves = newOpenedValves
            });

        // CASE 2
        foreach (var g in rawValves.Where(x => currentValve.CurrValves[0].TunnelValves.Contains(x.Value)))
        {
            foreach (var h in rawValves.Where(x => currentValve.CurrValves[1].TunnelValves.Contains(x.Value)))
            {
                if (!currentValve.OpenedValves.Select(x => x.Key).ToList().Contains(g.Value) && !currentValve.OpenedValves.Select(x => x.Key).ToList().Contains(g.Value))
                    nextValves.Add(new RouteTracker()
                    {
                        CurrValves = new List<ValveInfo> { g, h },
                        Sum = currentValve.Sum + currentValve.OpenedValves.Select(x => x.Value).Sum(),
                        OpenedValves = currentValve.OpenedValves
                    });
            }
            if (!currentValve.OpenedValves.Select(x => x.Key).ToList().Contains(g.Value))
                nextValves.Add(new RouteTracker()
                {
                    CurrValves = new List<ValveInfo> { g, currentValve.CurrValves[1] },
                    Sum = currentValve.Sum + currentValve.OpenedValves.Select(x => x.Value).Sum(),
                    OpenedValves = currentValve.OpenedValves
                });
        }

        foreach (var h in rawValves.Where(x => currentValve.CurrValves[1].TunnelValves.Contains(x.Value)))
        {
            if (!currentValve.OpenedValves.Select(x => x.Key).ToList().Contains(h.Value))
                nextValves.Add(new RouteTracker()
                {
                    CurrValves = new List<ValveInfo>() { currentValve.CurrValves[0], h },
                    Sum = currentValve.Sum + currentValve.OpenedValves.Select(x => x.Value).Sum(),
                    OpenedValves = currentValve.OpenedValves
                });
        }

        // CASE 3 - Human moves, elephant opens tunnel
        newOpenedValves = new HashSet<KeyValuePair<string, int>>();
        foreach (var b in currentValve.OpenedValves)
        {
            newOpenedValves.Add(b);
        }
        if (!currentValve.OpenedValves.Select(x => x.Key).Contains(currentValve.CurrValves[1].Value) && currentValve.CurrValves[1].FlowRate != 0)
        {
            newOpenedValves.Add(new KeyValuePair<string, int>(currentValve.CurrValves[1].Value, currentValve.CurrValves[1].FlowRate));
        }
        foreach (var nextValveHuman in rawValves.Where(x => currentValve.CurrValves[0].TunnelValves.Contains(x.Value)))
        {
            if (!currentValve.OpenedValves.Select(x => x.Key).ToList().Contains(nextValveHuman.Value))
                nextValves.Add(new RouteTracker()
                {
                    CurrValves = new List<ValveInfo> { nextValveHuman, currentValve.CurrValves[1] },
                    Sum = currentValve.Sum + currentValve.OpenedValves.Select(x => x.Value).Sum(),
                    OpenedValves = newOpenedValves
                });
        }

        // Case 4
        newOpenedValves = new HashSet<KeyValuePair<string, int>>();
        foreach (var b in currentValve.OpenedValves)
        {
            newOpenedValves.Add(b);
        }
        if (!currentValve.OpenedValves.Select(x => x.Key).Contains(currentValve.CurrValves[0].Value) && currentValve.CurrValves[0].FlowRate != 0)
        {
            newOpenedValves.Add(new KeyValuePair<string, int>(currentValve.CurrValves[0].Value, currentValve.CurrValves[0].FlowRate));
        }
        foreach (var nextValveElephant in rawValves.Where(x => currentValve.CurrValves[1].TunnelValves.Contains(x.Value)))
        {
            if (!currentValve.OpenedValves.Select(x => x.Key).ToList().Contains(nextValveElephant.Value))
                nextValves.Add(new RouteTracker()
                {
                    CurrValves = new List<ValveInfo> { currentValve.CurrValves[0], nextValveElephant },
                    Sum = currentValve.Sum + currentValve.OpenedValves.Select(x => x.Value).Sum(),
                    OpenedValves = newOpenedValves
                });
        }
    }

    var minsRemaining = totalMins - currentMin;

    if (nextValves.Any(x => x.OpenedValves.Select(x => x.Value).Sum() == maxFlowRate))
    {
        var max = nextValves.Where(x => x.OpenedValves.Select(x => x.Value).Sum() == maxFlowRate).Select(x => x.Sum).Max();
        if (max >= nextValves.Select(x => x.Sum).Max())
        {
            Console.WriteLine(max + (maxFlowRate * minsRemaining));
            finished = true;
            break;
        }
    }

    const int takeMax = 90000;
    if (nextValves.Count > takeMax)
    {
        var maxCurrSum = v.Select(x => x.Sum).Max();
        var maxCurrFlowRate = v.First(x => x.Sum == maxCurrSum).OpenedValves.Select(x => x.Value).Sum();
        var maxTotal = maxCurrSum + maxCurrFlowRate * minsRemaining;

        v = nextValves.Select(x => new RouteTracker()
        {
            CurrValves = x.CurrValves,
            Sum = x.Sum,
            OpenedValves = x.OpenedValves
        })
        .Where(x => x.Sum + maxFlowRate * (minsRemaining) >= maxTotal)
     .GroupBy(x => x.CurrValves.Select(x => x.Value).OrderBy(x => x)).Select(x => x.OrderByDescending(y => y.OpenedValves.Select(x => x.Value).Sum()).First())
     .OrderByDescending(x => x.OpenedValves.Select(x => x.Value).Sum()).Take(takeMax).ToHashSet();
    }
    else
    {
        v = nextValves.ToHashSet();
    }

    nextValves.Clear();
    currentMin++;
}
if (!finished) Console.WriteLine(v.Select(x => x.Sum).Max());