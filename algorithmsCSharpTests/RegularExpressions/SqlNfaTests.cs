using AlgorithmsCSharp.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace algorithmsCSharpTests.RegularExpressions
{
    [TestClass]
    public class SqlNfaTests
    {

        private void TestSqlNfa(string regexp, List<Tuple<string, bool>> list)
        {
            var nfa = new SqlNfa(regexp);

            foreach (var t in list)
            {
                var result = nfa.Recognizes(t.Item1);
                Assert.AreEqual(t.Item2, result);
            }
        }

        [TestMethod]
        public void TestEmpty()
        {
            var regexp = "";
            var list = new List<Tuple<string, bool>>
            {
                new("", true),
            };
            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestWideSpace()
        {
            var regexp = " ";
            var list = new List<Tuple<string, bool>>
            {
                new(" ", true),
            };
            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestSqlNFAAnyTwoChars()
        {
            var regexp = "__";
            var list = new List<Tuple<string, bool>>
            {
                new("CC", true),
                new("cc", true),
                new("AB", true),
                new("EF", true),
            };
            TestSqlNfa(regexp, list);
        }


        [TestMethod]
        public void TestUnderscoreAndAnyOf()
        {
            var regexp = "_[abc]";

            var list = new List<Tuple<string, bool>>
            {
                new("gb", true),
                new("ca", true),
                new("cb", true),
                new("cc", true),
            };

            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestUnderscoreAndRange()
        {
            var regexp = "_[a-c]";

            var list = new List<Tuple<string, bool>>
            {
                new("gb", true),
                new("ca", true),
                new("cb", true),
                new("cc", true),
            };

            TestSqlNfa(regexp, list);

        }

        [TestMethod]
        public void TestPercentSign1()
        {
            var regexp = "%";

            var list = new List<Tuple<string, bool>>
            {
                new("gb", true),
                // new("ca", true),
                // new("cb", true),
                // new("cc", true),
            };

            TestSqlNfa(regexp, list);

        }

        [TestMethod]
        public void TestPercentSign()
        {
            var regexp = "_%";

            var list = new List<Tuple<string, bool>>
            {
                new("gb", true),
                new("ca", true),
                new("cb", true),
                new("cc", true),
            };
            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestPercentWithLetters()
        {
            var regexp = "abb%le";
            var list = new List<Tuple<string, bool>>
            {
                 new("able", true),
                new("abbbbbble", true),
            };
            TestSqlNfa(regexp, list);
            
        }

        [TestMethod]
        public void TestPercentWithBetweenLetters()
        {
            var regexp = "a%c%d%g";

            var list = new List<Tuple<string, bool>>
            {
                new("abcdefghlmbng", true),
            };

            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestUnderscoreBetweenLetters()
        {
            var regexp = "a_c_e_g";

            var list = new List<Tuple<string, bool>>
            {
                new("abcdefg", true),
            };

            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestRange()
        {
            var regexp = "a[b-l]";

            var list = new List<Tuple<string, bool>>
            {
                new("ag", true),
            };

            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestAnyOfWithMultiByteChars()
        {
            var regexp = "a[ao]";

            var list = new List<Tuple<string, bool>>
            {
                new("ao", true),
            };
            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestNotAnyOf()
        {
            var regexp = "[^bc]ar";

            var list = new List<Tuple<string, bool>>
            {
                new("par", true),
            };
            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestAnyWildcardInsideGroup()
        {
            var regexp = "[%_]";

            var list = new List<Tuple<string, bool>>
            {
                new("_", true),
            };
            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestNotAnyWildcardInsideGroup()
        {
            var regexp = "[^%_]";

            var list = new List<Tuple<string, bool>>
            {
                new("*", true),
            };
            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestStartsWithMultiWildcardUnderscore()
        {
            var regexp = "[bb]";

            var list = new List<Tuple<string, bool>>
            {
                new("b", true),
            };

            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestStartsWithMultiWildcardGroupWithDoubleLettersInGroup()
        {
            var regexp = "%[b]";

            var list = new List<Tuple<string, bool>>
            {
                new("ac", false),
            };
            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestStartsWithMultiWildcardBackwardNotGroupMatch()
        {
            var regexp = "%[^b]";

            var list = new List<Tuple<string, bool>>
            {
                new("ab", false),
            };
            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestGroupTest()
        {
            var regexp = "[b^]";

            var list = new List<Tuple<string, bool>>
            {
                new("b", true),
            };
            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestMatchStrWithPatternTest()
        {
            var regexp = "b^";

            var list = new List<Tuple<string, bool>>
            {
                new("b^", true),
            };

            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestPercentFirstAndAfter()
        {
            var regexp = "%[a-z]%";

            var list = new List<Tuple<string, bool>>
            {
                new("b", true),
            };
            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestMinimumTwoLetters()
        {
            var regexp = "%_[x-z]";

            var list = new List<Tuple<string, bool>>
            {
                new("bx", true),
            };
            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        public void TestDash()
        {
            var regexp = "[-]";

            var list = new List<Tuple<string, bool>>
            {
                new("-", true),
            };
            TestSqlNfa(regexp, list);
        }

        [TestMethod]
        [Ignore]
        public void TestAnyofIncludingNotAnyOf()
        {
            var regexp = "a^b";

            var list = new List<Tuple<string, bool>>
            {
                new("^", true),
            };
            TestSqlNfa(regexp, list);
        }
    }
}
