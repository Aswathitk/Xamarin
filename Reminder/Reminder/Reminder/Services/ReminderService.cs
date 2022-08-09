using Reminder.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Reminder.Services
{
    public static class ReminderService
    {
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
                return;
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<ReminderItem>();
        }

        public static async Task AddReminder(string text, bool complete)
        {
            await Init();
            var reminder = new ReminderItem
            {
                Text = text,
                Complete = complete
            };
            var id = await db.InsertAsync(reminder);
        }

        public static async Task RemoveReminder(int id)
        {
            await Init();
            await db.DeleteAsync<ReminderItem>(id);
        }

        public static async Task<IEnumerable<ReminderItem>> GetReminder()
        {
            await Init();
            var ReminderList = await db.Table<ReminderItem>().ToListAsync();
            return ReminderList;
        }

    }
}
