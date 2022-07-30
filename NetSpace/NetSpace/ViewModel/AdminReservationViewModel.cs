using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NetSpace.ViewModel;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.View;
using Xamarin.Forms;
using NetSpace.Util;

namespace NetSpace.ViewModel
{
    public class AdminReservationViewModel : BaseViewModel
    {
        public Command searchCommand;
        public Command menuCommand;
        public Command menuOptionCommand;
        private Command reservationDetailCommand;
        private UserSession ses = UserSession.getSession();
        public ObservableCollection<Reservation> reservation { get; set; }
        ReservationService reservationService = new ReservationService();

        public AdminReservationViewModel()
        {
            reservation = new ObservableCollection<Reservation>();
            foreach (var item in reservationService.readSpecific(ses.getUser().provider))
            {
                reservation.Add(item);
            }
            reservationDetailCommand = new Command<Reservation>(async (r) => await loadSelectedReservationAsync(r));
        }


        async Task loadSelectedReservationAsync(Reservation r)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdminReservationDetailView(r));
        }

        public Command ReservationDetailCommand
        {
            get { return reservationDetailCommand; }
            protected set { reservationDetailCommand = value; }
        }
    }
}

