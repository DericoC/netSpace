using System;
using System.Collections.Generic;
using NetSpace.Model;
using NetSpace.Util;
using NetSpace.ViewModel;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class AdminAccountsView : ContentPage
    {
        private readonly SnackBarAlert alert = new SnackBarAlert();
        int itemIndex = -1;
        Image rightImage;
        Image leftImage;

        public AdminAccountsView()
        {
            InitializeComponent();
            BindingContext = new AdminAccountsViewModel();
        }

        protected override void OnAppearing()
        {
            ((AdminAccountsViewModel)this.BindingContext).init();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.usersList.ResetSwipe();
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
                List<User> lista = new List<User>(((AdminAccountsViewModel)(this.BindingContext)).users);
                if (lista[itemIndex].role != "Cliente")
                {
                    await ((AdminAccountsViewModel)(this.BindingContext)).goToModifyAccount(lista[itemIndex]);
                    this.usersList.ResetSwipe();
                } else
                {
                    alert.displaySnackBarAlertAsync("No se puede editar la cuenta de un cliente", 5, SnackBarAlert.WARNING);
                    this.usersList.ResetSwipe();
                }
            }
        }

        private async void Delete()
        {
            if (itemIndex >= 0)
            {
                List<User> lista = new List<User>(((AdminAccountsViewModel)(this.BindingContext)).users);
                if (await ((AdminAccountsViewModel)(this.BindingContext)).deletePlaceAsync(lista[itemIndex]))
                {
                    ((AdminPlacesViewModel)(this.BindingContext)).places.RemoveAt(itemIndex);
                }

                this.usersList.ResetSwipe();
            }
        }
    }
}

