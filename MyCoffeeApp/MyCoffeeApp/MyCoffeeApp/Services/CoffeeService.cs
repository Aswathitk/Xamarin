using MyCoffeeApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyCoffeeApp.Services
{
    public static class CoffeeService
    {
        static SQLiteAsyncConnection db;
        static async Task init()
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Mydata.db");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<Coffee>();
        }

        public static async Task AddCoffee(string name, string roaster)
        {
            await init();
            string image = "https://images.prismic.io/yesplz/f4dd8d9e-fb65-46da-8a29-58e667250fdf_194label1.jpg?auto=compress,format";
            var coffee = new Coffee
            {
                Roaster = roaster,
                Name = name,
                Image = image
            };
            await db.InsertAsync(coffee);
        }
        public static async Task RemoveCoffee(int id)
        {
            await init();
            await db.DeleteAsync<Coffee>(id);
        }

        public static async Task<IEnumerable<Coffee>> GetCoffee()
        {
            await init();
            var coffee =  await db.Table<Coffee>().ToListAsync();
            return coffee;
        }

        public static async Task<Coffee> GetCoffee(int id)
        {
            await init();
            var coffee = await db.Table<Coffee>().FirstOrDefaultAsync(c=>c.Id==id);
            return coffee;
        }
    }
}
