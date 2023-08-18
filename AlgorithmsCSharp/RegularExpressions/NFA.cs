using AlgorithmsCSharp.Graphs.Directed;
using AlgorithmsCSharp.Graphs.Directed.Searches;

namespace AlgorithmsCSharp.RegularExpressions
{
    public class Nfa
    {
        private char[] re;
        private Digraph G;
        private int M;
        private Dictionary<int, int> _setsMatchMap = new();

        public Nfa(string regexp)
        {
            var ops = new Stack<int>();
            re = regexp.ToCharArray();
            M = re.Length;
            G = new Digraph(M + 1);


            for (var i = 0; i < M; i++)
            {
                var lp = i;
                if (re[i] == '(' || re[i] == '|' || re[i] == '[')
                {
                    ops.Push(i);
                }
                else if (re[i] == ')')
                {

                    var orOperatorIndexes = new HashSet<int>();
                    while (re[ops.Peek()] == '|')
                    {
                        int or = ops.Pop();
                        orOperatorIndexes.Add(or);
                    }

                    lp = ops.Pop();
                    foreach (var orOpIx in orOperatorIndexes)
                    {
                        G.AddEdge(lp, orOpIx + 1);
                        G.AddEdge(orOpIx, i);
                    }
                }
                //match Set
                else if (re[i] == ']')
                {
                    var leftOperator = ops.Pop();

                    for (int ixInBracket = leftOperator + 1; ixInBracket < i; ixInBracket++)
                    {
                        G.AddEdge(leftOperator, ixInBracket);

                        _setsMatchMap.Add(ixInBracket, i);
                        if (re[ixInBracket + 1] == '-')
                        {
                            ixInBracket += 2;
                        }
                    }
                }

                if (i < M - 1)
                {
                    if (re[i + 1] == '*')
                    {
                        G.AddEdge(lp, i + 1);
                        G.AddEdge(i + 1, lp);
                    }
                    else if (re[i + 1] == '+')
                    {
                        G.AddEdge(i + 1, lp);
                    }
                }

                if (re[i] == '(' || re[i] == '*' || re[i] == ')' || re[i] == '[' || re[i] == ']'|| re[i]=='+')
                {
                    G.AddEdge(i, i + 1);
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
                        if (_setsMatchMap.ContainsKey(v))
                        {
                            if (re[v + 1] == '-')
                            {
                                var leftRangeChar = re[v];
                                var rightRangeChar = re[v + 2];

                                if (t >= leftRangeChar && t <= rightRangeChar)
                                {
                                    var rightSqBracketIx = _setsMatchMap.GetValueOrDefault(v);
                                    match.Add(rightSqBracketIx);
                                }
                            }
                            else if (re[v] == t || re[v] == '.')
                            {
                                var rightSqBracketIx = _setsMatchMap.GetValueOrDefault(v);
                                match.Add(rightSqBracketIx);
                            }
                        }
                        else if (re[v] == t || re[v] == '.')
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