using AlgorithmsCSharp.Graphs.Undirected.Readers;
using AlgorithmsCSharp.Graphs.Undirected.Searches;

namespace algorithmsCSharpTests.GraphsTests
{
    [TestClass]
    public class DepthFirstSearchTests
    {
        [TestMethod]
        public void TestSimpleDepthFirstSearch()
        {
            var fileRead = new FileGraphReader();
            var graph = fileRead.ReadFromFile("GraphsData\\tinyCG.dat");
            var dfs = new DepthFirstSearch(graph, 0);
            Assert.AreEqual(6, dfs.Count());
            for (int i = 0; i < 6; i++)
            {
                Assert.IsTrue(dfs.IsMarked(i)); 
            }
           
        }
    }
}