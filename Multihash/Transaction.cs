using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multihash
{
    internal class Transaction
    {
        [Name("PROD_CODE")]
        public string PROD_CODE { get; set; }
        [Name("BASKET_ID")]
        public long BASKET_ID { get; set; }
    }

    internal class HashFunc
    {
        int paramI;
        int paramJ;
        int mod;

        public HashFunc(int paramI, int paramJ, int mod)
        {
            this.paramI = paramI;
            this.paramJ = paramJ;
            this.mod = mod;
        }

        public int Hash(int first, int second)
        {
            return (paramI * first + paramJ * second) % mod;
        }
    }
}
