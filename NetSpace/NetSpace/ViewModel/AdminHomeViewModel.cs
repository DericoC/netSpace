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
    public class AdminHomeViewModel : BaseViewModel
    {
        public ObservableCollection<Reservation> reservations { get; set; }
        public ObservableCollection<Place> places { get; set; }
        private Command placeDetailCommand;
        private Command reservationDetailCommand;
        public bool eventsEmpty { get; set; }
        public bool placesEmpty { get; set; }
        private int businessID { get; set; }
        PlaceService placeService = new PlaceService();
        ReservationService reservationService = new ReservationService();
        UserSession user = UserSession.getSession();

        public AdminHomeViewModel()
        {
            eventsEmpty = true;
            placesEmpty = true;
            businessID = user.getUser().provider;
            placeDetailCommand = new Command(async (p) => await loadSelectedPlaceAsync(p));
            reservationDetailCommand = new Command(async (r) => await loadSelectedReservationAsync(r));
            places = new ObservableCollection<Place>();
            reservations = new ObservableCollection<Reservation>();
            this.init();
        }

        public void init()
        {
            reservations.Clear();
            places.Clear();
            foreach (var item in reservationService.nearestReservations(businessID))
            {
                eventsEmpty = false;
                reservations.Add(item);
            }
            foreach (var item in placeService.topPlaces(businessID))
            {
                placesEmpty = false;
                places.Add(item);
            }
        }

        async Task loadSelectedReservationAsync(Object r)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdminReservationDetailView(r as Reservation));
        }

        async Task loadSelectedPlaceAsync(Object p)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PlaceView(p as Place, true));
        }

        public Command ReservationDetailCommand
        {
            get => reservationDetailCommand;
        }

        public Command PlaceDetailCommand
        {
            get => placeDetailCommand;
        }
    }
}

