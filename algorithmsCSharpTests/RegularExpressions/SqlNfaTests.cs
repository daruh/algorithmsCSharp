using AlgorithmsCSharp.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithmsCSharpTests.RegularExpressions
{
    [TestClass]
    public class SqlNfaTests
    {
        [TestMethod]
        public void TestSqlNFAAnyTwoChars()
        {
            var regexp = "__";
            var nfa = new SqlNfa(regexp);

            var list = new List<Tuple<string, bool>>
            {
                new("CC", true),
                new("cc", true),
                new("AB", true),
                new("EF", true),
            };

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }


        [TestMethod]
        public void TestUnderscoreAndAnyOf()
        {
            var regexp = "_[abc]";
            var nfa = new SqlNfa(regexp);

            var list = new List<Tuple<string, bool>>
            {
                new("gb", true),
                new("ca", true),
                new("cb", true),
                new("cc", true),
            };

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }

        [TestMethod]
        public void TestUnderscoreAndRange()
        {
            var regexp = "_[a-c]";
            var nfa = new SqlNfa(regexp);

            var list = new List<Tuple<string, bool>>
            {
                new("gb", true),
                new("ca", true),
                new("cb", true),
                new("cc", true),
            };

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }

        [TestMethod]
        public void TestPercentSign1()
        {
            var regexp = "%";
            var nfa = new SqlNfa(regexp);

            var list = new List<Tuple<string, bool>>
            {
                new("gb", true),
                // new("ca", true),
                // new("cb", true),
                // new("cc", true),
            };

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }

        [TestMethod]
        public void TestPercentSign()
        {
            var regexp = "_%";
            var nfa = new SqlNfa(regexp);

            var list = new List<Tuple<string, bool>>
            {
                new("gb", true),
                new("ca", true),
                new("cb", true),
                new("cc", true),
            };

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }

        [TestMethod]
        public void TestPercentWithLetters()
        {
            var regexp = "abb%le";
            var nfa = new SqlNfa(regexp);

            var list = new List<Tuple<string, bool>>
            {
                 new("able", true),
                new("abbbbbble", true),
            };

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }

        [TestMethod]
        public void TestPercentWithBetweenLetters()
        {
            var regexp = "a%c%d%g";
            var nfa = new SqlNfa(regexp);

            var list = new List<Tuple<string, bool>>
            {
                new("abcdefghlmbng", true),
            };

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }
    }
}
