using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NetSpace.ViewModel;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.View;
using Xamarin.Forms;

namespace NetSpace.ViewModel
{
    public class ReservationViewModel : BaseViewModel
    {
        public Command searchCommand;
        public Command menuCommand;
        public Command menuOptionCommand;
        private Command reservationDetailCommand;
        public ObservableCollection<Reservation> reservation { get; set; }
        ReservationService reservationService = new ReservationService();

        public ReservationViewModel()
        {
            reservation = new ObservableCollection<Reservation>();
            foreach (var item in reservationService.read())
            {
                reservation.Add(item);
            }
            reservationDetailCommand = new Command<Reservation>(async (r) => await loadSelectedReservationAsync(r));
        }


        async Task loadSelectedReservationAsync(Reservation r)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ReservationDetailView(r));
        }

        public Command ReservationDetailCommand
        {
            get { return reservationDetailCommand; }
            protected set { reservationDetailCommand = value; }
        }
    }
}