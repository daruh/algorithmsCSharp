using AlgorithmsCSharp.Graphs.Directed;
using AlgorithmsCSharp.Graphs.Directed.Searches;

namespace AlgorithmsCSharp.RegularExpressions
{
    public class Nfa
    {
        private char[] re;
        private Digraph G;
        private int M;

        public Nfa(string regexp)
        {
            var ops = new Stack<int>();
            re = regexp.ToCharArray();
            M = re.Length;
            G = new Digraph(M + 1);


            for (var i = 0; i < M; i++)
            {
                var lp = i;
                if (re[i] == '(' || re[i] == '|')
                {
                    ops.Push(i);
                }
                else if (re[i] == ')')
                {
                    var or = ops.Pop();
                    if (re[or] == '|')
                    {
                        lp = ops.Pop();
                        G.AddEdge(lp, or + 1);
                        G.AddEdge(or, i);
                    }
                    else
                    {
                        lp = or;
                    }
                }

                if (i < M - 1 && re[i + 1] == '*')
                {
                    G.AddEdge(lp,i+1);
                    G.AddEdge(i+1,lp);
                }

                if (re[i] == '(' || re[i] == '*' || re[i] == ')')
                {
                    G.AddEdge(i,i+1);
                }
            }
        }

        public bool Recognizes(string txt)
        {
            var pc = new HashSet<int>();
            var dfs = new DirectedDfs(G, 0);

            for (int v = 0; v < G.V(); v++)
            {
                if (dfs.IsMarked(v))
                {
                    pc.Add(v);
                }
            }


            foreach (var t in txt)
            {
                var match = new HashSet<int>();

                foreach (var v in pc)
                {
                    if (v < M)
                    {
                        if (re[v] == t || re[v] == '.')
                        {
                            match.Add(v + 1);
                        }
                    }
                }

                pc = new HashSet<int>();
                dfs = new DirectedDfs(G, match);

                for (var v = 0; v < G.V(); v++)
                {
                    if (dfs.IsMarked(v))
                    {
                        pc.Add(v);
                    }
                }
            }

            return pc.Any(v => v == M);
        }
    }
}