using System;
using System.Collections.Generic;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            txtMail.Text = null;
            txtPassword.Text = null;
        }
    }
}

