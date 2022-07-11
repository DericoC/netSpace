using System;
using NetSpace.ViewModel;

namespace NetSpace.Model
{
    public class PopUpMenuItem : BaseViewModel
    {
        public string icon { get; set; }
        public string name { get; set; }

        public PopUpMenuItem()
        {
        }

        public PopUpMenuItem(string icon, string name)
        {
            this.icon = icon;
            this.name = name;
        }
    }
}

