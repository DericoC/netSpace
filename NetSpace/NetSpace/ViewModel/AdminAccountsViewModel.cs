using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.Util;
using NetSpace.View;
using Xamarin.Forms;

namespace NetSpace.ViewModel
{
    public class AdminAccountsViewModel : BaseViewModel
    {
        public Command addAccountCommand;
        public Command modifyAccountCommand;
        public Command logoutCommand;
        private UserSession ses = UserSession.getSession();
        public ObservableCollection<User> users { get; set; }
        private UserService userService = new UserService();
        private readonly SnackBarAlert alert = new SnackBarAlert();

        public AdminAccountsViewModel()
        {
            addAccountCommand = new Command(async () => await goToCreateAccount());
            modifyAccountCommand = new Command<User>(async (u) => await goToModifyAccount(u));
            logoutCommand = new Command(async () => await logoutAsync());
            this.init();
        }

        public void init()
        {
            users = new ObservableCollection<User>();
            foreach (var item in userService.readSpecific(ses.getUser().provider))
            {
                users.Add(item);
            }
        }

        private async Task goToCreateAccount()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdminCreateAccountView(null));
        }

        public async Task goToModifyAccount(User account)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdminCreateAccountView(account));
        }

        private async Task logoutAsync()
        {
            UserSession ses = UserSession.getSession();
            ses.reset();
            Application.Current.MainPage = new NavigationPage(new LoginView());
        }

        public async Task<bool> deletePlaceAsync(User u)
        {
            bool valido = false;

            if (userService.delete(u))
            {
                await alert.displaySnackBarAlertAsync("Lugar borrado.", 3, SnackBarAlert.INFORMATION);
                valido = true;
            }
            else
            {
                await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 3, SnackBarAlert.ERROR);
            }

            return valido;
        }

        public Command AddAccountCommand
        {
            get => addAccountCommand;
        }

        public Command ModifyAccountCommand
        {
            get => modifyAccountCommand;
        }

        public Command LogoutCommand
        {
            get => logoutCommand;
        }
    }
}

