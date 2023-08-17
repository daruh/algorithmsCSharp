using AlgorithmsCSharp.Graphs.Directed.Searches;
using AlgorithmsCSharp.Graphs.Undirected.Readers;
using AlgorithmsCSharp.Graphs.Undirected.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithmsCSharpTests.DigraphsTests
{
    [TestClass]
    public class DepthFirstSearchTests
    {
        [TestMethod]
        public void TestSimpleDepthFirstSearch()
        {
            var fileRead = new FileDigraphReader();
            var graph = fileRead.ReadFromFile("DigraphsData\\tinyDG.dat");
            var dfs = new DirectedDfs(graph, 0);

            for (int i = 0; i < 6; i++)
            {
                Assert.IsTrue(dfs.IsMarked(i));
            }

            for (int i = 6; i < graph.V(); i++)
            {
                Assert.IsFalse(dfs.IsMarked(i));
            }

        }
    }
}
