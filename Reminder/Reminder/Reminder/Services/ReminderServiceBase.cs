namespace Reminder.Services
{
    public static class ReminderServiceBase
    {

        public static async Task<List<ReminderItem>> GetReminder()
        {
            await Init();
            return await db.Table<ReminderItem>().ToListAsync();

        }
    }
}