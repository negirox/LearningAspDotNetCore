using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

public class EditDistanceBenchmark
{
    private string s1 = "kitten";
    private string s2 = "sitting";

    [Benchmark]
    public int BasicEditDistance() => Levenshtein.EditDistance(s1, s2);

    [Benchmark]
    public int WeightedEditDistance() => Levenshtein.EditDistance(s1, s2, 1, 2, 3);

    [Benchmark]
    public int OptimizedEditDistance() => Levenshtein.EditDistanceOptimized(s1, s2);
}
