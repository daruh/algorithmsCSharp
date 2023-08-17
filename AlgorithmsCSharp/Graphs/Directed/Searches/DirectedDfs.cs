
namespace AlgorithmsCSharp.Graphs.Directed.Searches
{
    public class DirectedDfs
    {
        private bool[] marked;

        public DirectedDfs(IDigraph g, int s)
        {
            marked = new bool[g.V()];
            dfs(g, s);
        }

        public DirectedDfs(IDigraph g, IEnumerable<int> sources)
        {
            marked = new bool[g.V()];
            foreach (var s in sources)
            {
                if (!marked[s])
                {
                    dfs(g, s);
                }
            }
        }

        private void dfs(IDigraph g, int v)
        {
            marked[v] = true;
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
    }
}