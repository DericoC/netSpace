using System;
using System.Collections.Generic;
using System.Threading;
using NetSpace.Model;
using NetSpace.ViewModel;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class AdminPlacesView : ContentPage
    {
        int itemIndex = -1;
        Image rightImage;
        Image leftImage;

        public AdminPlacesView()
        {
            InitializeComponent();
            BindingContext = new AdminPlacesViewModel();
        }

        protected override void OnAppearing()
        {
            ((AdminPlacesViewModel)this.BindingContext).init();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.placeList.ResetSwipe();
        }

        private void ListView_SwipeStarted(object sender, Syncfusion.ListView.XForms.SwipeStartedEventArgs e)
        {
            itemIndex = -1;
        }

        private void ListView_Swiping(object sender, SwipingEventArgs e)
        {
            if (e.ItemIndex == 1 && e.SwipeOffSet > 70)
                e.Handled = true;
        }

        private void ListView_SwipeEnded(object sender, Syncfusion.ListView.XForms.SwipeEndedEventArgs e)
        {
            itemIndex = e.ItemIndex;
        }

        private void editIcon_BindingContextChanged(object sender, EventArgs e)
        {
            if (leftImage == null)
            {
                leftImage = sender as Image;
                (leftImage.Parent as Xamarin.Forms.View).GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(Update) });
            }
        }

        private void deleteIcon_BindingContextChanged(object sender, EventArgs e)
        {
            if (rightImage == null)
            {
                rightImage = sender as Image;
                (rightImage.Parent as Xamarin.Forms.View).GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(Delete) });
            }
        }

        private async void Update()
        {
            if (itemIndex >= 0)
            {
                List<Place> lista = new List<Place>(((AdminPlacesViewModel)(this.BindingContext)).places);
                await ((AdminPlacesViewModel)(this.BindingContext)).goToUpdatePlace(lista[itemIndex]);
                this.placeList.ResetSwipe();
            }                
        }

        private async void Delete()
        {
            if (itemIndex >= 0)
            {
                List<Place> lista = new List<Place>(((AdminPlacesViewModel)(this.BindingContext)).places);
                if (await ((AdminPlacesViewModel)(this.BindingContext)).deletePlaceAsync(lista[itemIndex]))
                {
                    ((AdminPlacesViewModel)(this.BindingContext)).places.RemoveAt(itemIndex);
                }
                
                this.placeList.ResetSwipe();
            }
        }
    }
}