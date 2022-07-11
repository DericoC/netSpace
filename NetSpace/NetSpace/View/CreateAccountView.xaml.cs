using System;
using System.Collections.Generic;
using NetSpace.ViewModel;

using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class CreateAccountView : ContentPage
    {
        public CreateAccountView()
        {
            InitializeComponent();
            BindingContext = new CreateAccountViewModel();
        }
    }
}

