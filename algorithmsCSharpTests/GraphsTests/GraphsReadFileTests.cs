using AlgorithmsCSharp.Graphs.Undirected.Readers;

namespace algorithmsCSharpTests.GraphsTests
{
    [TestClass]
    public class GraphsReadFileTests
    {
        [TestMethod]
        public void ReadGraphFile()
        {
            var reader = new FileGraphReader();
            var graph=reader.ReadFromFile("GraphsData\\tinyG.dat");
            Assert.AreEqual("13 verticies, 13 edges \r\n0: 5 1 2 6\r\n1: 0\r\n2: 0\r\n3: 4 5\r\n4: 3 6 5\r\n5: 0 4 3\r\n6: 4 0\r\n7: 8\r\n8: 7\r\n9: 12 10 11\r\n10: 9\r\n11: 12 9\r\n12: 9 11\r\n",graph.ToString());
        }


    }
}