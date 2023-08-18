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
                new ("A", true),
                new("AAAE",true),
                new("AAAEEE",true),
                new("AAAEEEI",true),
            };

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }
    }
}