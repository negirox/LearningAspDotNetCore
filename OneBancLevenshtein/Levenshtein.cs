using System;
using System.Collections.Generic;
using System.Linq;

public class Levenshtein
{
    /// <summary>
    /// Calculates the Levenshtein distance between two strings.
    /// Task 1: Implement the Levenshtein Algorithm
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public static int EditDistance(string s1, string s2)
    {
        int m = s1.Length, n = s2.Length;
        int[,] dp = new int[m + 1, n + 1];

        for (int i = 0; i <= m; i++)
            dp[i, 0] = i;

        for (int j = 0; j <= n; j++)
            dp[0, j] = j;

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                int cost = s1[i - 1] == s2[j - 1] ? 0 : 1;
                dp[i, j] = Math.Min(Math.Min(
                    dp[i - 1, j] + 1,
                    dp[i, j - 1] + 1),
                    dp[i - 1, j - 1] + cost
                );
            }
        }

        return dp[m, n];
    }

    /// <summary>
    /// Calculates the weighted Levenshtein distance between two strings.
    /// Task 2: Minimum Weighted Edit Distance Calculation
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <param name="Ci"></param>
    /// <param name="Cd"></param>
    /// <param name="Cs"></param>
    /// <returns></returns>
    public static int EditDistance(string s1, string s2, int Ci, int Cd, int Cs)
    {
        int m = s1.Length, n = s2.Length;
        int[,] dp = new int[m + 1, n + 1];

        for (int i = 0; i <= m; i++)
            dp[i, 0] = i * Cd;

        for (int j = 0; j <= n; j++)
            dp[0, j] = j * Ci;

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                int cost = s1[i - 1] == s2[j - 1] ? 0 : Cs;
                dp[i, j] = Math.Min(Math.Min(
                    dp[i - 1, j] + Cd,
                    dp[i, j - 1] + Ci),
                    dp[i - 1, j - 1] + cost
                );
            }
        }

        return dp[m, n];
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
        if (s1.Length < s2.Length)
        {
            var temp = s1;
            s1 = s2;
            s2 = temp;
        }

        int[] prev = new int[s2.Length + 1];
        int[] curr = new int[s2.Length + 1];

        for (int j = 0; j <= s2.Length; j++)
            prev[j] = j;

        for (int i = 1; i <= s1.Length; i++)
        {
            curr[0] = i;
            for (int j = 1; j <= s2.Length; j++)
            {
                int cost = s1[i - 1] == s2[j - 1] ? 0 : 1;
                curr[j] = Math.Min(Math.Min(
                    curr[j - 1] + 1,
                    prev[j] + 1),
                    prev[j - 1] + cost
                );
            }
            var tmp = prev;
            prev = curr;
            curr = tmp;
        }

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
        List<(string word, int distance)> results = new List<(string word, int distance)>();

        foreach (var word in dictionary)
        {
            int dist = EditDistance(input, word, Ci, Cd, Cs);
            results.Add((word, dist));
        }

        int minDist = results.Min(r => r.distance);
        return results.Where(r => r.distance == minDist).Select(r => r.word).ToList();
    }
}