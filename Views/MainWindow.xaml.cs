using Caliburn.Micro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CryptoProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private async void Button1_ClickAsync(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string ii = button.Name;
        }

        private async void RefreshAsync(object sender, RoutedEventArgs e)
        {
            List<Cryptocurrency> cryptocurrencies = await DataAccess.GetCryptocurrenciesAsync(10);
            int i = 1;

            foreach (Cryptocurrency coin in cryptocurrencies)
            {
                Label name =(Label)this.FindName("NameCoin"+i);
                Label ticker = (Label)this.FindName("TickerCoin" + i);
                Label volume = (Label)this.FindName("VolumeCoin" + i);
                Label price = (Label)this.FindName("PriceCoin" + i);
                Label change = (Label)this.FindName("ChangeCoin" + i);
                name.Content = coin.name;
                ticker.Content = coin.asset_id;
                volume.Content = Math.Round(coin.volume_24h / 1000000, 2) +"M";
                if (coin.price > 10)
                {
                    price.Content = Math.Round(coin.price,2);
                }
                else if(coin.price > 0.001)
                {
                    price.Content = Math.Round(coin.price,5);
                }
                else
                {
                    price.Content = Math.Round(coin.price, 10);

                }
                price.Content = "$ " + price.Content;
                change.Content = Math.Round(coin.change_24h,2)+ "%";
                i++;
            }
        }



    }
}
