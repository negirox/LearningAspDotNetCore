using System;
using System.Collections.Generic;
using System.Linq;

public class Levenshtein
{
    /// <summary>
    /// Calculates the Levenshtein distance between two strings.
    /// Task 1: Implement the Levenshtein Algorithm
    /// </summary>
    /// <param name="s1">first string</param>
    /// <param name="s2">second string</param>
    /// <returns></returns>
    public static int EditDistance(string s1, string s2)
    {
        int m = s1.Length, n = s2.Length; // Get the lengths of both input strings
        int[,] dp = new int[m + 1, n + 1]; // Create a 2D array to store edit distances

        // Initialize the first column (converting s1[0..i] to an empty string requires i deletions)
        for (int i = 0; i <= m; i++)
            dp[i, 0] = i;

        // Initialize the first row (converting an empty string to s2[0..j] requires j insertions)
        for (int j = 0; j <= n; j++)
            dp[0, j] = j;

        // Fill the rest of the dp table
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                // If characters are the same, no cost; otherwise, cost is 1 (substitution)
                int cost = s1[i - 1] == s2[j - 1] ? 0 : 1;

                // Take the minimum of:
                // 1. Deletion (dp[i-1, j] + 1)
                // 2. Insertion (dp[i, j-1] + 1)
                // 3. Substitution or match (dp[i-1, j-1] + cost)
                dp[i, j] = Math.Min(
                    Math.Min(
                        dp[i - 1, j] + 1,      // Deletion
                        dp[i, j - 1] + 1       // Insertion
                    ),
                    dp[i - 1, j - 1] + cost   // Substitution or match
                );
            }
        }

        return dp[m, n]; // The bottom-right cell contains the final edit distance
    }

    /// <summary>
    /// Calculates the weighted Levenshtein distance between two strings.
    /// Task 2: Minimum Weighted Edit Distance Calculation
    /// Ci: Cost of insertion, Cd: Cost of deletion, Cs: Cost of substitution.
    /// </summary>
    public static int EditDistance(string s1, string s2, int Ci, int Cd, int Cs)
    {
        int m = s1.Length, n = s2.Length; // Get lengths of both strings
        int[,] dp = new int[m + 1, n + 1]; // Create DP table for storing distances

        // Initialize first column: cost of deleting all characters from s1
        for (int i = 0; i <= m; i++)
            dp[i, 0] = i * Cd;

        // Initialize first row: cost of inserting all characters to s1 to get s2
        for (int j = 0; j <= n; j++)
            dp[0, j] = j * Ci;

        // Fill the DP table
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                // If characters match, no substitution cost; else, use Cs
                int cost = s1[i - 1] == s2[j - 1] ? 0 : Cs;

                // Take the minimum of:
                // 1. Deletion (move up, add Cd)
                // 2. Insertion (move left, add Ci)
                // 3. Substitution or match (move diagonally, add cost)
                dp[i, j] = Math.Min(
                    Math.Min(
                        dp[i - 1, j] + Cd,      // Deletion
                        dp[i, j - 1] + Ci       // Insertion
                    ),
                    dp[i - 1, j - 1] + cost    // Substitution or match
                );
            }
        }

        return dp[m, n]; // The bottom-right cell contains the minimum weighted edit distance
    }

    /// <summary>
    /// Calculates the Levenshtein distance using an optimized space complexity approach.
    /// Task 4: Optimization Task (Bonus)
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public static int EditDistanceOptimized(string s1, string s2)
    {
        // Ensure s1 is the longer string to minimize space usage
        if (s1.Length < s2.Length)
        {
            var temp = s1;
            s1 = s2;
            s2 = temp;
        }

        // Allocate two arrays to represent the previous and current rows of the DP table
        int[] prev = new int[s2.Length + 1];
        int[] curr = new int[s2.Length + 1];

        // Initialize the first row: converting an empty string to s2[0..j] requires j insertions
        for (int j = 0; j <= s2.Length; j++)
            prev[j] = j;

        // Fill the DP rows one by one
        for (int i = 1; i <= s1.Length; i++)
        {
            curr[0] = i; // First column: converting s1[0..i] to empty string requires i deletions
            for (int j = 1; j <= s2.Length; j++)
            {
                // If characters match, no cost; otherwise, cost is 1 (substitution)
                int cost = s1[i - 1] == s2[j - 1] ? 0 : 1;
                // Take the minimum of deletion, insertion, and substitution
                curr[j] = Math.Min(
                    Math.Min(
                        curr[j - 1] + 1,    // Insertion
                        prev[j] + 1         // Deletion
                    ),
                    prev[j - 1] + cost     // Substitution or match
                );
            }
            // Swap prev and curr for the next iteration (no need to copy arrays)
            var tmp = prev;
            prev = curr;
            curr = tmp;
        }

        // The last value in prev contains the final edit distance
        return prev[s2.Length];
    }

    /// <summary>
    /// Suggests words from a dictionary based on the minimum weighted edit distance to an input string.
    /// Task 3: Apply It to a Real Problem
    /// </summary>
    /// <param name="input"></param>
    /// <param name="dictionary"></param>
    /// <param name="Ci"></param>
    /// <param name="Cd"></param>
    /// <param name="Cs"></param>
    /// <returns></returns>
    public static List<string> SuggestWords(string input, List<string> dictionary, int Ci, int Cd, int Cs)
    {
        // Create a list to store each word and its edit distance to the input
        List<(string word, int distance)> results = new List<(string word, int distance)>();

        // For each word in the dictionary
        foreach (var word in dictionary)
        {
            // Calculate the weighted edit distance between the input and the current word
            int dist = EditDistance(input, word, Ci, Cd, Cs);
            // Add the word and its distance to the results list
            results.Add((word, dist));
        }

        // Find the minimum edit distance among all dictionary words
        int minDist = results.Min(r => r.distance);

        // Return all words from the dictionary that have the minimum edit distance
        return results.Where(r => r.distance == minDist).Select(r => r.word).ToList();
    }

}