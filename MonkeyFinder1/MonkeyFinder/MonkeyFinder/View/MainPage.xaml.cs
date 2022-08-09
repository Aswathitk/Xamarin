using MonkeyFinder.Model;
using MonkeyFinder.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyFinder
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

         async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var monkey = e.SelectedItem as Monkey;
            if (monkey == null) return;

            await Navigation.PushAsync(new DetailsPage(monkey));
            ((ListView)sender).SelectedItem = null;
        }

        
    }
}
