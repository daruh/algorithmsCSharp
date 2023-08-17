using AlgorithmsCSharp.Graphs.Undirected.Readers;

namespace algorithmsCSharpTests.DigraphsTests
{
    [TestClass]
    public class DigraphsReadFileTests
    {
        [TestMethod]
        public void ReadDigraphFile()
        {
            var reader = new FileDigraphReader();
            var graph=reader.ReadFromFile("DigraphsData\\tinyDG.dat");
            Assert.AreEqual("13 verticies, 22 edges \r\n0: 1 5\r\n1: \r\n2: 3 0\r\n3: 2 5\r\n4: 2 3\r\n5: 4\r\n6: 0 4 9\r\n7: 8 6\r\n8: 9 7\r\n9: 10 11\r\n10: 12\r\n11: 12 4\r\n12: 9\r\n", graph.ToString());
        }


    }
}