using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NetSpace.ViewModel;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.Util;
using NetSpace.View;
using Xamarin.Forms;
using System.Linq;

namespace NetSpace.ViewModel
{
	public class HomeViewModel : BaseViewModel
	{
		public Command searchCommand;
		public Command menuCommand;
		public Command menuOptionCommand;
		public bool searchBarVisibility { get; set; }
		public string search { get; set; }
		private Command placeDetailCommand;
		public ObservableCollection<Place> places { get; set; }
		public ObservableCollection<Place> placesOriginal { get; set; }
		public bool displayPopup { get; set; }
		PlaceService placeService = new PlaceService();

		public HomeViewModel()
		{
			searchBarVisibility = false;
			places = new ObservableCollection<Place>();
			foreach (var item in placeService.read()) {
				places.Add(item);
            }
			placesOriginal = places;
			displayPopup = false;
            menuCommand = new Command(showPopup);
            menuOptionCommand = new Command<string>(async (x) => await navigateMenuAsync(x));
			placeDetailCommand = new Command(async (p) => await loadSelectedPlaceAsync(p));
			searchCommand = new Command(searchShow);
		}

		public void isSearching(String search)
        {
			ObservableCollection<Place> newList = new ObservableCollection<Place>();

			if (search == "")
			{
				newList = placesOriginal;
			} else
            {
				foreach (var item in placesOriginal)
				{
					if (item.place_name.ToLower().Contains(search.ToLower()) || item.tags.Any(x =>  x.name.ToLower().Contains(search.ToLower())))
					{
						newList.Add(item);
					}
				}
			}

			places = newList;
		}

		private void searchShow()
        {
			searchBarVisibility = !searchBarVisibility;

		}

		private void showPopup()
        {
			displayPopup = true;
		}

		private async Task navigateMenuAsync(string text)
        {
			switch (text)
            {
				case "Cuenta":
					await Application.Current.MainPage.Navigation.PushAsync(new MyAccountView());
					break;
				case "Reservas":
					await Application.Current.MainPage.Navigation.PushAsync(new ReservationView());
					break;
				case "Salir":
					UserSession ses = UserSession.getSession();
					ses.reset();
					displayPopup = false;
					Application.Current.MainPage = new NavigationPage(new LoginView());
					break;
			}
		}

		async Task loadSelectedPlaceAsync(Object p)
		{
			await Application.Current.MainPage.Navigation.PushAsync(new PlaceView(p as Place, false));
		}

		public Command PlaceDetailCommand
		{
			get { return placeDetailCommand; }
			protected set { placeDetailCommand = value; }
		}

		public Command SearchCommand
		{
			get => searchCommand;
		}

		public Command MenuCommand
		{
			get => menuCommand;
		}

		public Command MenuOptionCommand
        {
			get => menuOptionCommand;

		}
	}
}

