using System;
using System.IO;
using NetSpace.Model;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class AdminCreatePlaceView : ContentPage
    {
        public bool imageLoaded = false;
        public AdminCreatePlaceView(Place p)
        {
            InitializeComponent();
            BindingContext = new AdminCreatePlaceViewModel(p);
        }
        async void OnPickPhotoButtonClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                image.Source = ImageSource.FromStream(() => stream);
                imageLoaded = true;
            }
            (sender as Button).IsEnabled = true;
        }
    }
}