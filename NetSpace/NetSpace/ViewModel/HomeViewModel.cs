using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NetSpace.ViewModel;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.Util;
using NetSpace.View;
using Xamarin.Forms;

namespace NetSpace.ViewModel
{
	public class HomeViewModel : BaseViewModel
	{
		public Command searchCommand;
		public Command menuCommand;
		public Command menuOptionCommand;
		private Command placeDetailCommand;
		public ObservableCollection<Place> places { get; set; }
		public ObservableCollection<PopUpMenuItem> popUpMenuItems { get; set; }
		public bool displayPopup { get; set; }
		PlaceService placeService = new PlaceService();

		public HomeViewModel()
		{
			popUpMenuItems = new ObservableCollection<PopUpMenuItem>();
			places = new ObservableCollection<Place>();
			foreach (var item in placeService.read()) {
				places.Add(item);
            }
			popUpMenuItems.Add(new PopUpMenuItem("\uf007", "Cuenta"));
			popUpMenuItems.Add(new PopUpMenuItem("\uf1ad", "Reservas"));
			popUpMenuItems.Add(new PopUpMenuItem("\uf2f5", "Salir"));
			displayPopup = false;
            menuCommand = new Command(showPopup);
            menuOptionCommand = new Command<string>(async (x) => await navigateMenuAsync(x));
			placeDetailCommand = new Command(async (p) => await loadSelectedPlaceAsync(p));
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
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
					break;
			}
		}

		async Task loadSelectedPlaceAsync(Object p)
		{
			await Application.Current.MainPage.Navigation.PushAsync(new PlaceView(p as Place));
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

