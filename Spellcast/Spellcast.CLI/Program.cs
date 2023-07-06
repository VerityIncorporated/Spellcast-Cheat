namespace Spellcast.CLI
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "Spellcast.CLI";
            
            Console.Write("Dictionary Path: ");
            var dictPath = Console.ReadLine();
            
            Console.Write("Letters: ");
            var readInput = Console.ReadLine();

            var dictionary = new Dictionary.Main(dictPath!, 5, 5);
            
            Console.WriteLine($"Best Word: {dictionary.GetBestWord(readInput!)}");

            Console.ReadLine();
        }
    }
}