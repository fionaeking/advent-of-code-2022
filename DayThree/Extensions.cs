namespace DayThree
{
    public static class Extensions
    {
        public static char Invert(this char c)
        {
            return char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c);
        }

        public static IEnumerable<List<T>> Partition<T>(this List<T> inputList, int listSize)
        {
            for (int i = 0; i < inputList.Count; i += listSize)
            {
                yield return inputList.GetRange(i, Math.Min(listSize, inputList.Count - i));
            }
        }

        public static char MostCommonChar(this IEnumerable<char> chars)
        {
            return chars.GroupBy(x => x)
                      .OrderByDescending(x => x.Count())
                      .First().Key;
        }
    }

}