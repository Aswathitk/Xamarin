using MvvmHelpers.Commands;
using Reminder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Reminder.Services;

namespace Reminder.ViewModels
{
    class ReminderViewModel
    {
        public ObservableCollection<ReminderItem> TodoItems { get; set; }

        //public AsyncCommand AddReminderCommand { get; }

        //public AsyncCommand<ReminderItem> RemoveReminderCommand { get; }

        public ReminderViewModel()
        {
            TodoItems = new ObservableCollection<ReminderItem>();
        }

        public ICommand AddTodoCommand => new Command(AddTodoItem);
        public String newTodoValue { get; set; }
        void AddTodoItem()
        {
            //TodoItems.Add(new ReminderItem(newTodoValue, false));
            _ = ReminderService.AddReminder(newTodoValue, false);
        }



        public ICommand RemoveTodoCommand => new Command(RemoveTodoItem);
        void RemoveTodoItem(Object o)
        {
            ReminderItem itemToBeRemoved = o as ReminderItem;
            _ = ReminderService.RemoveReminder(itemToBeRemoved.Id);
        }
    }
}
