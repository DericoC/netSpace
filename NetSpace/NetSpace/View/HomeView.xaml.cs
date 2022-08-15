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
            if (Device.RuntimePlatform == Device.iOS)
            {
                Thickness margin = searchIcon.Margin;
                margin.Top = 15;
                searchIcon.Margin = margin;
            } else
            {
                Thickness margin = searchIcon.Margin;
                margin.Top = 20;
                searchIcon.Margin = margin;
            }
        }

        void txtSearch_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
			((HomeViewModel)(this.BindingContext)).isSearching(txtSearch.Text);
		}
    }
}

