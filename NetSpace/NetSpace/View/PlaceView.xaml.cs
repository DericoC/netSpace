using System;
using System.Collections.Generic;
using NetSpace.Model;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class PlaceView : ContentPage
    {
        public PlaceView(Place p)
        {
            InitializeComponent();
            BindingContext = new PlaceViewModel(p);
        }
    }
}

