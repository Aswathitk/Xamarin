using MvvmHelpers.Commands;
using MyCoffeeApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyCoffeeApp.ViewModels
{
    class AddMyCoffeeViewModel : ViewModelBase
    {
        string name, roaster;
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Roaster { get => roaster; set => SetProperty(ref roaster, value); }
        public AsyncCommand SaveCommand { set; get; }
        ICoffeeService coffeeService;
        public AddMyCoffeeViewModel()
        {
            Title = "Add Coffee";
            SaveCommand = new AsyncCommand(save);
            coffeeService = DependencyService.Get<ICoffeeService>();
        }

        public async Task save()
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(roaster))
                return;
            await coffeeService.AddCoffee(name, roaster);
            await Shell.Current.GoToAsync("..");
        }
    }
}
