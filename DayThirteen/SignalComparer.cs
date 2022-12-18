using Newtonsoft.Json.Linq;

namespace DayThirteen
{
    class SignalComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            var arrayA = ParseInputString(x);
            var arrayB = ParseInputString(y);
            for (int i = 0; i < arrayA.Length; i++)
            {
                if (i > arrayB.Length - 1)
                {
                    return 1;
                }
                var result = CompareTokens(arrayA[i], arrayB[i]);
                if (result is not null) return result == true ? -1 : 1;
            }
            return (arrayA.Length <= arrayB.Length) ? -1 : 1;
        }

        static JToken[] ParseInputString(string inputString)
        {
            return JObject.Parse("{ \"Data\":" + inputString + "}").First.First.Children().ToArray();
        }

        bool? CompareTokens(JToken a, JToken b)
        {
            if (a.Type == JTokenType.Integer && b.Type == JTokenType.Integer)
            {
                if ((int)a == (int)b) return null;
                else return (int)a < (int)b;
            }
            else if (a.Type == JTokenType.Array && b.Type == JTokenType.Array)
            {
                for (int i = 0; i < a.Count(); i++)
                {
                    if (i >= (b as JArray).Count) return false;
                    var result = CompareTokens((a as JArray)[i], (b as JArray)[i]);
                    if (result is not null)
                    {
                        return result;
                    }
                }
                if ((a as JArray).Count < (b as JArray).Count) return true;
                return null;
            }
            else if (a.Type == JTokenType.Array && b.Type == JTokenType.Integer)
            {
                if (!(a as JArray).Any()) return true;
                var m = JToken.FromObject(new List<int>() { (int)b });
                return CompareTokens((a as JArray), m);
            }
            else if (a.Type == JTokenType.Integer && b.Type == JTokenType.Array)
            {
                if (!(b as JArray).Any()) return false;
                var m = JToken.FromObject(new List<int>() { (int)a });
                return CompareTokens(m, (b as JArray));
            }
            else
            {
                throw new Exception();
            }
        }

    }
}

