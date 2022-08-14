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
    public class CreateBusinessViewModel
    {
        public Command createBusinessCommand;
        public Business business { get; set; }
        public string rePassword { get; set; }
        public User user { get; set; }
        public ObservableCollection<GeneralParameters> types { get; set; }
        private BusinessService businessService = new BusinessService();
        private UserService userService = new UserService();
        private GeneralParameterService generalParameterService = new GeneralParameterService();
        private readonly SnackBarAlert alert = new SnackBarAlert();

        public CreateBusinessViewModel()
        {
            user = new User();
            business = new Business();
            createBusinessCommand = new Command(async () => await createBusinessAsync());
            types = new ObservableCollection<GeneralParameters>();
            foreach (var item in generalParameterService.businessTypes())
            {
                types.Add(item);
            }
        }

        private async Task createBusinessAsync()
        {
            await businessService.insertAsync(business);
            int businessID = businessService.findLatestId(business.business_name, business.typeObject.general_parameter_id);

            user.image = "no-image.png";
            user.role = "Administrador";
            user.provider = businessID;

            if ((user.first_name != "" || user.first_name != null) && (user.last_name != "" || user.last_name != null) && (user.gender != "" && user.gender != null) && (user.mail != "" || user.mail != null) && (user.phone != "" || user.phone != null) && (user.address != "" || user.address != null) && (user.password != "" || user.password != null) && (rePassword != "" || rePassword != null))
            {
                if (user.password == rePassword)
                {
                    if (userService.insert(user))
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new LoginView());
                        await alert.displaySnackBarAlertAsync("Negocio y administrador creado", 5, SnackBarAlert.INFORMATION);
                    }
                    else
                    {
                        await alert.displaySnackBarAlertAsync("Ha ocurrido un error al crear la cuenta", 5, SnackBarAlert.WARNING);
                    }
                }
                else
                {
                    await alert.displaySnackBarAlertAsync("Las contraseñas deben ser iguales", 5, SnackBarAlert.WARNING);
                }
            }
            else
            {
                await alert.displaySnackBarAlertAsync("Debe completar todos los campos", 5, SnackBarAlert.WARNING);
            }

        }

        public Command CreateBusiness
        {
            get => createBusinessCommand;
        }
    }
}

