using MonkeyFinder.Model;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MonkeyFinder.ViewModel
{
    public class MonkeyDetailsViewModel : BaseViewModel
    {
        public Command OpenMapCommand { get; }

        public MonkeyDetailsViewModel()
        {
            OpenMapCommand = new Command(async () => await openMapAsync());
        }

        public MonkeyDetailsViewModel(Monkey monkey) : this()
        {

        }

        async Task openMapAsync()
        {
            try
            {
                await Map.OpenAsync(Monkey.Latitude, Monkey.Longitude);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Unable to launch maps: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error No Maps Apps", ex.Message, "OK");
            }
        }

        Monkey monkey;
        public Monkey Monkey
        {
            get => monkey;
            set
            {
                if(monkey == value)
                    return;
                monkey = value;
                Title = "Monkey Finder";
                OnPropertyChanged();
            }
        }
    }
}
