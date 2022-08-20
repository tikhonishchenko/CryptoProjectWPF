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
        private async void MainMenu(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());

        }
        public async Task SetCoinAsync(string Name)
        {
            Cryptocurrency coin = await DataAccess.GetCryptocurrencyAsync(Name.ToUpper());
            if(coin == null)
            {
                coin = await DataAccess.GetCryptocurrencyNameAsync(Name.ToLower());
            }
            int i = 1;
            if (coin != null)
            {
                foreach (Market market in coin.Markets)
                {
                    Label marketLabel = (Label)this.FindName("Market" + i);
                    marketLabel.Content = market.Name + ": 1 " + coin.asset_id + " = " + market.priceQuote + " USDT";
                    i++;
                }

                
                NameCoin.Content = coin.name;
                TickerCoin.Content = coin.asset_id;
                VolumeCoin.Content = Math.Round(coin.volume_24h / 1000000, 2) + "M";
                if (coin.price > 10)
                {
                    PriceCoin.Content = Math.Round(coin.price, 2);

                }
                else if (coin.price > 0.001)
                {
                    PriceCoin.Content = Math.Round(coin.price, 5);
                }
                else
                {
                    PriceCoin.Content = Math.Round(coin.price, 10);
                }
                PriceCoin.Content = "$ " + PriceCoin.Content;
                if (coin.change_24h >= 0)
                {
                    ChangeCoin.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    ChangeCoin.Foreground = new SolidColorBrush(Colors.Red);

                }
                ChangeCoin.Content = Math.Round(coin.change_24h, 2) + "%";
                placeholder.Content = "";
            }
            else
            {
                placeholder.Content = "No coin found named: " + Name;
            }

        }
        public DetailsPage(string Name)
        {
            SetCoinAsync(Name);
            InitializeComponent();
        }
    }
}
