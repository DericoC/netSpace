using System;
using System.Collections.ObjectModel;
using NetSpace.Model;
using NetSpace.Service;

namespace NetSpace.ViewModel
{
    public class AdminHomeViewModel
    {
        public ObservableCollection<Reservation> reservations { get; set; }
        public ObservableCollection<Place> places { get; set; }
        PlaceService placeService = new PlaceService();
        ReservationService reservationService = new ReservationService();

        public AdminHomeViewModel()
        {
            reservations = new ObservableCollection<Reservation>();
            places = new ObservableCollection<Place>();
            foreach (var item in reservationService.nearestReservations())
            {
                reservations.Add(item);
            }
            foreach (var item in placeService.topPlaces())
            {
                places.Add(item);
            }
        }
    }
}

