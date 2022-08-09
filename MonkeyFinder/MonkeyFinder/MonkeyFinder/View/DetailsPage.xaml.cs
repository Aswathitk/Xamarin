using MonkeyFinder.Model;
using MonkeyFinder.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonkeyFinder.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage()
        {
            InitializeComponent();
        }

        public DetailsPage(Monkey monkey) :this()
        {
            ((MonkeyDetailsViewModel)BindingContext).Monkey =  monkey;
        }
    }
}