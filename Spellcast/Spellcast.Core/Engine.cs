using System.Reflection;
using System.Resources;
using Spellcast.Core.Utility;

namespace Spellcast.Core
{
    public class Engine
    {
        internal static List<string>? WordList { get; private set; }
        internal static int Rows { get; set; }
        internal static int Columns { get; set; }

        public Engine(int rows, int columns)
        {
            WordList = GetAllLinesFromEmbeddedResource("Spellcast.Core.WordList").ToList();
            Rows = rows;
            Columns = columns;
        }

        public List<string> GetWords(string input)
        {
            var grid = Grid.ConvertToGrid(input);
            var words = WordList!.Where(word => Utility.Word.CanFormWord(word, grid)).ToList();
            return words;
        }

        public string GetBestWord(string input)
        {
            var words = GetWords(input);
            var bestWord = string.Empty;
            var highestPoints = 0;

            foreach (var word in words)
            {
                var points = Point.CalculateWordPoints(word);
                if (points <= highestPoints) continue;
                highestPoints = points;
                bestWord = word;
            }

            return bestWord;
        }

        public List<(int, int)> GetWordPosition(string input, string word)
        {
            var grid = Grid.ConvertToGrid(input);
            return Grid.FindWordPositions(word, grid);
        }

        private static IEnumerable<string> GetAllLinesFromEmbeddedResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new ArgumentException($"Embedded resource '{resourceName}' not found.");
            }

            using var reader = new StreamReader(stream);
            while (reader.ReadLine() is { } line)
            {
                yield return line;
            }
        }
    }
}