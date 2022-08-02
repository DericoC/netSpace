using System.Threading.Tasks;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.Util;
using NetSpace.View;
using Xamarin.Forms;

namespace NetSpace.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public string loginBtnText { get; set; }
        public User p { get; set; }
        UserSession ses;
        private UserService service = new UserService();
        public Command loginCommand;
        public Command createAccountCommand;
        public string mailErrorMsg { get; set; }
        public string passErrorMsg { get; set; }
        private readonly SnackBarAlert alert = new SnackBarAlert();

        public LoginViewModel()
        {
            loginBtnText = "LOG IN";
            p = new User();
            loginCommand = new Command(async () => await loginAsync());
            createAccountCommand = new Command(async () => await goToCreateAccountAsync());
        }

        async Task loginAsync()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                loginBtnText = "CARGANDO...";
            });
            await Task.Delay(100);
            ses = UserSession.getSession();
            if ((p.password != null || p.password != "") && (p.mail != null || p.mail == ""))
            {
                ses.getUser().mail = p.mail;
                ses.getUser().password = p.password;
                if (service.login(ses))
                {
                    if (ses.getUser().role == "Cliente")
                    {
                        NavigationPage home = new NavigationPage(new HomeView());
                        home.BackgroundColor = Color.White;
                        home.BarBackground = Color.White;
                        home.BarTextColor = Color.Black;
                        Application.Current.MainPage = home;
                    } else if (ses.getUser().role == "Administrador")
                    {
                        NavigationPage adminHome = new NavigationPage(new AdminTabbedView());
                        adminHome.BarBackground = Color.White;
                        adminHome.BackgroundColor = Color.White;
                        adminHome.BarTextColor = Color.Black;
                        Application.Current.MainPage = adminHome;
                    } else if (ses.getUser().role == "Manager")
                    {
                        await alert.displaySnackBarAlertAsync("Este usuario es gestor. Por favor contacte al administrador.", 5, SnackBarAlert.WARNING);
                    } else
                    {
                        await alert.displaySnackBarAlertAsync("Ha ocurrido un error inesperado.", 5, SnackBarAlert.ERROR);
                    }
                }
                else
                {
                    await alert.displaySnackBarAlertAsync("Usuario o contraseña incorrecto.", 5, SnackBarAlert.ERROR);
                }
            } else
            {
                await alert.displaySnackBarAlertAsync("Usuario o contraseña incorrecto", 5, SnackBarAlert.ERROR);
                mailErrorMsg = p.mail == null || p.mail.Length == 0 ? "Email required" : "";
                passErrorMsg = p.password == null ? "Password required" : "";
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                loginBtnText = "Log In";
            });
        }

        async Task goToCreateAccountAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CreateAccountView());
        }

        public Command CreateAccountCommand
        {
            get => createAccountCommand;
        }

        public Command LoginCommand
        {
            get => loginCommand;
        }
    }
}

