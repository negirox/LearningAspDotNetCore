using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections.Generic;

[TestFixture]
public class EditDistanceTests
{
    [Test]
    public void TestBasicEditDistance()
    {
        Assert.That(Levenshtein.EditDistance("kitten", "sitting"), Is.EqualTo(3));
        Assert.That(Levenshtein.EditDistance("flaw", "lawn"), Is.EqualTo(2));
        Assert.That(Levenshtein.EditDistance("algorithm", "logarithm"), Is.EqualTo(3));
    }

    [Test]
    public void TestWeightedEditDistance()
    {
        Assert.That(Levenshtein.EditDistance("kitten", "sitting", 1, 2, 3), Is.EqualTo(7));
        Assert.That(Levenshtein.EditDistance("flaw", "lawn", 2, 2, 1), Is.EqualTo(4));
        Assert.That(Levenshtein.EditDistance("algorithm", "logarithm", 1, 3, 2), Is.EqualTo(6));
    }

    [Test]
    public void TestSpellCheckerSuggestions()
    {
        var dictionary = new List<string> { "cred", "bet", "shat", "that", "brad", "cart", "brat", "card" };
        var suggestions = Levenshtein.SuggestWords("dat", dictionary, 1, 1, 1);
        var expected = new List<string> { "bet", "shat", "that", "cart", "brat" };
        CollectionAssert.AreEquivalent(expected, suggestions);
    }
}
