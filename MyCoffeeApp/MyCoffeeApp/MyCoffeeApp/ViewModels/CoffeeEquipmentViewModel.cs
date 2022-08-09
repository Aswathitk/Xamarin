using MvvmHelpers;
using MvvmHelpers.Commands;
using MyCoffeeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffeeApp.ViewModels
{
    public class CoffeeEquipmentViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Coffee> Coffee { get; set; }
        public ObservableRangeCollection<Grouping<string, Coffee>> CoffeeGroups { get; set; }

        public AsyncCommand<Coffee> FavouriteCommand { get; }
        public Command LoadMoreCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public Command DelayLoadMoreCommand { get; }
        public Command ClearCommand { get; }
        public CoffeeEquipmentViewModel()
        {
            Title = "Coffee Equipment";

            Coffee = new ObservableRangeCollection<Coffee>();
            CoffeeGroups = new ObservableRangeCollection<Grouping<string, Coffee>>();



            LoadMore();
            

            RefreshCommand = new AsyncCommand(Refresh);
            FavouriteCommand = new AsyncCommand<Coffee>(Favourite);
            LoadMoreCommand = new Command(LoadMore);
            SelectedCommand = new AsyncCommand<Object>(selected);
            DelayLoadMoreCommand = new Command(DelayLoadMore);
            ClearCommand = new Command(Clear);
        }

        void LoadMore()
        {
            if (Coffee.Count >= 20)
                return;
            string image = "https://images.prismic.io/yesplz/f4dd8d9e-fb65-46da-8a29-58e667250fdf_194label1.jpg?auto=compress,format";
            Coffee.Add(new Coffee { Roaster = "Yes Please", Name = "Sip of Sunshine", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Please", Name = "Energy Drink", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Rise and shine", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Please", Name = "Energy Drink", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Rise and shine", Image = image });

            CoffeeGroups.Clear();

            CoffeeGroups.Add(new Grouping<string, Coffee>("Blue Bottle", Coffee.Where(c => c.Roaster == "Blue Bottle")));
            CoffeeGroups.Add(new Grouping<string, Coffee>("Yes Please", Coffee.Where(c => c.Roaster == "Yes Please")));
        }

        async Task Favourite(Coffee coffee)
        {
            if (coffee == null)
                return;
            await App.Current.MainPage.DisplayAlert("Favourite", coffee.Name, "OK!");


        }

        Coffee previouslySelectedCoffee;
        Coffee selectedCoffee;
        public Coffee SelectedCoffee
        {
            get => selectedCoffee;
            set => SetProperty(ref selectedCoffee, value);
            
        }

        async Task selected(Object args)
        {
            var coffee = args as Coffee;
            if (coffee == null)
                return;
            SelectedCoffee = null;
            await App.Current.MainPage.DisplayAlert("Selected", coffee.Name, "OK!");
        }
        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }

        void DelayLoadMore()
        {
            if (Coffee.Count <= 10)
                return;
            LoadMore();
        }

        void Clear()
        {
            Coffee.Clear();
            CoffeeGroups.Clear();

        }
    }
}
