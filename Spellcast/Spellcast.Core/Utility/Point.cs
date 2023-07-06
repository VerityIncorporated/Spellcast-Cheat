namespace Spellcast.Core.Utility;

internal class Point
{
    internal static int CalculateWordPoints(string word)
    {
        var points = 0;

        foreach (var letter in word)
        {
            if (PointsDictionary.TryGetValue(letter, out var value))
            {
                points += value;
            }
        }

        if (word.Length >= 6)
        {
            points += 10;
        }

        return points;
    }

    private static readonly Dictionary<char, int> PointsDictionary = new()
    {
        { 'a', 1 },
        { 'e', 1 },
        { 'i', 1 },
        { 'o', 1 },
        { 'n', 2 },
        { 'r', 2 },
        { 's', 2 },
        { 't', 2 },
        { 'd', 3 },
        { 'g', 3 },
        { 'l', 3 },
        { 'b', 4 },
        { 'h', 4 },
        { 'p', 4 },
        { 'm', 4 },
        { 'u', 4 },
        { 'y', 4 },
        { 'c', 5 },
        { 'f', 5 },
        { 'v', 5 },
        { 'w', 5 },
        { 'k', 6 },
        { 'j', 7 },
        { 'x', 7 },
        { 'q', 8 },
        { 'z', 8 }
    };
}