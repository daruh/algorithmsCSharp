using AlgorithmsCSharp.Tries;

namespace algorithmsCSharpTests.Tries
{
    [TestClass]
    public class TriesStTests
    {
        [TestMethod]
        public void TestTrieBuild()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("she", 3),
                new("sells", 5),
                new("shells", 6),
                new("by", 2),
                new("the", 3),
                new("shore", 5),
            };

            var trie = new TrieSt<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            Assert.AreEqual(3, trie.Get("she"));
            Assert.AreEqual(5, trie.Get("sells"));
            Assert.AreEqual(6, trie.Get("shells"));
            Assert.AreEqual(2, trie.Get("by"));
            Assert.AreEqual(3, trie.Get("the"));
            Assert.AreEqual(5, trie.Get("shore"));
        }

        [TestMethod]
        public void TestTrieSize()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("she", 3),
                new("sells", 5),
                new("shells", 6),
                new("by", 2),
                new("the", 3),
                new("shore", 5),
            };

            var trie = new TrieSt<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            Assert.AreEqual(6, trie.Size());
        }

        [TestMethod]
        public void TestTriePrefixes()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("she", 3),
                new("sells", 5),
                new("shells", 6),
                new("by", 2),
                new("the", 3),
                new("shore", 5),
            };

            var trie = new TrieSt<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            var prefixesList = trie.KeysWithPrefix("");
            CollectionAssert.AreEquivalent(new List<string>() { "she", "sells", "shells", "by", "the", "shore" },
                prefixesList.ToList());
        }

        [TestMethod]
        public void TestAllKeysTrie()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("she", 3),
                new("sells", 5),
                new("shells", 6),
                new("by", 2),
                new("the", 3),
                new("shore", 5),
            };

            var trie = new TrieSt<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            var prefixesList = trie.AllKeys();
            CollectionAssert.AreEquivalent(new List<string>() { "she", "sells", "shells", "by", "the", "shore" },
                prefixesList.ToList());
        }

        [TestMethod]
        public void TestLongestPrefixes()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("she", 3),
                new("sells", 5),
                new("shells", 6),
                new("by", 2),
                new("the", 3),
                new("shore", 5),
            };

            var trie = new TrieSt<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            var longestPrefix = trie.LongestPrefixOf("shell");
            Assert.AreEqual("she", longestPrefix);
        }

        [TestMethod]
        public void TestDeleteKey()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("she", 3),
                new("sells", 5),
                new("shells", 6),
                new("by", 2),
                new("the", 3),
                new("shore", 5),
            };

            var trie = new TrieSt<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            var key = "she";

            Assert.IsNotNull(trie.Get(key));
            trie.Delete(key);
            Assert.IsNull(trie.Get(key));
        }

        [TestMethod]
        public void TestSelectKey()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("she", 3),
                new("sells", 5),
                new("shells", 6),
                new("by", 2),
                new("the", 3),
                new("shore", 5),
            };

            var trie = new TrieSt<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            for (int i = 0; i < listOfWords.Count; i++)
            {
                var select = trie.Select(i);
                CollectionAssert.Contains(listOfWords.Select(t => t.Item1).ToList(), select);
            }
        }

        [TestMethod]
        public void TestIndexOfKey()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("she", 3),
                new("sells", 5),
                new("shells", 6),
                new("by", 2),
                new("the", 3),
                new("shore", 5),
            };

            var trie = new TrieSt<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }


            foreach (var t in listOfWords)
            {
                var index = trie.IndexOf(t.Item1);
                Assert.IsTrue(index < listOfWords.Count);
            }
        }

        [TestMethod]
        public void TestMinimumKey()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("she", 3),
                new("sells", 5),
                new("shells", 6),
                new("by", 2),
                new("the", 3),
                new("shore", 5),
            };

            var trie = new TrieSt<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            var minKey = trie.MinKey();
            Assert.AreEqual("by", minKey);
        }

        [TestMethod]
        public void TestMaximumKey()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("she", 3),
                new("sells", 5),
                new("shells", 6),
                new("by", 2),
                new("the", 3),
                new("shore", 5),
            };

            var trie = new TrieSt<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            var minKey = trie.MaxKey();
            Assert.AreEqual("the", minKey);
        }

        [TestMethod]
        public void TestDeleteMinKey()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("she", 3),
                new("sells", 5),
                new("shells", 6),
                new("by", 2),
                new("the", 3),
                new("shore", 5),
            };

            var trie = new TrieSt<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            Assert.AreEqual(listOfWords.Count, trie.Size());
            trie.DeleteMinKey();
            Assert.AreEqual(listOfWords.Count-1, trie.Size());
        }

        [TestMethod]
        public void TestDeleteMaxKey()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("she", 3),
                new("sells", 5),
                new("shells", 6),
                new("by", 2),
                new("the", 3),
                new("shore", 5),
            };

            var trie = new TrieSt<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            Assert.AreEqual(listOfWords.Count, trie.Size());
            trie.DeleteMaxKey();
            Assert.AreEqual(listOfWords.Count - 1, trie.Size());
        }
    }
}