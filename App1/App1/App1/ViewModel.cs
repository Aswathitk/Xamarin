using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1
{

    class ViewModel
    {
        public ObservableCollection<Items> TodoItems { get; set; }

        public ViewModel()
        {
            TodoItems = new ObservableCollection<Items>();
        }

        public ICommand AddTodoCommand => new Command(AddTodoItem);
        public String newTodoValue { get; set; }
        void AddTodoItem()
        {
            TodoItems.Add(new Items(newTodoValue, false));
        }

        

        public ICommand RemoveTodoCommand => new Command(RemoveTodoItem);
        void RemoveTodoItem(Object o)
        {
            Items itemToBeRemoved = o as Items;
            TodoItems.Remove(itemToBeRemoved);
        }
    }
}
