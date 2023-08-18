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
            var nfa =new Nfa(regexp);


        }


        [TestMethod]
        public void TestNFARecognizes()
        {
            var regexp = "(.*AB((C|D*E)F)*G)";
            var nfa = new Nfa(regexp);
            var result = nfa.Recognizes("CCABDEFG");
            Assert.IsTrue(result);
        }
    }
}
