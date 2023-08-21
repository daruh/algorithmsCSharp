using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmsCSharp.Tries;

namespace algorithmsCSharpTests.Tries
{
    [TestClass]
    public class TernaryTriesTest
    {
        [TestMethod]
        public void TestTrieBuild()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("are", 12),
                new("by", 4),
                new("sea", 14),
                new("shells", 15),
                new("she", 10),
                new("shore", 7),
                new("surely", 13),
                new("sure", 0),
                new("the", 8),
            };

            var trie = new TernaryTrie<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            foreach (var word in listOfWords)
            {
                var value = trie.Get(word.Item1);
                Assert.AreEqual(value, word.Item2);
            }
        }
    }
}