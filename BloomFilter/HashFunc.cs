using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomFilter
{
    internal class HashFunc
    {
        private int param = 5;
        private int seed = 0;

        public HashFunc()
        {
            var rnd = new Random();
            seed = (int)Math.Floor(rnd.NextDouble() * param);
        }

        public uint Hash(string str)
        {
            uint result = 1;
            for (var i = 0; i < str.Length; i++)
                result = (uint)(seed * result + (int)str[i]);
            return result;
        }
    }
}
