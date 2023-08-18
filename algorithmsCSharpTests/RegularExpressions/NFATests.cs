using AlgorithmsCSharp.RegularExpressions;

namespace algorithmsCSharpTests.RegularExpressions
{
    [TestClass]
    public class NFATests
    {
        [TestMethod]
        public void TestNFABuild()
        {
            var regexp = "(.*AB((C|D*E)F)*G)";
            var nfa = new Nfa(regexp);
        }


        [TestMethod]
        public void TestNFARecognizes()
        {
            var regexp = "(.*AB((C|D*E)F)*G)";
            var nfa = new Nfa(regexp);
            var result = nfa.Recognizes("CCABDEFG");
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TestNFASpecifiedSet()
        {
            var regexp = "([AEIOU])*";
            var nfa = new Nfa(regexp);

            var list = new List<Tuple<string, bool>>
            {
                new("A", true),
                new("AAAE", true),
                new("AAAEEE", true),
                new("AAAEEEI", true),
            };

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }


        [TestMethod]
        public void TestNFARangeSet()
        {
            var regexp = "[A-G]";
            var nfa = new Nfa(regexp);

            var list = new List<Tuple<string, bool>>
            {
                new("C", true),
                new("CC", false),
            };

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }

        [TestMethod]
        public void TestNFARangeSetMany()
        {
            var regexp = "([A-G])*";
            var nfa = new Nfa(regexp);

            var list = new List<Tuple<string, bool>>
            {
                new("C", true),
                new("CC", true),
                new("ACCBBDDGG", true),
                new("Z", false),
            };

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }

        [TestMethod]
        public void TestNFAOneOrMore()
        {
            var regexp = "(A)+B";
            var nfa = new Nfa(regexp);

            var list = new List<Tuple<string, bool>>
            {
                new("AB", true),
                new("AAB", true),
                new("AAAA", false),
            };

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }

        [TestMethod]
        public void TestNFAOneOrs()
        {
            var regexp = "(AB|CD)";
            var nfa = new Nfa(regexp);

            var list = new List<Tuple<string, bool>>
            {
                new("AB", true),
                new("CD", true),
            };

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }

        [TestMethod]
        public void TestNFAManyOrs()
        {
            var regexp = "(AB|CD|EF|GH|IJ)";
            var nfa = new Nfa(regexp);

            var list = new List<Tuple<string, bool>>
            {
                new("AB", true),
                new("CD", true),
                new("EF", true),
                new("GH", true),
                new("IJ", true),
            };

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }
    }
}