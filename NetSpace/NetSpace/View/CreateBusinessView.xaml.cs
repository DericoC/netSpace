using System;
using System.Collections.Generic;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class CreateBusinessView : ContentPage
    {
        public CreateBusinessView()
        {
            InitializeComponent();
            BindingContext = new CreateBusinessViewModel();
        }
    }
}

