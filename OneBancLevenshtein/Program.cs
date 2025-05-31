using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        using (var writer = new StreamWriter("output_log.txt"))
        {

            writer.WriteLine("== Basic Edit Distance ==");
            writer.WriteLine(Levenshtein.EditDistance("kitten", "sitting"));  // 3
            writer.WriteLine(Levenshtein.EditDistance("flaw", "lawn"));      // 2
            writer.WriteLine(Levenshtein.EditDistance("algorithm", "logarithm")); // 3

            writer.WriteLine("\n== Weighted Edit Distance ==");
            writer.WriteLine(Levenshtein.EditDistance("kitten", "sitting", 1, 2, 3)); // 7
            writer.WriteLine(Levenshtein.EditDistance("flaw", "lawn", 2, 2, 1));      // 4
            writer.WriteLine(Levenshtein.EditDistance("algorithm", "logarithm", 1, 3, 2)); // 6

            writer.WriteLine("\n== Spell Checker Suggestions ==");
            var dictionary = new List<string> { "cred", "bet", "shat", "that", "brad", "cart", "brat", "card" };
            var suggestions = Levenshtein.SuggestWords("dat", dictionary, 1, 1, 1);
            writer.WriteLine(string.Join(", ", suggestions)); // "bet", "shat", "that", "cart", "brat"
            
        }
        Console.WriteLine("Output written to output_log.txt");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}