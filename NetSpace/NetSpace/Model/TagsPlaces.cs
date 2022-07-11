using System;
using NetSpace.ViewModel;

namespace NetSpace.Model
{
	public class TagsPlaces : BaseViewModel
	{
		public Place place;
		public Tags tag;

		public TagsPlaces()
		{
		}

        public TagsPlaces(Place place, Tags tag)
        {
            this.place = place;
            this.tag = tag;
        }
    }
}

