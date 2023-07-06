namespace Spellcast.Core.Utility;

internal class Grid
{
    internal static char[,] ConvertToGrid(string input)
    {
        var grid = new char[Engine.Rows, Engine.Columns];
        var index = 0;

        for (var i = 0; i < Engine.Rows; i++)
        {
            for (var j = 0; j < Engine.Columns; j++)
            {
                if (index < input.Length)
                    grid[i, j] = input[index++];
                else
                    grid[i, j] = ' ';
            }
        }

        return grid;
    }
    
    internal static List<(int, int)> FindWordPositions(string word, char[,] grid)
    {
        var wordPositions = new List<(int, int)>();
        var wordLength = word.Length;

        for (var i = 0; i < Engine.Rows; i++)
        {
            for (var j = 0; j < Engine.Columns; j++)
            {
                if (grid[i, j] != word[0])
                    continue;

                var positions = new List<(int, int)>();
                if (!Word.CheckAdjacentCharacters(word, grid, i, j, wordLength, positions)) continue;
                wordPositions.AddRange(positions);
                return wordPositions;
            }
        }

        return wordPositions;
    }
}