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
    /// Interaction logic for PageWPF.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button1_ClickAsync(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string ii = button.Name.Substring(13);
            Label name = (Label)this.FindName("TickerCoin" + ii);
            this.NavigationService.Navigate(new DetailsPage(name.Content.ToString()));
        }

        private async void RefreshAsync(object sender, RoutedEventArgs e)
        {
            List<Cryptocurrency> cryptocurrencies = await DataAccess.GetCryptocurrenciesAsync(10);
            int i = 1;

            foreach (Cryptocurrency coin in cryptocurrencies)
            {
                Label name = (Label)this.FindName("NameCoin" + i);
                Label ticker = (Label)this.FindName("TickerCoin" + i);
                Label volume = (Label)this.FindName("VolumeCoin" + i);
                Label price = (Label)this.FindName("PriceCoin" + i);
                Label change = (Label)this.FindName("ChangeCoin" + i);
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
                i++;
            }
        }

        private async void Search(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SearchPage());

        }
    }
}
