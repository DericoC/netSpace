using System;
using System.Collections.Generic;
using NetSpace.Model;
using NetSpace.ViewModel;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class AdminPolicyView : ContentPage
    {
        int itemIndex = -1;
        Image rightImage;
        Image leftImage;

        public AdminPolicyView()
        {
            InitializeComponent();
            BindingContext = new AdminPolicyViewModel();
        }

        protected override void OnAppearing()
        {
            ((AdminPolicyViewModel)this.BindingContext).init();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.policyList.ResetSwipe();
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

        private void Update()
        {
            if (itemIndex >= 0)
            {
                List<Policy> lista = new List<Policy>(((AdminPolicyViewModel)(this.BindingContext)).policies);
                Navigation.PushAsync(new AdminCreatePolicyView(lista[itemIndex]));
                this.policyList.ResetSwipe();
            }
        }

        private async void Delete()
        {
            if (itemIndex >= 0)
            {
                List<Policy> lista = new List<Policy>(((AdminPolicyViewModel)(this.BindingContext)).policies);
                if (await ((AdminPolicyViewModel)(this.BindingContext)).deletePolicyAsync(lista[itemIndex]))
                {
                    ((AdminPolicyViewModel)(this.BindingContext)).policies.RemoveAt(itemIndex);
                }

                this.policyList.ResetSwipe();
            }
        }
    }
}

