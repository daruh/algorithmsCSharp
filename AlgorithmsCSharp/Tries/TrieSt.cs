namespace AlgorithmsCSharp.Tries
{
    public class Node<Value>
    {
        private static int R = 256;
        public Value? Val { get; set; }
        public Node<Value>[] Next = new Node<Value>[R];
    }

    public class TrieSt<Value>
    {
        private Node<Value> _root;


        public Value Get(string key)
        {
            var x = Get(_root, key, 0);
            return x.Val;
        }

        private Node<Value> Get(Node<Value> x, string key, int d)
        {
            if (x == null) return null;
            if (d == key.Length) return x;
            var c = key[d];
            return Get(x.Next[c], key, d + 1);
        }

        public void Put(string key, Value val)
        {
            _root = Put(_root, key, val, 0);
        }

        private Node<Value> Put(Node<Value> x, string key, Value val, int d)
        {
            x ??= new Node<Value>();

            if (d == key.Length)
            {
                x.Val = val;
                return x;
            }

            var c = key[d];

            x.Next[c] = Put(x.Next[c], key, val, d + 1);
            return x;
        }

        public IEnumerable<string> AllKeys()
        {
            return KeysWithPrefix("");
        }

        public IEnumerable<string> KeysWithPrefix(string prefix)
        {
            var q = new Queue<string>();
            var n = Get(_root, prefix, 0);
            Collect(n, prefix, q);
            return q;
        }

        private void Collect(Node<Value> x, string prefix, Queue<string> queue)
        {
            if (x == null) return;
            if (x.Val != null) queue.Enqueue(prefix);

            for (var c = 0; c < 256; c++)
            {
                Collect(x.Next[c], prefix + (char)c, queue);
            }
        }

        public string LongestPrefixOf(string s)
        {
            var l = search(_root, s, 0, 0);
            return s.Substring(0, l);
        }

        private int search(Node<Value> x, string s, int d, int length)
        {
            if (x == null) return length;
            if (x.Val != null) length = d;
            if (d == s.Length) return length;
            var c = s[d];
            return search(x.Next[c], s, d + 1, length);
        }

        public void Delete(string key)
        {
            _root = delete(_root, key, 0);
        }

        private Node<Value> delete(Node<Value> x, string key, int d)
        {
            if (x == null) return null;
            if (d == key.Length)
            {
                x.Val = default;
            }
            else
            {
                var c = key[d];
                x.Next[c] = delete(x.Next[c], key, d + 1);
            }

            if (x.Val != null) return x;

            for (int i = 0; i < 256; i++)
            {
                if (x.Next[i] != null) return x;
            }

            return null;
        }

        public int Size()
        {
            return size(_root);
        }

        private int size(Node<Value> x)
        {
            if (x == null) return 0;
            var cnt = 0;
            if (x.Val != null) cnt++;

            for (int i = 0; i < 256; i++)
            {
                cnt += size(x.Next[i]);
            }

            return cnt;
        }
    }
}