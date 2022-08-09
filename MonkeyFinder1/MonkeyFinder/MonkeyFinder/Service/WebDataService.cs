using MonkeyFinder.Model;
using MonkeyFinder.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly:Dependency(typeof(WebDataService))]
namespace MonkeyFinder.Service
{
    class WebDataService : IDataService
    {

        HttpClient httpClient;
        HttpClient Client => httpClient ?? (httpClient = new HttpClient());

        public async Task<IEnumerable<Monkey>> GetMonkeysAsync()
        {
            var json = await Client.GetStringAsync("https://montemagno.com/monkeys.json");
            var all = Monkey.FromJson(json);
            return all;
        }
    }
}
