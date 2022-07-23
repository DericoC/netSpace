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
        private readonly SnackBarAlert alert = new SnackBarAlert();
        private PlaceService placeService = new PlaceService();
        public bool isCreate { get; set; }
        public AdminCreatePlaceViewModel(Place p)
        {
            place = p;
            createPlace = new Command(async () => await addPlace());
            savePlace = new Command(async () => await updatePlace());
            isCreate = p == null;
        }
        private async Task addPlace()
        {
            if (placeService.insert(place))
            {
                await alert.displaySnackBarAlertAsync("Lugar creado.", 3, SnackBarAlert.INFORMATION);
            }
            else
            {
                await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 3, SnackBarAlert.ERROR);
            }
        }
        private async Task updatePlace()
        {
            if (placeService.update(place))
            {
                await alert.displaySnackBarAlertAsync("Lugar actualizado.", 3, SnackBarAlert.INFORMATION);
            }
            else
            {
                await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 3, SnackBarAlert.ERROR);
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