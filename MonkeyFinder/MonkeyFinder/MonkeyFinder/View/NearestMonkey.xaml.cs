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
    public partial class NearestMonkey : ContentPage
    {
        public NearestMonkey()
        {
            InitializeComponent();
        }

        public NearestMonkey(Monkey first)
        {
            InitializeComponent();
            ((MonkeyViewModel)BindingContext).Monkey = first;
        }

    
    }
}