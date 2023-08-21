using System.Security.Cryptography.X509Certificates;

namespace AlgorithmsCSharp.Tries
{
    public class TrieNode<Value>
    {
        public char C;
        public Value? Val { get; set; }

        public TrieNode<Value> Left { get; set; }
        public TrieNode<Value> Mid { get; set; }
        public TrieNode<Value> Right { get; set; }
    }

    public class TernaryTrie<Value>
    {
        private TrieNode<Value> _root;


        public Value Get(string key)
        {
            var node = get(_root, key, 0);
            if (node == null)
            {
                return default(Value);
            }

            return node.Val;
        }

        private TrieNode<Value> get(TrieNode<Value> x, string key, int d)
        {
            if (x == null) return null;
            var c = key[d];
            if (c < x.C)
            {
                return get(x.Left, key, d);
            }

            if (c > x.C)
            {
                return get(x.Right, key, d);
            }

            if (d < key.Length - 1)
            {
                return get(x.Mid, key, d + 1);
            }

            return x;
        }

        public void Put(string key, Value val)
        {
            _root = put(_root, key, val, 0);
        }

        private TrieNode<Value> put(TrieNode<Value> x, string key, Value val, int d)
        {
            var c = key[d];
            if (x == null)
            {
                x = new TrieNode<Value>
                {
                    C = c
                };
            }

            if (c < x.C)
            {
                x.Left = put(x.Left, key, val, d);
            }
            else if (c > x.C)
            {
                x.Right = put(x.Right, key, val, d);
            }
            else if (d < key.Length - 1)
            {
                x.Mid = put(x.Mid, key, val, d + 1);
            }
            else
            {
                x.Val = val;
            }

            return x;
        }
    }
}