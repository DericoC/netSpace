using System;
using System.Threading.Tasks;
using NetSpace.Model;
using NetSpace.Util;
using NetSpace.View;
using Xamarin.Forms;

namespace NetSpace.ViewModel
{
    public class AdminAccountsViewModel
    {
        public Command addAccountCommand;
        public Command modifyAccountCommand;
        public Command logoutCommand;

        public AdminAccountsViewModel()
        {
            addAccountCommand = new Command(async () => await goToCreateAccount());
            modifyAccountCommand = new Command<User>(async (u) => await goToModifyAccount(u));
            logoutCommand = new Command(async () => await logoutAsync());
        }

        private async Task goToCreateAccount()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdminCreateAccountView(null));
        }

        private async Task goToModifyAccount(User account)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdminCreateAccountView(account));
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

