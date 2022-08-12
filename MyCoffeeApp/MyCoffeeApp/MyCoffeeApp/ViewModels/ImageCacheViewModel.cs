using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Command = Xamarin.Forms.Command;

namespace MyCoffeeApp.ViewModels
{
    class ImageCacheViewModel :ViewModelBase
    {
        public UriImageSource Image { get; set; } = new UriImageSource
        {
            Uri = new Uri("https://images.prismic.io/yesplz/f4dd8d9e-fb65-46da-8a29-58e667250fdf_194label1.jpg?auto=compress,format"),
            CachingEnabled=true,
            CacheValidity=TimeSpan.FromMinutes(1)
        };

        public Command RefreshCommand { set; get; }
        public AsyncCommand RefreshLongCommand { set; get; }

        public ImageCacheViewModel()
        {
            RefreshCommand = new Command(() =>
            {
                Image = new UriImageSource
                {
                    Uri = new Uri("https://images.prismic.io/yesplz/f4dd8d9e-fb65-46da-8a29-58e667250fdf_194label1.jpg?auto=compress,format"),
                    CachingEnabled = true,
                    CacheValidity = TimeSpan.FromMinutes(1)
                };
        OnPropertyChanged(nameof(Image));
            });
            RefreshLongCommand = new AsyncCommand(async () =>
              {
                  IsBusy = true;
                  await Task.Delay(5000);
                  IsBusy = false;
              });
        }
    }
}
