using System;
using System.Collections.Generic;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class AdminPlacesView : ContentPage
    {
        public AdminPlacesView()
        {
            InitializeComponent();
            BindingContext = new AdminPlacesViewModel();
        }
    }
}

