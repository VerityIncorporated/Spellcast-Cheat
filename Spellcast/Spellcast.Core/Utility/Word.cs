namespace Spellcast.Core.Utility;

internal class Word
{
    internal static bool CheckAdjacentCharacters(string word, char[,] grid, int row, int column, int remainingLength, List<(int, int)> positions)
    {
        if (remainingLength == 0)
        {
            return true;
        }

        if (row < 0 || row >= Engine.Rows || column < 0 || column >= Engine.Columns)
        {
            return false;
        }

        if (grid[row, column] != word[^remainingLength])
            return false;

        positions.Add((row, column));

        var temp = grid[row, column];
        grid[row, column] = '#';

        var result =
            CheckAdjacentCharacters(word, grid, row - 1, column, remainingLength - 1, positions) || // Up
            CheckAdjacentCharacters(word, grid, row + 1, column, remainingLength - 1, positions) || // Down
            CheckAdjacentCharacters(word, grid, row, column - 1, remainingLength - 1, positions) || // Left
            CheckAdjacentCharacters(word, grid, row, column + 1, remainingLength - 1, positions) || // Right
            CheckAdjacentCharacters(word, grid, row - 1, column - 1, remainingLength - 1, positions) || // Diagonal Up-Left
            CheckAdjacentCharacters(word, grid, row - 1, column + 1, remainingLength - 1, positions) || // Diagonal Up-Right
            CheckAdjacentCharacters(word, grid, row + 1, column - 1, remainingLength - 1, positions) || // Diagonal Down-Left
            CheckAdjacentCharacters(word, grid, row + 1, column + 1, remainingLength - 1, positions); // Diagonal Down-Right

        grid[row, column] = temp;

        if (!result)
        {
            positions.RemoveAt(positions.Count - 1);
        }

        return result;
    }
    
    internal static bool CanFormWord(string word, char[,] grid)
    {
        var wordLength = word.Length;

        for (var i = 0; i < Engine.Rows; i++)
        {
            for (var j = 0; j < Engine.Columns; j++)
            {
                if (grid[i, j] != word[0])
                    continue;

                if (CheckAdjacentCharacters(word, grid, i, j, wordLength, new List<(int, int)>()))
                {
                    return true;
                }
            }
        }

        return false;
    }
}