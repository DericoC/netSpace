﻿using System;
using System.Collections.Generic;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class ReservationView : ContentPage
    {
        public ReservationView()
        {
            InitializeComponent();
            BindingContext = new ReservationViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((ReservationViewModel)this.BindingContext).init();
        }
    }
}

