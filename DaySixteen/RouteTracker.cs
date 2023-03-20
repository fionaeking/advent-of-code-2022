namespace DaySixteen
{
    class RouteTracker
    {
        public List<ValveInfo> CurrValves { get; set; }
        public int Sum { get; set; } = 0;
        public HashSet<KeyValuePair<string, int>> OpenedValves = new();
    }
}
