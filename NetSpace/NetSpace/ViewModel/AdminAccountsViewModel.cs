using System;
using System.Threading.Tasks;
using NetSpace.Util;
using Xamarin.Forms;

namespace NetSpace.ViewModel
{
    public class AdminAccountsViewModel
    {
        private Command addAccountCommand;
        private Command logoutCommand;

        public AdminAccountsViewModel()
        {
            logoutCommand = new Command(async () => await logoutAsync());
        }

        private async Task logoutAsync()
        {
            UserSession ses = UserSession.getSession();
            ses.reset();
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        public Command AddAccountCommand
        {
            get => addAccountCommand;
        }

        public Command LogoutCommand
        {
            get => logoutCommand;
        }
    }
}

