using System;
using NetSpace.ViewModel;

namespace NetSpace.Model
{
    public class User : BaseViewModel
    {
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public string mail { get; set; }
        public string image { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public int provider { get; set; }

        public User()
        {
        }

        public User(int user_id, string first_name, string last_name, string gender, string mail, string image, string phone, string address, string password, string role, int provider)
        {
            this.user_id = user_id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.gender = gender;
            this.mail = mail;
            this.image = image;
            this.phone = phone;
            this.address = address;
            this.password = password;
            this.role = role;
            this.provider = provider;
        }
    }
}

