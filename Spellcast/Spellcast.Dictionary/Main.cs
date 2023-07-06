using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Spellcast.Dictionary
{
    public class Main
    {
        private List<string> Dictionary { get; set; }
        private int Rows { get; set; }
        private int Columns { get; set; }
        private Dictionary<char, int> PointsDictionary { get; set; }

        public Main(string dictionary, int rows, int columns)
        {
            Dictionary = File.ReadAllLines(dictionary).ToList();
            Rows = rows;
            Columns = columns;
            PointsDictionary = CreatePointsDictionary();
        }

        public List<string> GetWords(string input)
        {
            var grid = ConvertToGrid(input);
            var words = Dictionary.Where(word => CanFormWord(word, grid)).ToList();
            return words;
        }

        public string GetBestWord(string input)
        {
            var words = GetWords(input);
            var bestWord = string.Empty;
            var highestPoints = 0;

            foreach (var word in words)
            {
                var points = CalculateWordPoints(word);
                if (points <= highestPoints) continue;
                highestPoints = points;
                bestWord = word;
            }

            return bestWord;
        }
        
        

        private bool CheckAdjacentCharacters(string word, char[,] grid, int row, int column, int remainingLength, List<(int, int)> positions)
        {
            if (remainingLength == 0)
            {
                return true;
            }

            if (row < 0 || row >= Rows || column < 0 || column >= Columns)
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

        public List<(int, int)> FindWordPositions(string word, char[,] grid)
        {
            var wordPositions = new List<(int, int)>();
            var wordLength = word.Length;

            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    if (grid[i, j] != word[0])
                        continue;

                    var positions = new List<(int, int)>();
                    if (!CheckAdjacentCharacters(word, grid, i, j, wordLength, positions)) continue;
                    wordPositions.AddRange(positions);
                    return wordPositions;
                }
            }

            return wordPositions;
        }

        public char[,] ConvertToGrid(string input)
        {
            var grid = new char[Rows, Columns];
            var index = 0;

            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    if (index < input.Length)
                        grid[i, j] = input[index++];
                    else
                        grid[i, j] = ' ';
                }
            }

            return grid;
        }

        private bool CanFormWord(string word, char[,] grid)
        {
            var wordLength = word.Length;

            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
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

        private static Dictionary<char, int> CreatePointsDictionary()
        {
            var pointsDictionary = new Dictionary<char, int>
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

            return pointsDictionary;
        }

        private int CalculateWordPoints(string word)
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
    }
}