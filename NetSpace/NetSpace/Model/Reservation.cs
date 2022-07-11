using System;
using NetSpace.ViewModel;

namespace NetSpace.Model
{
	public class Reservation : BaseViewModel
	{
		public int reservation_id { get; set; }
		public User user { get; set; }
		public Place place { get; set; }
		public Business business { get; set; }
		public string qr { get; set; }
		public DateTime date_slot { get; set; }
		public TimeSpan time_start { get; set; }
		public TimeSpan time_end { get; set; }

		public Reservation()
		{	
		}

        public Reservation(int reservation_id, User user, Place place, Business business, string qr, DateTime date_slot, TimeSpan time_start, TimeSpan time_end)
        {
            this.reservation_id = reservation_id;
            this.user = user;
            this.place = place;
            this.business = business;
            this.qr = qr;
            this.date_slot = date_slot;
            this.time_start = time_start;
            this.time_end = time_end;
        }
    }
}

