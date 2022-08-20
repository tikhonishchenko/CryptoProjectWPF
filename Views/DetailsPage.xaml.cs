using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptoProject.Views
{
    /// <summary>
    /// Interaction logic for DetailsPage.xaml
    /// </summary>
    public partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            InitializeComponent();
        }
        public async Task SetCoinAsync(string Name)
        {
            Cryptocurrency coin = await DataAccess.GetCryptocurrencyAsync(Name.ToUpper());
            if(coin == null)
            {
                coin = await DataAccess.GetCryptocurrencyNameAsync(Name.ToLower());
            }
            int i = 1;
            foreach (Market market in coin.Markets)
            {
                Label marketLabel = (Label)this.FindName("Market" + i);
                marketLabel.Content = market.Name+": 1 " + coin.asset_id + " = " + market.priceQuote + " USDT";
                i++;
            }
            Label name = (Label)this.FindName("NameCoin");
            Label ticker = (Label)this.FindName("TickerCoin");
            Label volume = (Label)this.FindName("VolumeCoin");
            Label price = (Label)this.FindName("PriceCoin");
            Label change = (Label)this.FindName("ChangeCoin");


            name.Content = coin.name;
            ticker.Content = coin.asset_id;
            volume.Content = Math.Round(coin.volume_24h / 1000000, 2) + "M";
            if (coin.price > 10)
            {
                price.Content = Math.Round(coin.price, 2);

            }
            else if (coin.price > 0.001)
            {
                price.Content = Math.Round(coin.price, 5);
            }
            else
            {
                price.Content = Math.Round(coin.price, 10);
            }
            price.Content = "$ " + price.Content;
            if (coin.change_24h >= 0)
            {
                change.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                change.Foreground = new SolidColorBrush(Colors.Red);

            }
            change.Content = Math.Round(coin.change_24h, 2) + "%";
        }
        public DetailsPage(string Name)
        {
            SetCoinAsync(Name);
            InitializeComponent();
        }
    }
}
