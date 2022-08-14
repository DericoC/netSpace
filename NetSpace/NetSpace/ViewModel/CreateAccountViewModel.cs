using System;
using System.Threading.Tasks;
using NetSpace.ViewModel;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.View;
using Xamarin.Forms;
using NetSpace.Util;

namespace NetSpace.ViewModel
{
    public class CreateAccountViewModel : BaseViewModel
    {
        public User user { get; set; }
        public string rePassword { get; set; }
        public Command createAccount { get; set; }
        private UserService userService = new UserService();
        private readonly SnackBarAlert alert = new SnackBarAlert();

        public CreateAccountViewModel()
        {
            user = new User();
            createAccount = new Command(async () => await saveAccountAsync());
        }

        private async Task saveAccountAsync()
        {
            user.image = "no-image.png";
            user.role = "Cliente";
            user.provider = 0;
            if ((user.first_name != "" || user.first_name != null) && (user.last_name != "" || user.last_name != null) && (user.gender != "" &&user.gender != null) && (user.mail != "" || user.mail != null) && (user.phone != "" || user.phone != null) && (user.address != "" || user.address != null) && (user.password != "" ||user.password != null) && (rePassword != "" || rePassword != null))
            {
                if (user.password == rePassword)
                {
                    if (userService.insert(user))
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new LoginView());
                        await alert.displaySnackBarAlertAsync("Cuenta creada", 5, SnackBarAlert.INFORMATION);
                    } else
                    {
                        await alert.displaySnackBarAlertAsync("Ha ocurrido un error al crear la cuenta", 5, SnackBarAlert.WARNING);
                    }
                }
                else
                {
                    await alert.displaySnackBarAlertAsync("Las contraseñas deben ser iguales", 5, SnackBarAlert.WARNING);
                }
            } else
            {
                await alert.displaySnackBarAlertAsync("Debe completar todos los campos", 5, SnackBarAlert.WARNING);
            }
        }

        public Command CreateAccount
        {
            get => createAccount;
        }
    }
}

