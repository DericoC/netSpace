﻿using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class AdminReservationsView : ContentPage
    {
        public AdminReservationsView()
        {
            InitializeComponent();
            BindingContext = new AdminReservationViewModel();
        }

        protected override void OnAppearing()
        {
            ((AdminReservationViewModel)this.BindingContext).init();
            base.OnAppearing();
        }
    }
}

