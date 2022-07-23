using System;
using System.Collections.Generic;
using NetSpace.Service;
using Syncfusion.SfCalendar.XForms;
using NetSpace.Util;
using System.Threading;

namespace NetSpace.ViewModel
{
    public class CalendarViewModel : BaseViewModel
    {
        UserSession user = new UserSession();
        public CalendarEventCollection CalendarInlineEvents { get; set; } = new CalendarEventCollection();
        PlaceSlotConfigService slotConfigService = new PlaceSlotConfigService();

        public CalendarViewModel(int placeId)
        {
            user = UserSession.getSession();
            this.loadEvents(placeId);
        }

        private void loadEvents(int placeId)
        {
            List<CalendarInlineEvent> slots = slotConfigService.generatePlaceSlots(placeId, user.getUser().user_id);

            foreach (CalendarInlineEvent slot in slots)
            {
                CalendarInlineEvents.Add(slot);
            }
        }
    }
}

