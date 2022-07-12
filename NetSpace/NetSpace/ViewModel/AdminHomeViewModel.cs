using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.View;
using Xamarin.Forms;

namespace NetSpace.ViewModel
{
    public class AdminHomeViewModel
    {
        public ObservableCollection<Reservation> reservations { get; set; }
        public ObservableCollection<Place> places { get; set; }
        private Command placeDetailCommand;
        PlaceService placeService = new PlaceService();
        ReservationService reservationService = new ReservationService();

        public AdminHomeViewModel()
        {
            reservations = new ObservableCollection<Reservation>();
            places = new ObservableCollection<Place>();
            placeDetailCommand = new Command(async (p) => await loadSelectedPlaceAsync(p));
            foreach (var item in reservationService.nearestReservations())
            {
                reservations.Add(item);
            }
            foreach (var item in placeService.topPlaces())
            {
                places.Add(item);
            }
        }

        async Task loadSelectedPlaceAsync(Object p)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PlaceView(p as Place));
        }

        public Command PlaceDetailCommand
        {
            get => placeDetailCommand;
        }
    }
}

