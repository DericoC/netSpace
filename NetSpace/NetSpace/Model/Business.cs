using System;
using NetSpace.ViewModel;

namespace NetSpace.Model
{
	public class Business : BaseViewModel
	{
		public int business_id { get; set; }
		public string business_name { get; set; }
		public int type { get; set; }
		public Place place { get; set; }


		public Business()
		{
		}

        public Business(int business_id, string business_name, int type, Place place)
        {
            this.business_id = business_id;
            this.business_name = business_name;
            this.type = type;
            this.place = place;
        }
    }
}

