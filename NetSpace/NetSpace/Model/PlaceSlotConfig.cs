using System;
using NetSpace.ViewModel;

namespace NetSpace.Model
{
	public class PlaceSlotConfig : BaseViewModel
	{
		public int place_slot_config_id { get; set; }
		public GeneralParameters frequency { get; set; }
		public int start_time { get; set; }
		public int end_time { get; set; }
		public Place place;

		public PlaceSlotConfig()
		{
		}

        public PlaceSlotConfig(int place_slot_config_id, GeneralParameters frequency, int start_time, int end_time, Place place)
        {
            this.place_slot_config_id = place_slot_config_id;
            this.frequency = frequency;
            this.start_time = start_time;
            this.end_time = end_time;
            this.place = place;
        }
    }
}

