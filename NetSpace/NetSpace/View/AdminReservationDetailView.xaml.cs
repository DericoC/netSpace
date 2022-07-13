using System;
using System.Collections.Generic;
using NetSpace.Model;
using NetSpace.ViewModel;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace NetSpace.View
{ 
    public partial class AdminReservationDetailView : ContentPage
    {
    ZXingBarcodeImageView qr;
    public AdminReservationDetailView(Reservation r)
    {
        InitializeComponent();
        BindingContext = new AdminReservationDetailViewModel(r);
    }

    private async void btnGenerarQR_Clicked(object sender, EventArgs e)
    {
        qr = new ZXingBarcodeImageView
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand
        };
        qr.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
        qr.BarcodeOptions.Width = 150;
        qr.BarcodeOptions.Width = 150;
        qr.BarcodeValue = "https//:google.com";
        Generador.Children.Add(qr);
    }

  }
}