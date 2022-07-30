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
    public class AdminPlacesViewModel : BaseViewModel
    {
        public Command createPlace;
        public Command createPolicy;
        private UserSession ses = UserSession.getSession();
        public ObservableCollection<Place> places { get; set; }
        PlaceService placeService = new PlaceService();
        private readonly SnackBarAlert alert = new SnackBarAlert();

        public AdminPlacesViewModel()
        {
            createPlace = new Command(async () => await goToCreatePlace());
            createPolicy = new Command(async () => await goToPoliciesList());

            places = new ObservableCollection<Place>();
            foreach (var item in placeService.readSpecificBusiness(ses.getUser().provider))
            {
                places.Add(item);
            }
        }

        private async Task goToCreatePlace()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdminCreatePlaceView(null));
        }

        private async Task goToPoliciesList()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdminPolicyView());
        }

        public Command GoToCreatePlace
        {
            get => createPlace;
        }

        public Command GoToPoliciesList
        {
            get => createPolicy;
        }

        public async Task<bool> deletePlaceAsync(Place p)
        {
            bool valido = false;

            if(placeService.delete(p))
            {
                await alert.displaySnackBarAlertAsync("Lugar borrado.", 3, SnackBarAlert.INFORMATION);
                valido = true;
            } else
            {
                await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 3, SnackBarAlert.ERROR);
            }

            return valido;
        }
    }
}