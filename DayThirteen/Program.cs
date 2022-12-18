using Newtonsoft.Json.Linq;

var index = 0;
var sum = 0;
foreach (var pair in File.ReadAllText("PuzzleInput.txt").Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
{
    index++;

    if (CompareJTokenArrays(ParseInputString(pair.Split(Environment.NewLine)[0]),
        ParseInputString(pair.Split(Environment.NewLine)[1])))
    {
        sum += index;
    }

}
Console.WriteLine(sum);

bool CompareJTokenArrays(JToken[] arrayA, JToken[] arrayB)
{
    for (int i = 0; i < arrayA.Length; i++)
    {
        if (i > arrayB.Length - 1)
        {
            return false;
        }
        var result = CompareTokens(arrayA[i], arrayB[i]);
        if (result is not null) return (bool)result;
    }
    return arrayA.Length <= arrayB.Length;
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
        for (int l = 0; l < a.Count(); l++)
        {
            var result = CompareTokens((a as JArray)[l], b);
            if (result is not null) return result;
        }
        return null;
    }
    else if (a.Type == JTokenType.Integer && b.Type == JTokenType.Array)
    {
        if (!(b as JArray).Any()) return false;
        for (int l = 0; l < b.Count(); l++)
        {
            var result = CompareTokens(a, (b as JArray)[l]);
            if (result is not null) return result;
        }
        return null;
    }
    else
    {
        throw new Exception();
    }
}

JToken[] ParseInputString(string inputString)
{
    return JObject.Parse("{ \"Data\":" + inputString + "}").First.First.Children().ToArray();
}
