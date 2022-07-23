using System;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.Model
{
    public class Slots : BaseViewModel
    {
        public string subject_name { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public Color color { get; set; }
        public bool is_all_day { get; set; }

        public Slots()
        {
        }

        public Slots(string subject_name, DateTime start_time, DateTime end_time, Color color, bool is_all_day)
        {
            this.subject_name = subject_name;
            this.start_time = start_time;
            this.end_time = end_time;
            this.color = color;
            this.is_all_day = is_all_day;
        }
    }
}

