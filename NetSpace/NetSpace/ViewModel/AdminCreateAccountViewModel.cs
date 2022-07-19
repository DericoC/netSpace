using System;
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
        public Command createUser;
        private readonly SnackBarAlert alert = new SnackBarAlert();
        private UserService userService = new UserService();
        public bool isCreate { get; set; }

        public AdminCreateAccountViewModel(User u)
        {
            user = new User();
            createUser = new Command(async () => await addUser());
            isCreate = u == null;
        }

        private async Task addUser()
        {
            if (userService.insert(user))
            {
                await alert.displaySnackBarAlertAsync("Cuenta creada.", 3, SnackBarAlert.INFORMATION);
            }
            else
            {
                await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 3, SnackBarAlert.ERROR);
            }
        }

        public Command CreateUser
        {
            get => createUser;
        }
    }
}

