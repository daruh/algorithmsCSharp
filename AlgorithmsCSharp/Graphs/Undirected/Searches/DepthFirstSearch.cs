using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsCSharp.Graphs.Undirected.Searches
{
    public class DepthFirstSearch
    {
        private bool[] marked;
        private int count;

        public DepthFirstSearch(IGraph g, int s)
        {
            marked = new bool[g.V()];
            dfs(g, s);
        }

        private void dfs(IGraph g, int v)
        {
            marked[v] = true;
            count++;
            foreach (var w in g.Adjacent(v))
            {
                if (!marked[w])
                {
                    dfs(g, w);
                }
            }
        }

        public bool IsMarked(int w)
        {
            return marked[w];
        }

        public int Count()
        {
            return count;
        }
    }
}