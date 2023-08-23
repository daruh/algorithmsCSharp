using System;

namespace AlgorithmsCSharp.Tries
{
    public class Node<Value>
    {
        public Value? Val { get; set; }
        public Node<Value>[] Next = new Node<Value>[TrieSt<Value>.R];
    }

    public class TrieSt<Value>
    {
        public static int R = 256;
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

            for (var c = 0; c < R; c++)
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

            for (int i = 0; i < R; i++)
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

            for (int i = 0; i < R; i++)
            {
                cnt += size(x.Next[i]);
            }

            return cnt;
        }

        public string Select(int index)
        {
            if (index < 0 || index > size(_root))
            {
                throw new IndexOutOfRangeException("index is out of bounds");
            }

            return select(_root, index, "");
        }

        private string select(Node<Value> x, int index, string prefix)
        {
            if (x == null)
                return null;

            if (x.Val != null)
            {
                if (index == 0)
                {
                    return prefix;
                }

                index--;
            }

            for (int i = 0; i < R; i++)
            {
                var nextNode = x.Next[i];
                if (nextNode != null)
                {
                    if (index < size(nextNode))
                    {
                        return select(nextNode, index, prefix + (char)i);
                    }

                    index -= size(nextNode);
                }
            }

            return null;
        }

        public int IndexOf(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            return indexOf(_root, key, 0, 0);
        }

        private int indexOf(Node<Value> x, string key, int digit, int size)
        {
            if (x == null || digit == key.Length)
            {
                return size;
            }

            if (x.Val != null)
            {
                if (digit < key.Length)
                {
                    size++;
                }
                else
                {
                    return size;
                }
            }

            var c = key[digit];
            for (var i = 0; i < c; i++)
            {
                size += this.size(x.Next[i]);
            }

            return indexOf(x.Next[c], key, digit + 1, size);
        }

        public string MinKey()
        {
            return min(_root, "");
        }

        private string min(Node<Value> x, string prefix)
        {
            if (x.Val != null)
            {
                return prefix;
            }

            int i = 0;
            while (i < R && x.Next[i] == null)
            {
                i++;
            }

            if (x.Next[i] != null)
            {
                return min(x.Next[i], prefix + (char)i);
            }

            return prefix;
        }

        public string MaxKey()
        {
            return max(_root, "");
        }

        private string max(Node<Value> x, string prefix)
        {
            if (x.Val != null)
            {
                return prefix;
            }

            int i = R - 1;
            while (i >= 0 && x.Next[i] == null)
            {
                i--;
            }

            if (x.Next[i] != null)
            {
                return max(x.Next[i], prefix + (char)i);
            }

            return prefix;
        }

        public void DeleteMinKey()
        {
            var minKey = MinKey();
            Delete(minKey);
        }

        public void DeleteMaxKey()
        {
            var maxKey = MaxKey();
            Delete(maxKey);
        }

        // Returns the highest key in the symbol table smaller than or equal to key.
        public string Floor(string key)
        {
            return floor(_root, key, 0, "", null, true);
        }

        //
        private string floor(Node<Value> x, string key, int digit, string prefix, string lastKeyFound,
            bool mustBeEqualDigit)
        {
            if (x == null)
                return null;

            if (prefix.CompareTo(key) > 0)
            {
                return lastKeyFound;
            }

            if (x.Val != null)
            {
                lastKeyFound = prefix;
            }

            char currentChar;

            if (mustBeEqualDigit && digit < key.Length)
            {
                currentChar = key[digit];
            }
            else
            {
                currentChar = (char)(R - 1);
            }

            for (var nextChar = currentChar;; nextChar--)
            {
                var nextNode = x.Next[nextChar];

                if (nextNode != null)
                {
                    if (nextChar < currentChar)
                    {
                        mustBeEqualDigit = false;
                    }

                    lastKeyFound = floor(nextNode, key, digit + 1, prefix + nextChar, lastKeyFound, mustBeEqualDigit);

                    if (lastKeyFound != null)
                    {
                        return lastKeyFound;
                    }
                }

                if (nextChar == 0)
                {
                    break;
                }
            }

            return lastKeyFound;
        }

        // Returns the highest key in the symbol table smaller than or equal to key.
        public string Ceilling(string key)
        {
            return ceilling(_root, key, 0, "", true);
        }

        private string ceilling(Node<Value> x, string key, int digit, string prefix, bool mustBeEqualOrDigit)
        {
            if (x == null)
            {
                return null;
            }

            if (x.Val != null && prefix.CompareTo(key) >= 0)
            {
                return prefix;
            }

            char currentChar;

            if (mustBeEqualOrDigit && digit < key.Length)
            {
                currentChar = key[digit];
            }
            else
            {
                currentChar = (char)0;
            }

            for (var nextChar = currentChar; nextChar < R; nextChar++)
            {
                if (x.Next[nextChar] != null)
                {
                    if (nextChar > currentChar)
                    {
                        mustBeEqualOrDigit = false;
                    }

                    var keyFound = ceilling(x.Next[nextChar], key, digit + 1, prefix + nextChar, mustBeEqualOrDigit);

                    if (keyFound != null)
                    {
                        return keyFound;
                    }
                }
            }

            return null;
        }
    }
}