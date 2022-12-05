public static class InvertCharExtension
{
    public static char Invert(this char c)
    {
        if (!char.IsLetter(c))
            return c;

        return char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c);
    }
}