using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Models
{
    class ReminderItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Complete { get; set; }

        
    }
}
