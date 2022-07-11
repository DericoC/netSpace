using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace NetSpace.Util
{
    public class SnackBarAlert
    {
        public readonly static Color SUCCESS = Color.FromRgb(103, 173, 91);
        public readonly static Color INFORMATION = Color.FromRgb(70, 150, 236);
        public readonly static Color WARNING = Color.FromRgb(241, 154, 56);
        public readonly static Color ERROR = Color.FromRgb(169, 37, 27);

        public SnackBarAlert()
        {
        }

        public async Task displaySnackBarAlertAsync(String message, int duration, Color type)
        {
            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Padding = 10,
                    Foreground = Color.White,
                    Message = message
                },
                CornerRadius = 15,
                BackgroundColor = type,
                Duration = TimeSpan.FromSeconds(duration),
            };

            await Application.Current.MainPage.DisplaySnackBarAsync(options);
        }
    }
}

