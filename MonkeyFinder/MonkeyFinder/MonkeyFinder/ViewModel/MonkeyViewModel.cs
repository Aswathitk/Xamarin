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
using MonkeyFinder.View;

namespace MonkeyFinder.ViewModel
{
    class MonkeyViewModel : BaseViewModel
    {

        public Command GetMonkeysCommand { get; }
        public Command GetNearestCommand { get; }

        public Command OpenMapCommand { get; }

        public ObservableRangeCollection<Monkey> Monkeys { get; }
        Monkey monkey;
        public Monkey Monkey
        {
            get => monkey;
            set
            {
                if (monkey == value)
                    return;
                monkey = value;
                Title = "Nearest Monkey";
                OnPropertyChanged();
            }
        }

        public Monkey first;

        public MonkeyViewModel()
        {
            Monkeys = new ObservableRangeCollection<Monkey>();
            Title = "Monkey Finder";
            GetMonkeysCommand = new Command(async () => await GetMonkeysAsync());
            //GetMonkeysCommand.Execute(null);
            GetNearestCommand = new Command(async () => await GetNearestAsync());
            OpenMapCommand = new Command(async () => await openMapAsync());
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

                 first = Monkeys.OrderBy(m => location.CalculateDistance(
                    new Location(m.Latitude, m.Longitude), DistanceUnits.Miles)).FirstOrDefault();

                if (first == null)
                    return;
                //await App.Current.MainPage.DisplayAlert("Closest Monkey",$"{first.Name} located in {first.Location}","OK");
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new NearestMonkey(first));

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

        async Task openMapAsync()
        {
            try
            {
                await Map.OpenAsync(Monkey.Latitude, Monkey.Longitude);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to launch maps: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error No Maps Apps", ex.Message, "OK");
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
