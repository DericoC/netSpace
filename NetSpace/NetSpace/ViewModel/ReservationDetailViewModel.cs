using System;
using NetSpace.ViewModel;
using NetSpace.Model;

namespace NetSpace.ViewModel
{
    public class ReservationDetailViewModel : BaseViewModel
    {
        public Reservation reservationDetail { get; set; }
        public String startTimeFormat { get; set; }
        public String endTimeFormat { get; set; }

        public ReservationDetailViewModel(Reservation r)
        {
            reservationDetail = new Reservation();
            reservationDetail.place = new Place();
            reservationDetail.user = new User();
            reservationDetail.business = new Business();
            reservationDetail.qr = r.qr;
            reservationDetail.date_slot = r.date_slot;
            reservationDetail.time_start = r.time_start;
            DateTime time = DateTime.Today.Add(r.time_start);
            startTimeFormat = time.ToString("hh:mm tt");
            reservationDetail.time_end = r.time_end;
            DateTime time2 = DateTime.Today.Add(r.time_end);
            endTimeFormat = time2.ToString("hh:mm tt");

            reservationDetail.place.image = r.place.image;
            reservationDetail.place.place_name = r.place.place_name;
            reservationDetail.place.description = r.place.description;

            reservationDetail.user.first_name = r.user.first_name;
            reservationDetail.user.last_name = r.user.last_name;

            reservationDetail.business.business_name = r.business.business_name;
            reservationDetail.business.type = r.business.type;


        }
    }
}