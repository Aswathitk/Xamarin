using MyCoffeeApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyCoffeeApp.Services
{
    class InternetCoffeeService
    {
        static string Baseurl = DeviceInfo.Platform == DevicePlatform.Android ?
            "http://10.0.2.2:44313" : "http://localhost:5000";

        static HttpClient client;
        

        static InternetCoffeeService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(Baseurl)
            };
            
        }

        public static async Task<IEnumerable<Coffee>> GetCoffee()
        {
            var json = await client.GetStringAsync("/api/Coffee");
            var coffees = JsonConvert.DeserializeObject<IEnumerable<Coffee>>(json);
            return coffees;
        }

        static Random random = new Random();

        public static async Task AddCoffee(string name,string roaster)
        {
            string image = "https://images.prismic.io/yesplz/f4dd8d9e-fb65-46da-8a29-58e667250fdf_194label1.jpg?auto=compress,format";
            var coffee = new Coffee
            {
                Roaster = roaster,
                Name = name,
                Image = image,
                Id = random.Next(0, 1000)
            };
            var json = JsonConvert.SerializeObject(coffee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/Coffee", content);

            if (!response.IsSuccessStatusCode)
            {

            }
        }

        public static async Task RemoveCoffee(int id)
        {
            var response = await client.DeleteAsync($"/api/coffee/{id}");
            if (!response.IsSuccessStatusCode)
            {

            }
        }
    }
}
