namespace Spellcast.CLI
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "Spellcast.CLI";
            
            Console.Write("Letters: ");
            var readInput = Console.ReadLine();

            var dictionary = new Core.Engine(5, 5);
            
            Console.WriteLine($"Best Word: {dictionary.GetBestWord(readInput!)}");

            Console.ReadLine();
        }
    }
}