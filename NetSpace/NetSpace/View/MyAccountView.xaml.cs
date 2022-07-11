using System;
using System.Collections.Generic;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class MyAccountView : ContentPage
    {
        public MyAccountView()
        {
            InitializeComponent();
            BindingContext = new MyAccountViewModel();
        }
    }
}

