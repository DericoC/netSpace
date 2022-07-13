using System;
using System.Threading.Tasks;
using NetSpace.Model;
using NetSpace.View;
using Xamarin.Forms;

namespace NetSpace.ViewModel
{
    public class AdminPlacesViewModel
    {
        public Command createPlace;
        public Command modifyPlace;
        public Command createPolicy;

        public AdminPlacesViewModel()
        {
            createPlace = new Command(async () => await goToCreatePlace());
            modifyPlace = new Command<Place>(async (p) => await goToModifyPlace(p));
            createPolicy = new Command(async () => await goToPoliciesList());
        }

        private async Task goToCreatePlace()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdminCreatePlaceView(null));
        }

        private async Task goToModifyPlace(Place place)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdminCreatePlaceView(place));
        }

        private async Task goToPoliciesList()
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new );
        }

        public Command GoToCreatePlace
        {
            get => createPlace;
        }

        public Command GoToPoliciesList
        {
            get => createPolicy;
        }
    }
}

