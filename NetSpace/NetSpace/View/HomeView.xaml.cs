using System;
using System.Collections.Generic;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
	public partial class HomeView : ContentPage
	{
		public HomeView()
		{
			InitializeComponent();
			BindingContext = new HomeViewModel();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			BindingContext = new HomeViewModel();
		}
	}
}

