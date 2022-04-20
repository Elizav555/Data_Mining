using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomFilter
{
    public class BloomFilterClass
    {
        private int filterSize;
        private int funcCount;
        public int[] filter;
        private HashFunc[] func;

        public BloomFilterClass(int maxNumb, double errorProbability)
        {
            var optimalParams = GetOptimalFilterParams(maxNumb, errorProbability);
            filterSize = optimalParams.Item1;
            funcCount = optimalParams.Item2;
            filter = new int[filterSize];
            filter = filter.Select(it => 0).ToArray();
            func = new HashFunc[funcCount];
            for (int i = 0; i < funcCount; i++)
            {
                func[i] = new HashFunc();
            }
        }

        public void Add(string str)
        {
            for (var i = 0; i < funcCount; i++)
                filter[func[i].Hash(str) % filterSize] |= 1;
        }

        public bool FindString(string str)
        {
            for (var i = 0; i < funcCount; i++)
                if (filter[func[i].Hash(str) % filterSize] == 0)
                    return false;
            return true;
        }

        private static (int, int) GetOptimalFilterParams(int elementsCount, double p)
        {
            var sizeDouble = -(elementsCount * Math.Log(p)) / (Math.Log(2) * Math.Log(2));
            var countDouble = (sizeDouble / elementsCount) * Math.Log(2);
            int size = (int)Math.Ceiling(sizeDouble);
            int count = (int)Math.Ceiling(countDouble);
            Console.WriteLine($"Optimal filter size : {size}");
            Console.WriteLine($"Optimal func count : {count}");
            return (size, count);
        }
    }
}
