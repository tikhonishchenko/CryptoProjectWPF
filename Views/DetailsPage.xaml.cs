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
                    TextBlock marketLabel = (TextBlock)this.FindName("Market" + i);
                    market.priceQuote = Double.Parse(market.priceQuote.Replace(".", ",")).ToString();
                    marketLabel.Text = market.Name + ": 1 " + coin.asset_id + " = " + market.priceQuote + " USDT";
                    i++;
                }

                NameCoin.Text =  coin.name;
                TickerCoin.Text = coin.asset_id;
                VolumeCoin.Text = "Volume 24h: "+Math.Round(coin.volume_24h / 1000000, 2) + "M";
                if (coin.price > 10)
                {
                    PriceCoin.Text = Math.Round(coin.price, 2).ToString();

                }
                else if (coin.price > 0.001)
                {
                    PriceCoin.Text = Math.Round(coin.price, 5).ToString();
                }
                else
                {
                    PriceCoin.Text = Math.Round(coin.price, 10).ToString();
                }
                PriceCoin.Text = "Price: " + PriceCoin.Text+" $";
                if (coin.change_24h >= 0)
                {
                    ChangeCoin.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    ChangeCoin.Foreground = new SolidColorBrush(Colors.Red);

                }
                ChangeCoin.Text = "Price change 24h: "+Math.Round(coin.change_24h, 2) + "%";
                placeholder.Text = "";
            }
            else
            {
                placeholder.Text = "No coin found named: " + Name;
            }

        }
        public DetailsPage(string Name)
        {
            SetCoinAsync(Name);
            InitializeComponent();
        }
    }
}
