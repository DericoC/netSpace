using System;
using System.Collections.Generic;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class AdminAccountsView : ContentPage
    {
        public AdminAccountsView()
        {
            InitializeComponent();
            BindingContext = new AdminAccountsViewModel();
        }
    }
}

