using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProject
{
    internal class CryptocurrencyObject
    {
        public string name;
        public string symbol;
        public double priceUsd;
        public double volumeUsd24Hr;
        public double changePercent24Hr;
        public List<Market> Markets = new List<Market>();

    }
}
