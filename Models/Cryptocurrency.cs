using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProject
{
    internal class Cryptocurrency
    {
        public string name;
        public string asset_id;
        public double price;
        public double volume_24h;
        public double change_24h;
        public List<Market> Markets = new List<Market>();



    }
}
