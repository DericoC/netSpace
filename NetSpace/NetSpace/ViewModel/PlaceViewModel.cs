using System;
using NetSpace.ViewModel;
using NetSpace.Model;
using Xamarin.Forms;
using System.Threading.Tasks;
using NetSpace.View;
using NetSpace.Service;
using NetSpace.Util;
using System.Threading;

namespace NetSpace.ViewModel
{
    public class PlaceViewModel : BaseViewModel
    {
        public Place placeDetail { get; set; }
        public string hasRestrooms { get; set; }
        public bool isBusy { get; set; }
        public Command goToCalendar;
        public bool viewAsAdmin { get; set; }

        public PlaceViewModel(Place p, bool isAdmin)
        {
            viewAsAdmin = isAdmin;
            placeDetail = new Place();
            placeDetail.business = new Business();
            placeDetail.business = p.business;
            placeDetail.place_id = p.place_id;
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
            goToCalendar = new Command(async () => await moveToCalendarAsync());
        }

        private async Task moveToCalendarAsync()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                isBusy = true;
            });

            await Task.Delay(100);

            BusinessService businessService = new BusinessService();
            await Application.Current.MainPage.Navigation.PushAsync(new CalendarView(placeDetail.place_id, placeDetail.business.business_id, placeDetail.place_name));
            Device.BeginInvokeOnMainThread(() =>
            {
                isBusy = false;
            });
        }

        public Command GoToCalendar
        {
            get => goToCalendar;
        }
    }
}

