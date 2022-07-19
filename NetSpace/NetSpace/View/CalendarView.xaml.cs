using System;
using System.Collections.Generic;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class CalendarView : ContentPage
    {
        public CalendarView()
        {
            InitializeComponent();
            BindingContext = new CalendarViewModel();
        }
    }
}

