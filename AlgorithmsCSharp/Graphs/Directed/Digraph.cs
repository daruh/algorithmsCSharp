﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsCSharp.Graphs.Directed
{
    public interface IDigraph : IGraph
    {
        Digraph Reverse();
    }

    public class Digraph : IDigraph
    {
        private readonly int _verticies;
        private int _edges;
        private readonly Dictionary<int, HashSet<int>> _adjacencyList;

        public Digraph(int v)
        {
            _verticies = v;
            _edges = 0;
            _adjacencyList = new Dictionary<int, HashSet<int>>();
            for (var i = 0; i < _verticies; i++)
            {
                _adjacencyList.Add(i, new HashSet<int>());
            }
        }

        public void AddEdge(int v, int w)
        {
            _adjacencyList[v].Add(w);
            _edges++;
        }

        public IEnumerable<int> Adjacent(int v)
        {
            return _adjacencyList[v];
        }

        public int V()
        {
            return _verticies;
        }

        public int E()
        {
            return _edges;
        }

        public Digraph Reverse()
        {
            var r = new Digraph(_verticies);
            for (int v = 0; v < _verticies; v++)
            {
                foreach (var w in Adjacent(v))
                {
                    r.AddEdge(w, v);
                }
            }

            return r;
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"{_verticies} verticies, {_edges} edges ");
            for (var i = 0; i < _verticies; i++)
            {
                strBuilder.AppendLine($"{i}: {string.Join(" ", Adjacent(i))}");
            }

            return strBuilder.ToString();
        }
    }
}