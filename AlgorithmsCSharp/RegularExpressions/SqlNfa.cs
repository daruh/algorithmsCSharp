using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsCSharp.RegularExpressions
{
    public class SqlNfa : Nfa
    {
        public SqlNfa(string pattern) : base(pattern, '%', '_')
        {
        }
    }
}