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
            }
            return cryptocurrency;
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
    }
}
