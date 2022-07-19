using System;
using NetSpace.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetSpace
{
    public partial class App : Application
    {
        public App ()
        {
            //V0.3.1
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Njc5MzM2QDMyMzAyZTMyMmUzMER2L3dQV1dzMEk3VG5aR0NiUzRxRDRZV0NVSmZhMEh4NDdodHFUTmY2TGs9");
            InitializeComponent();
            NavigationPage page = new NavigationPage(new LoginView());
            page.BarTextColor = Color.Black;
            MainPage = page;
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

