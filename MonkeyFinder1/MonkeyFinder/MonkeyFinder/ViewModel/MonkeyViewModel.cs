using MonkeyFinder.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using System.Diagnostics;
using MvvmHelpers.Commands;
using Xamarin.Essentials;
using System.Linq;

namespace MonkeyFinder.ViewModel
{
    class MonkeyViewModel : BaseViewModel
    {

        public Command GetMonkeysCommand { get; }
        public Command GetNearestCommand { get; }

        public ObservableRangeCollection<Monkey> Monkeys { get; }

        public MonkeyViewModel()
        {
            Monkeys = new ObservableRangeCollection<Monkey>();
            Title = "Monkey Finder";
            GetMonkeysCommand = new Command(async () => await GetMonkeysAsync());
            //GetMonkeysCommand.Execute(null);
            GetNearestCommand = new Command(async () => await GetNearestAsync());
        }

       
        public async Task GetNearestAsync()
        {
            if (IsBusy || Monkeys.Count == 0)
                return;
            try
            {
                IsBusy = true;
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    }); ;
                }

                var first = Monkeys.OrderBy(m => location.CalculateDistance(
                    new Location(m.Latitude, m.Longitude), DistanceUnits.Miles)).FirstOrDefault();

                if (first == null)
                    return;
                await App.Current.MainPage.DisplayAlert("Closest Monkey",$"{first.Name} located in {first.Location}","OK");
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Unable to get monkeys {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error!", ex.Message, " OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task GetMonkeysAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var monkeys = await DataService.GetMonkeysAsync();
                Monkeys.ReplaceRange(monkeys);
                Title = $"Monkey Finder {Monkeys.Count}";
                
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Unable to get monkeys {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error!", ex.Message ," OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
