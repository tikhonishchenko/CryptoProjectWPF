using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProject.ViewModels
{
    internal class ShellViewModel
    {
        public BindableCollection<Cryptocurrency> Crypto { get; set; }
        public List<Cryptocurrency> CryptoList { get; set; }

        public ShellViewModel()
        {
            GetCrypto();
            Crypto = new BindableCollection<Cryptocurrency>(CryptoList);
        }
        private async Task GetCrypto()
        {
            CryptoList = await DataAccess.GetCryptocurrenciesAsync(10);
        }
    }
}
