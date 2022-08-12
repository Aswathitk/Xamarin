using MyCoffeeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyCoffeeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnimationPage : ContentPage
    {
        ImageCacheViewModel vm;
        readonly Animation rotation;
        public AnimationPage()
        {
            InitializeComponent();
            rotation = new Animation(v => LabelLoad.Rotation = v, 0, 360, Easing.Linear);
            vm = (ImageCacheViewModel)BindingContext;
            vm.PropertyChanged += vm_PropertyChanged;
        }

        private void vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(vm.IsBusy))
            {
                if(vm.IsBusy)
                {
                    rotation.Commit(this, "rotate", 16, 1000, Easing.Linear,
                        (v, c) => LabelLoad.Rotation = 0, () => true);
                }
                else
                {
                    this.AbortAnimation("rotate");
                }
            }
        }

        //private async void Button_Clicked(object sender, EventArgs e)
        //{
        //    //var a1 =  LabelLoad.FadeTo(0, 1000, Easing.Linear);
        //    //var a2 =  LabelLoad.ScaleTo(2.0, 1000, Easing.BounceIn);

        //    //await Task.WhenAll(a1, a2);

        //    //var a3 =  LabelLoad.ScaleTo(1.0, 1000, Easing.BounceOut);
        //    //var a4 =  LabelLoad.FadeTo(1, 1000, Easing.Linear);
        //    //await Task.WhenAll(a3, a4);

        //    await LabelLoad.RotateTo(360, 1000, Easing.Linear);
        //    LabelLoad.Rotation = 0;
        //}


    }
}