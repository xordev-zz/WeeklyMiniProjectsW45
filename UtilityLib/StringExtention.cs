namespace UtilityLib;

public static class StringExtention
{
    public static string Reverse(string str)
    {
        char[] charArray = str.ToCharArray();
        Array.Reverse(charArray);
        return new String(charArray);
    }
    public static int Lenght(string str)
    {
        int charCount = 0;

        foreach (var c in str)
        {
            charCount++;
        }

        return charCount;
    }
}
