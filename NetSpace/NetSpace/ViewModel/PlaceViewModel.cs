using System;
using NetSpace.ViewModel;
using NetSpace.Model;

namespace NetSpace.ViewModel
{
    public class PlaceViewModel : BaseViewModel
    {
        public Place placeDetail { get; set; }
        public String hasRestrooms { get; set; }

        public PlaceViewModel(Place p)
        {
            placeDetail = new Place();
            placeDetail.policy = new Policy();
            placeDetail.image = (string) p.GetType().GetProperty("image").GetValue(p, null);
            placeDetail.place_name = p.place_name;
            placeDetail.description = p.description;
            placeDetail.dimensions = p.dimensions + "m2";
            placeDetail.capacity = p.capacity;
            placeDetail.amenities = p.amenities.ToString();
            placeDetail.restrooms = p.restrooms;
            hasRestrooms = p.restrooms ? "Sí" : "No";
            placeDetail.policy.price = p.policy.price;
            placeDetail.policy.deposit = p.policy.deposit;
            placeDetail.policy.policy_name = p.policy.policy_name;
        }
    }
}

