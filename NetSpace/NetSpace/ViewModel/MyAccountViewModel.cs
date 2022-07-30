using System;
using System.Threading.Tasks;
using NetSpace.ViewModel;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.Util;
using Xamarin.Forms;

namespace NetSpace.ViewModel
{
    public class MyAccountViewModel : BaseViewModel
    {
        public User user { get; set; }
        public Command saveAccount;
        private UserService service = new UserService();
        private readonly SnackBarAlert alert = new SnackBarAlert();

        public MyAccountViewModel()
        {
            user = new User();
            user = UserSession.getSession().getUser();
            saveAccount = new Command(async () => await saveAccountDetailsAsync());
        }

        private async Task saveAccountDetailsAsync()
        {
            if (service.update(user))
            {
                Application.Current.MainPage.Navigation.PopAsync();
                await alert.displaySnackBarAlertAsync("Cuenta actualizada", 5, SnackBarAlert.INFORMATION);
            } else
            {
                await alert.displaySnackBarAlertAsync("Ha ocurrido un error. Intente mas tarde.", 5, SnackBarAlert.ERROR);
            }
        }

        public Command SaveAccount
        {
            get => saveAccount;
        }
    }
}

