using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.Util;
using Xamarin.Forms;

namespace NetSpace.ViewModel
{
    public class AdminCreatePlaceViewModel
    {
        public Place place { get; set; }
        public Command createPlace;
        public Command savePlace;
        public Command pickImage;
        public ObservableCollection<User> managers { get; set; }
        public ObservableCollection<Policy> policies { get; set; }
        private readonly SnackBarAlert alert = new SnackBarAlert();
        private UserSession ses = UserSession.getSession();
        private UserService userService = new UserService();
        private PlaceService placeService = new PlaceService();
        private PolicyService policyService = new PolicyService();
        public bool isCreate { get; set; }

        public AdminCreatePlaceViewModel(Place p)
        {
            managers = new ObservableCollection<User>();
            foreach (var item in userService.businessManagers(ses.getUser().provider))
            {
                managers.Add(item);
            }
            policies = new ObservableCollection<Policy>();
            foreach (var item in policyService.businessPolicies(ses.getUser().provider))
            {
                policies.Add(item);
            }
            place = p != null ? p : new Place();
            createPlace = new Command(async () => await addPlace());
            savePlace = new Command(async () => await updatePlace());
            isCreate = p == null;
        }
        private async Task addPlace()
        {
            if (place.manager != null)
            {
                if (place.policy != null)
                {
                    if (placeService.insert(place))
                    {
                        Application.Current.MainPage.Navigation.PopAsync();
                        await alert.displaySnackBarAlertAsync("Lugar creado.", 5, SnackBarAlert.INFORMATION);
                    }
                    else
                    {
                        await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 5, SnackBarAlert.ERROR);
                    }
                } else
                {
                    await alert.displaySnackBarAlertAsync("Debe seleccionar una politica.", 5, SnackBarAlert.ERROR);
                }
            } else
            {
                await alert.displaySnackBarAlertAsync("Debe seleccionar un administrador.", 5, SnackBarAlert.ERROR);
            }
        }
        private async Task updatePlace()
        {
            if (placeService.update(place))
            {
                Application.Current.MainPage.Navigation.PopAsync();
                await alert.displaySnackBarAlertAsync("Lugar actualizado.", 5, SnackBarAlert.INFORMATION);
            }
            else
            {
                await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 5, SnackBarAlert.ERROR);
            }
        }

        public Command CreatePlace
        {
            get => createPlace;
        }

        public Command SavePlace
        {
            get => savePlace;
        }
    }
}