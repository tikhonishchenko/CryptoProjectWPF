using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProject
{
    internal static class DataAccess
    {
        static HttpClient client = new HttpClient();
        public static async Task<Cryptocurrency> GetCryptocurrencyAsync(string name)
        {
            string path = "https://cryptingup.com/api/assets/" + name;
            Cryptocurrency cryptocurrency = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var parsedObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                cryptocurrency = JsonConvert.DeserializeObject<Cryptocurrency>
                    (parsedObject["asset"].ToString());
                cryptocurrency.Markets = await GetExchangesAsync(cryptocurrency.asset_id);

            }
            return cryptocurrency;
        }

        private static async Task<List<Market>> GetExchangesAsync(string asset_id)
        {
            string[] exchanges = new string[7] { "binance", "kucoin", "kraken", "huobi", "bitfinex", "gate", "whitebit" };
            List<Market> marketsToReturn = new List<Market>();
            foreach (string change in exchanges)
            {
                if (marketsToReturn.Count < 3)
                {
                    string path = "https://api.coincap.io/v2/markets?exchangeId=" + change + "&baseSymbol=" + asset_id + "&quoteSymbol=USDT";
                    HttpResponseMessage response = await client.GetAsync(path);
                    Market market = new Market();
                    if (response.IsSuccessStatusCode)
                    {
                        var parsedObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                        List<Market> markets = JsonConvert.DeserializeObject<List<Market>>
                            (parsedObject["data"].ToString());
                        if(markets.Count!=0)
                        {
                            market = markets.ToArray()[0];
                            market.Name = change.First().ToString().ToUpper() + change.Substring(1);
                            marketsToReturn.Add(market);
                        }
                    }

                }
                else
                {
                    break;
                }
            }
            return marketsToReturn;
        }

        public static async Task<Cryptocurrency> GetCryptocurrencyNameAsync(string name)
        {
            string path = "https://api.coincap.io/v2/assets/" + name;
            CryptocurrencyObject cryptocurrency = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var parsedObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                cryptocurrency = JsonConvert.DeserializeObject<CryptocurrencyObject>
                    (parsedObject["data"].ToString());
                cryptocurrency.Markets = await GetExchangesAsync(cryptocurrency.symbol);

            }

            if(cryptocurrency != null)
            {
                return ToDefault(cryptocurrency);

            }
            return null;
        }
        public static async Task<List<Cryptocurrency>> GetCryptocurrenciesAsync(int size)
        {
            string path = "https://cryptingup.com/api/assets/?size=" + size;
            List<Cryptocurrency> cryptocurrency = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var parsedObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                cryptocurrency = JsonConvert.DeserializeObject<List<Cryptocurrency>>
                    (parsedObject["assets"].ToString());
            }
            return cryptocurrency;
        }

        public static Cryptocurrency ToDefault(CryptocurrencyObject crypto)
        {
            return new Cryptocurrency
            {
                asset_id = crypto.symbol,
                name = crypto.name,
                volume_24h = crypto.volumeUsd24Hr,
                change_24h = crypto.changePercent24Hr,
                price = crypto.priceUsd,
                Markets = crypto.Markets
            };
        }
    }
}
