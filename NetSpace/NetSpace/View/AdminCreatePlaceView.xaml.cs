using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.Util;
using NetSpace.ViewModel;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class AdminCreatePlaceView : ContentPage
    {
        private Place place;
        private PlaceService placeService = new PlaceService();
        public bool imageLoaded = false;

        public AdminCreatePlaceView(Place p)
        {
            InitializeComponent();
            place = p;
            BindingContext = new AdminCreatePlaceViewModel(p);
            if (Device.RuntimePlatform == Device.iOS)
            {
                tagsComboBox.HeightRequest = 20;
            }
            else
            {
                tagsComboBox.HeightRequest = 51;
            }
        }

        async void OnPickPhotoButtonClicked(object sender, EventArgs e)
        {
            string[] response = new string[2];
            (sender as SfButton).IsEnabled = false;
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    box.IsVisible = true;
                    busyindicator.IsBusy = true;
                });

                byte[] imageToBytes = GetImageStreamAsBytes(stream);
                response = await placeService.upload(imageToBytes, place.place_id, place.place_name);
                await Task.Delay(100);

                image.Source = Constants.PLACES_PICS + "/" + place.place_id + "/" + response[1];
                place.image = Constants.PLACES_PICS + "/" + place.place_id + "/" + response[1];
                imageLoaded = true;
            }
            (sender as SfButton).IsEnabled = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                box.IsVisible = false;
                busyindicator.IsBusy = false;
            });
        }

        private byte[] GetImageStreamAsBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        void tagsComboBox_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            List<object> c = (List<object>)e.Value;
            List<Tags> afterCast = c.Cast<Tags>().ToList();
            ((AdminCreatePlaceViewModel)(this.BindingContext)).selectedTags = afterCast;
        }
    }
}