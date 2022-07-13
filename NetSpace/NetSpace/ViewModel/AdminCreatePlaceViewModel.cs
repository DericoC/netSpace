using System;
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
        private readonly SnackBarAlert alert = new SnackBarAlert();
        private PlaceService placeService = new PlaceService();
        public bool isCreate { get; set; }

        public AdminCreatePlaceViewModel(Place p)
        {
            place = new Place();
            createPlace = new Command(async () => await addPlace());
            isCreate = p == null;
        }

        private async Task addPlace()
        {
            if (placeService.insert(place))
            {
                await alert.displaySnackBarAlertAsync("Lugar creado.", 3, SnackBarAlert.INFORMATION);
            } else
            {
                await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 3, SnackBarAlert.ERROR);
            }
        }

        public Command CreatePlace
        {
            get => createPlace;
        }
    }
}

