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

        void txtSearch_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
			((HomeViewModel)(this.BindingContext)).isSearching(txtSearch.Text);
		}
    }
}

