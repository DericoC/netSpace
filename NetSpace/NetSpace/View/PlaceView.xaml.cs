using System;
using System.Collections.Generic;
using NetSpace.Model;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class PlaceView : ContentPage
    {
        public PlaceView(Place p, bool isAdmin)
        {
            InitializeComponent();
            BindingContext = new PlaceViewModel(p, isAdmin);
        }
    }
}

