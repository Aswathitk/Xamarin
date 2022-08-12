using MvvmHelpers;
using MvvmHelpers.Commands;
using MyCoffeeApp.Models;
using MyCoffeeApp.Services;
using MyCoffeeApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyCoffeeApp.ViewModels
{
    public class MyCoffeeViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Coffee> Coffee { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Coffee> RemoveCommand { get; }
        public AsyncCommand<Coffee> SelectedCommand { get; }

        ICoffeeService coffeeService;
        public MyCoffeeViewModel()
        {
            string Title = "MyCoffeeApp";
            Coffee = new ObservableRangeCollection<Coffee>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Coffee>(Remove);
            SelectedCommand = new AsyncCommand<Coffee>(Selected);
            coffeeService = DependencyService.Get<ICoffeeService>();

        }
            async Task Add()
            {
                //var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Name of Coffee");
                //var roaster = await App.Current.MainPage.DisplayPromptAsync("Roaster", "Roaster of Coffee");
                //await CoffeeService.AddCoffee(name, roaster);
                //await Refresh();
                var route = $"{nameof(AddMyCoffeePage)}";
                await Shell.Current.GoToAsync(route);
            }

            async Task Selected(Coffee coffee)
            {
                if (coffee == null)
                    return;
                var route = $"{nameof(MyCoffeeDetailsPage)}?CoffeeId={coffee.Id}";
                await Shell.Current.GoToAsync(route);
            }

            async Task Remove(Coffee coffee)
            {
                await coffeeService.RemoveCoffee(coffee.Id);
                await Refresh();
            }

            async Task Refresh()
            {
            IsBusy = true;
            await Task.Delay(2000);
            Coffee.Clear();
            var coffees = await coffeeService.GetCoffee();
            Coffee.AddRange(coffees);
            IsBusy = false;
            DependencyService.Get<IToast>()?.MakeToast("Refreshed!");
            }
       

    }
}
