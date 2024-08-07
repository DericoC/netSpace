﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NetSpace.ViewModel;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.View;
using Xamarin.Forms;
using NetSpace.Util;

namespace NetSpace.ViewModel
{
    public class ReservationViewModel : BaseViewModel
    {
        public Command searchCommand;
        public Command menuCommand;
        public Command menuOptionCommand;
        private Command reservationDetailCommand;
        private UserSession ses = UserSession.getSession();
        public ObservableCollection<Reservation> reservation { get; set; }
        ReservationService reservationService = new ReservationService();

        public ReservationViewModel()
        {
            reservation = new ObservableCollection<Reservation>();
            reservationDetailCommand = new Command<Reservation>(async (r) => await loadSelectedReservationAsync(r));
            this.init();
        }

        public void init()
        {
            reservation.Clear();
            foreach (var item in reservationService.readClient(ses.getUser().user_id))
            {
                reservation.Add(item);
            }
        }

        async Task loadSelectedReservationAsync(Reservation r)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ReservationDetailView(r));
        }

        public Command ReservationDetailCommand
        {
            get { return reservationDetailCommand; }
            protected set { reservationDetailCommand = value; }
        }
    }
}