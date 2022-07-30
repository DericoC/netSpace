using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.Util;
using NetSpace.ViewModel;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class MyAccountView : ContentPage
    {
        private UserSession user = UserSession.getSession();
        public bool imageLoaded = false;
        UserService userService = new UserService();

        public MyAccountView()
        {
            InitializeComponent();
            BindingContext = new MyAccountViewModel();
            box.IsVisible = false;
            busyindicator.IsBusy = false;
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
                response = await userService.upload(imageToBytes, user.getUser().user_id, user.getUser().first_name);
                await Task.Delay(100);

                image.Source = Constants.PROFILES_PICS + "/" + user.getUser().user_id + "/" + response[1];
                user.getUser().image = Constants.PROFILES_PICS + "/" + user.getUser().user_id + "/" + response[1];
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
    }
}

