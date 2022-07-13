using System;
using System.Collections.Generic;
using NetSpace.Model;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class AdminCreateAccountView : ContentPage
    {
        public AdminCreateAccountView(User u)
        {
            InitializeComponent();
            BindingContext = new AdminCreateAccountViewModel(u);
        }
    }
}

