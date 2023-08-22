
using System.Xml.XPath;

namespace AlgorithmsCSharp.Tries
{
    public class TrieNode<Value>
    {
        public char C;
        public Value? Val { get; set; }

        public int Size { get; set; }
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

        public bool Contains(string key)
        {
            return Get(key) != null;
        }

        public void Put(string key, Value val)
        {
            var isNewKey = !Contains(key);

            _root = put(_root, key, val, 0, isNewKey);
        }


        private TrieNode<Value> put(TrieNode<Value> x, string key, Value val, int d, bool isNewKey)
        {
            var c = key[d];
            if (x == null)
            {
                x = new TrieNode<Value>
                {
                    C = c,
                };
            }

            if (c < x.C)
            {
                x.Left = put(x.Left, key, val, d,isNewKey);
            }
            else if (c > x.C)
            {
                x.Right = put(x.Right, key, val, d, isNewKey);
            }
            else if (d < key.Length - 1)
            {
                x.Mid = put(x.Mid, key, val, d + 1, isNewKey);
                if (isNewKey)
                {
                    x.Size += 1;
                }
            }
            else
            {
                x.Val = val;
                if (isNewKey)
                {
                    x.Size += 1;
                }
            }

            return x;
        }

        public IEnumerable<string> Keys()
        {
            var queue = new Queue<string>();
            collect(_root, "", queue);
            return queue;
        }

        public IEnumerable<string> KeysWithPrefix(string prefix)
        {
            var queue = new Queue<string>();

            var startNode = get(_root, prefix, 0);

            if (startNode == null)
                return queue;

            collect(startNode, prefix, queue);
            return queue;
        }

        private void collect(TrieNode<Value> x, string prefix, Queue<string> q)
        {
            if (x == null) return;

            collect(x.Left, prefix, q);

            if (x.Val != null)
            {
                q.Enqueue(prefix + x.C);
            }

            collect(x.Mid, prefix + x.C, q);
            collect(x.Right, prefix, q);
        }

        public int GetTreeSize()
        {
            return getTreeSize(_root);
        }

        private int getTreeSize(TrieNode<Value> x)
        {
            if(x==null) return 0;
            var size = x.Size;
            size += getTreeSize(x.Left);
            size += getTreeSize(x.Right);

            return size;
        }
    }
}