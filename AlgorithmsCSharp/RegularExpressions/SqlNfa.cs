using AlgorithmsCSharp.Graphs.Directed.Searches;
using AlgorithmsCSharp.Graphs.Directed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsCSharp.RegularExpressions
{
    public class SqlNfa 
    {
        private char[] re;
        private Digraph G;
        private int M;
        private Dictionary<int, int> _setsMatchMap = new();
        private Dictionary<int, HashSet<char>> _setsCharaceterComplementsMap = new();
        private Dictionary<int, List<CharacterRange>> _setsRangesComplementsMap = new();

        protected const char Wildcard= '%';
        protected const char AnyCharacter ='_';
        private const char RangeCharacter = '-';

        public SqlNfa(string regexp)
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
                    handleSetRange(leftOperator, i);
                }

                if (re[i] == Wildcard)
                {
                    G.AddEdge(i, i);
                }

                if (i < M - 1)
                {
                    if (re[i + 1] == Wildcard)
                    {
                        G.AddEdge(lp, i + 1);
                        G.AddEdge(i + 1, lp);
                    }
                    else if (re[i + 1] == '+')
                    {
                        G.AddEdge(i + 1, lp);
                    }
                }

                if (new[] { '(', Wildcard, ')', '[', ']', '+' }.Contains(re[i]))
                {
                    G.AddEdge(i, i + 1);
                }
            }
        }

        private void handleSetRange(int leftSquareBracket, int index)
        {
            bool isComplementSet = false;
            var charactersToComplement = new HashSet<char>();
            var rangesToComplement = new List<CharacterRange>();

            if (re[leftSquareBracket + 1] == '^')
            {
                isComplementSet = true;
                leftSquareBracket++;

                for (var indexInBracket = leftSquareBracket + 1; indexInBracket < index; indexInBracket++)
                {
                    if (re[indexInBracket + 1] == RangeCharacter)
                    {
                        rangesToComplement.Add(new CharacterRange(re[indexInBracket], re[indexInBracket + 2]));
                        indexInBracket += 2;
                    }
                    else
                    {
                        charactersToComplement.Add(re[indexInBracket]);
                    }
                }
            }


            for (var indexInBracket = leftSquareBracket + 1; indexInBracket < index; indexInBracket++)
            {
                G.AddEdge(leftSquareBracket, indexInBracket);
                _setsMatchMap.Add(indexInBracket, index);

                if (isComplementSet)
                {
                    _setsCharaceterComplementsMap.Add(indexInBracket, charactersToComplement);
                    if (rangesToComplement.Count > 0)
                    {
                        _setsRangesComplementsMap.Add(indexInBracket, rangesToComplement);
                    }
                }

                if (re[indexInBracket + 1] == RangeCharacter)
                {
                    indexInBracket += 2;
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
                            recognizeRangeSet(t, v, match);
                        }
                        else if (re[v] == t || re[v] == AnyCharacter || re[v] == Wildcard)
                        {
                            match.Add(v + 1);
                            if (re[v] == Wildcard)
                            {
                                match.Add(v);
                            }
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

                if (pc.Count == 0)
                {
                    return false;
                }
            }

            foreach (var v in pc)
            {
                if (v == M)
                {
                    return true;
                }
            }

            return false;
        }

        private void recognizeRangeSet(char t, int v, HashSet<int> states)
        {
            var rightSquareBracketIx = _setsMatchMap.GetValueOrDefault(v);

            if (re[v + 1] == RangeCharacter)
            {
                var leftChar = re[v];
                var rightChar = re[v + 2];

                if (leftChar <= t && t <= rightChar)
                {
                    if (!isCharInComplementSet(t, v))
                    {
                        states.Add(rightSquareBracketIx);
                    }
                }
                else if (_setsCharaceterComplementsMap.ContainsKey(v) && !isCharInComplementSet(t, v))
                {
                    states.Add(rightSquareBracketIx);
                }
            }
            else if (re[v] == t || re[v] == AnyCharacter)
            {
                if (!isCharInComplementSet(t, v))
                {
                    states.Add(rightSquareBracketIx);
                }
            }
            else if (_setsCharaceterComplementsMap.ContainsKey(v) && !isCharInComplementSet(t, v))
            {
                states.Add(rightSquareBracketIx);
            }
        }

        private bool isCharInComplementSet(char t, int v)
        {
            if (_setsCharaceterComplementsMap.ContainsKey(v) &&
                _setsCharaceterComplementsMap.GetValueOrDefault(v).Contains(t))
            {
                return true;
            }

            if (_setsRangesComplementsMap.ContainsKey(v))
            {
                foreach (var rangeComplement in _setsRangesComplementsMap.GetValueOrDefault(v))
                {
                    if (rangeComplement.Left <= t && t <= rangeComplement.Right)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}