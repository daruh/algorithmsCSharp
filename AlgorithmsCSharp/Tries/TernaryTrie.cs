using System.Text;
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
                x.Left = put(x.Left, key, val, d, isNewKey);
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
            if (x == null) return 0;
            var size = x.Size;
            size += getTreeSize(x.Left);
            size += getTreeSize(x.Right);

            return size;
        }

        public string LongestPrefixOf(string word)
        {
            var length = search(_root, word, 0, 0);
            return word.Substring(0, length);
        }

        private int search(TrieNode<Value> x, string word, int d, int length)
        {
            if (x == null) return length;
            if (x.Val != null && x.C == word[d])
            {
                length = d + 1;
            }

            var c = word[d];

            if (c < x.C)
            {
                return search(x.Left, word, d, length);
            }

            if (c > x.C)
            {
                return search(x.Right, word, d, length);
            }

            if (d < word.Length - 1)
            {
                return search(x.Mid, word, d + 1, length);
            }

            return length;
        }

        public void Delete(string key)
        {
            if (!Contains(key))
            {
                return;
            }

            _root = delete(_root, key, 0);
        }


        private TrieNode<Value> delete(TrieNode<Value> x, string key, int d)
        {
            if (x == null)
            {
                return null;
            }

            if (d == key.Length - 1)
            {
                x.Size -= 1;
                x.Val = default(Value);
            }
            else
            {
                var c = key[d];
                if (c < x.C)
                {
                    x.Left = delete(x.Left, key, d);
                }
                else if (c > x.C)
                {
                    x.Right = delete(x.Right, key, d);
                }
                else
                {
                    x.Size -= 1;
                    x.Mid = delete(x.Mid, key, d + 1);
                }
            }

            if (x.Size == 0)
            {
                if (x.Left == null && x.Right == null)
                {
                    return null;
                }
                else if (x.Left == null)
                {
                    return x.Right;
                }
                else if (x.Right == null)
                {
                    return x.Left;
                }
                else
                {
                    //TODO is there a use case to handle?
                }
            }

            return x;
        }

        public string MinKey()
        {
            var minNode = min(_root);

            var builder = new StringBuilder();
            builder.Append(minNode.C);

            while (minNode != null)
            {
                minNode = minNode.Mid;

                while (minNode?.Left != null)
                {
                    minNode = minNode.Left;
                }

                if (minNode != null)
                    builder.Append(minNode.C);
            }

            return builder.ToString();
        }


        private TrieNode<Value> min(TrieNode<Value> x)
        {
            if (x.Left == null) return x;
            return min(x.Left);
        }

        public string MaxKey()
        {
            var maxNode = max(_root);

            var builder = new StringBuilder();
            builder.Append(maxNode.C);

            while (maxNode != null)
            {
                maxNode = maxNode.Mid;

                while (maxNode?.Right != null)
                {
                    maxNode = maxNode.Right;
                }

                if (maxNode != null)
                    builder.Append(maxNode.C);
            }

            return builder.ToString();
        }

        private TrieNode<Value> max(TrieNode<Value> x)
        {
            if (x.Right == null) return x;
            return max(x.Right);
        }

        public string Select(int index)
        {
            //TODO throw exception if size exceeded
            return select(_root, index, "");
        }

        private string select(TrieNode<Value> x, int index, string prefix)
        {
            if (x == null) return null;
            var leftSubtree = getTreeSize(x.Left);
            var tstSize = leftSubtree + x.Size;

            if (index < leftSubtree)
            {
                return select(x.Left, index, prefix);
            }

            if (index > tstSize)
            {
                return select(x.Right, index - tstSize, prefix);
            }

            index -= leftSubtree;
            if (x.Val != null)
            {
                if (index == 0)
                {
                    return prefix + x.C;
                }

                index--;
            }

            return select(x.Mid, index, prefix + x.C);
        }
    }
}