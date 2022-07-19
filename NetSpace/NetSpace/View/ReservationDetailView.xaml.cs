using System;
using System.Collections.Generic;
using NetSpace.Model;
using NetSpace.ViewModel;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace NetSpace.View
{
    public partial class ReservationDetailView : ContentPage
    {
        ZXingBarcodeImageView qr;
        public ReservationDetailView(Reservation r)
        {
            InitializeComponent();
            BindingContext = new ReservationDetailViewModel(r);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            qr = new ZXingBarcodeImageView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            qr.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            qr.BarcodeOptions.Width = 500;
            qr.BarcodeOptions.Height = 500;
            qr.BarcodeValue = lblName.Text;
            Generando.Children.Add(qr);
        }
    }
}

