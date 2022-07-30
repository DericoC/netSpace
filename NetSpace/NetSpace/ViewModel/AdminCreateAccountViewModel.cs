using System.Threading.Tasks;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.Util;
using Xamarin.Forms;

namespace NetSpace.ViewModel
{
    public class AdminCreateAccountViewModel : BaseViewModel
    {
        public User user { get; set; }
        public string rePassword { get; set; }
        public Command createUser;
        public Command saveUser;
        public Command pickImage;
        private readonly SnackBarAlert alert = new SnackBarAlert();
        private UserService userService = new UserService();
        private UserSession ses = UserSession.getSession();
        public bool isCreate { get; set; }

        public AdminCreateAccountViewModel(User u)
        {
            user = u != null ? u : new User();
            createUser = new Command(async () => await addUser());
            saveUser = new Command(async () => await updateUser());
            isCreate = u == null;
            rePassword = !isCreate ? u.password : null;
        }

        private async Task addUser()
        {
            if (user.password == rePassword)
            {
                user.provider = ses.getUser().provider;
                if (userService.insert(user))
                {
                    Application.Current.MainPage.Navigation.PopAsync();
                    await alert.displaySnackBarAlertAsync("Cuenta creada.", 5, SnackBarAlert.INFORMATION);
                }
                else
                {
                    await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 5, SnackBarAlert.ERROR);
                }
            } else
            {
                await alert.displaySnackBarAlertAsync("Las contraseñas deben ser iguales", 5, SnackBarAlert.WARNING);
            }
        }

        private async Task updateUser()
        {
            if (user.password == rePassword)
            {
                if (userService.update(user))
                {
                    Application.Current.MainPage.Navigation.PopAsync();
                    await alert.displaySnackBarAlertAsync("Usuario actualizado.", 3, SnackBarAlert.INFORMATION);
                }
                else
                {
                    await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 3, SnackBarAlert.ERROR);
                }
            }
        }

        public Command CreateUser
        {
            get => createUser;
        }

        public Command SaveUser
        {
            get => saveUser;
        }
    }
}

