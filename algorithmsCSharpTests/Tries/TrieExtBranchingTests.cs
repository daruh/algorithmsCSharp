using AlgorithmsCSharp.Tries;

namespace algorithmsCSharpTests.Tries
{
    [TestClass]
    public class TrieExtBranchingTests
    {
        [TestMethod]
        public void TestTrieBuild()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("shells", 1),
                new("shellfish", 2),
            };

            var trie = new TrieExtBranching<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }
            
        }
    }
}
