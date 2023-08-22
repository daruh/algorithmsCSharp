using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
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

        [TestMethod]
        public void TestTrieSizeManyBranch()
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

            var value = trie.GetTreeSize();
            Assert.AreEqual(listOfWords.Count, value);
        }

        [TestMethod]
        public void TestTrie2Branches()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("shells", 15),
                new("shellsware", 15),
                new("shellsing", 15),
            };

            var trie = new TernaryTrie<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            var value = trie.GetTreeSize();
            Assert.AreEqual(listOfWords.Count, value);
        }

        [TestMethod]
        public void TestTrieDeletePostFix()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("shells", 15),
                new("shellsware", 15),
                new("shellsing", 15),
            };

            var trie = new TernaryTrie<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            trie.Delete("shellsware");
            trie.Delete("shellsing");
            trie.Delete("shells");

            var value = trie.Keys();
            Assert.AreEqual(0, value.Count());
        }

        [TestMethod]
        public void TestTrieDeleteRandom()
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

            trie.Delete("shells");
            trie.Delete("she");
            trie.Delete("shore");
            trie.Delete("are");
            trie.Delete("by");
            trie.Delete("sea");
            trie.Delete("surely");
            trie.Delete("the");
            trie.Delete("sure");

            var value = trie.Keys();
            Assert.AreEqual(0, value.Count());
        }

        [TestMethod]
        public void TestTrieMinimum()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("surely", 13),
                new("shells", 15),
                new("by", 4),
                new("sea", 14),
                new("she", 10),
                new("shore", 7),
                new("sure", 0),
                new("the", 8),
                new("are", 12),
            };

            var trie = new TernaryTrie<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            var minKey = trie.MinKey();

            Assert.AreEqual("are", minKey);
        }

        [TestMethod]
        public void TestTrieMaximum()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("surely", 13),
                new("shells", 15),
                new("by", 4),
                new("sea", 14),
                new("she", 10),
                new("shore", 7),
                new("sure", 0),
                new("the", 8),
                new("are", 12),
            };

            var trie = new TernaryTrie<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            var minKey = trie.MaxKey();

            Assert.AreEqual("the", minKey);
        }


        [TestMethod]
        public void TestSelectTrenary()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("surely", 13),
                new("sells", 15),
                new("shells", 19),
                new("by", 4),
                new("sea", 14),
                new("she", 10),
                new("shore", 7),
                new("sure", 0),
                new("the", 8),
                new("are", 12),
            };

            var trie = new TernaryTrie<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            for (int i = 0; i < listOfWords.Count; i++)
            {
                var value = trie.Select(i);
                Assert.IsTrue(listOfWords.Exists(a => a.Item1 == value), $"Faile index {i}");
            }
        }

        [TestMethod]
        public void TestTrenaryIndexOf()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("surely", 13),
                new("sells", 15),
                new("shells", 19),
                new("by", 4),
                new("sea", 14),
                new("she", 10),
                new("shore", 7),
                new("sure", 0),
                new("the", 8),
                new("are", 12),
            };

            var trie = new TernaryTrie<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }


            var value = trie.IndexOf("are");
            Assert.AreEqual(0, value);
        }

        [TestMethod]
        public void TestTrieLongestPrefix1()
        {
            var listOfWords = new List<Tuple<string, int>>()
            {
                new("shells", 15),
                new("shellsware", 15),
                new("shellsing", 15),
            };

            var trie = new TernaryTrie<object>();

            foreach (var val in listOfWords)
            {
                trie.Put(val.Item1, val.Item2);
            }

            var value = trie.LongestPrefixOf("shellswa");
            Assert.AreEqual("shells", value);
        }

        [TestMethod]
        public void TestTrieWithPrefixBuild()
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

            var keysWithPrefix = trie.KeysWithPrefix("sure");

            CollectionAssert.AreEquivalent(new List<string>() { "surely", "sure" }, keysWithPrefix.ToList());
        }


        [TestMethod]
        public void TestTrieWithPrefixBuild2()
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

            var keysWithPrefix = trie.KeysWithPrefix("sh");

            CollectionAssert.AreEquivalent(new List<string>() { "shells", "she", "shore" }, keysWithPrefix.ToList());
        }


        [TestMethod]
        public void TestTrieKeys()
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

            var keysWithPrefix = trie.Keys();

            CollectionAssert.AreEquivalent(
                new List<string>() { "are", "by", "sea", "shells", "she", "shore", "surely", "sure", "the" },
                keysWithPrefix.ToList());
        }
    }
}