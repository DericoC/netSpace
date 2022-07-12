using System;
using System.Collections.Generic;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class AdminHomeView : ContentPage
    {
        public AdminHomeView()
        {            
            InitializeComponent();
            BindingContext = new AdminHomeViewModel();
        }
    }
}

