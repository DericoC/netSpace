using System;
using System.Collections.Generic;
using System.Linq;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.Util;
using NetSpace.ViewModel;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class CalendarView : ContentPage
    {
        private readonly SnackBarAlert alert = new SnackBarAlert();
        int PlaceID = 0;
        int BusinessID = 0;
        UserSession ses = UserSession.getSession();
        ReservationService reservationService = new ReservationService();

        public CalendarView(int placeId, int business_id)
        {
            InitializeComponent();
            BindingContext = new CalendarViewModel(placeId);
            PlaceID = placeId;
            BusinessID = business_id;
        }

        private async void Calendar_InlineItemTapped(object sender, InlineItemTappedEventArgs e)
        {
            var appointment = e.InlineEvent;
            bool answer = await DisplayAlert(appointment.Subject, appointment.StartTime.ToString(), "Confirmar", "Cancelar");
            if (answer)
            {
                Reservation reservation = new Reservation();
                reservation.user = new User();
                reservation.place = new Place();
                reservation.business = new Business();
                reservation.user.user_id = ses.getUser().user_id;
                reservation.place.place_id = PlaceID;
                reservation.business.business_id = BusinessID;
                reservation.qr = RandomString(10);
                reservation.date_slot = e.SelectedDate;
                reservation.time_start = new TimeSpan(appointment.StartTime.Hour, appointment.StartTime.Minute, 0);
                reservation.time_end = new TimeSpan(appointment.EndTime.Hour, appointment.EndTime.Minute, 0);

                if (reservationService.insert(reservation))
                {
                    await alert.displaySnackBarAlertAsync("Se ha realizado la reserva.", 5, SnackBarAlert.INFORMATION);
                    await Navigation.PushAsync(new HomeView());
                } else
                {
                    await alert.displaySnackBarAlertAsync("Ha ocurrido un error", 5, SnackBarAlert.ERROR);
                }
            }
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

