using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsCSharp.Tries
{
    public class TrieExtBranching<Value>
    {
        protected static int R = 256;

        private Node<Value> _root;

        private class Node<Value>
        {
            public Value? Val { get; set; }

            public Node<Value>[] Next = new Node<Value>[R];

            public int Size { get; set; }

            public string Characters { get; set; }
        }


        public void Put(string key, Value val)
        {
            _root = put(_root, key, val, 0, true);
        }

        private Node<Value> put(Node<Value> x, string key, Value val, int digit, bool isNewKey)
        {
            if (x == null)
            {
                x = new Node<Value>()
                {
                    Val = val,
                    Size = 1
                };

                if (digit != key.Length)
                {
                    x.Characters = key.Substring(digit, key.Length);
                }

                return x;
            }

            if (isNewKey)
            {
                x.Size++;
            }

            if (x.Characters != null)
            {
                var nodeCharactersLength = x.Characters.Length;

                //TODO if path exists only fill Val

                var parentNode = new Node<Value>();
                parentNode.Size = 2;

                var currentNode = parentNode;
                var maxLength = Math.Max(nodeCharactersLength, key.Length - digit);

                


            }

            return null;
        }
    }
}