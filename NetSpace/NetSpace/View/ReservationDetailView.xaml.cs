using System;
using System.Collections.Generic;
using NetSpace.Model;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class ReservationDetailView : ContentPage
    {
        public ReservationDetailView(Reservation r)
        {
            InitializeComponent();
            BindingContext = new ReservationDetailViewModel(r);
        }
    }
}

