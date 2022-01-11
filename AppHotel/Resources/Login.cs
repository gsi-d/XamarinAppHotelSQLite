using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppHotel.Resources
{
    public class Login
    {
        public Login(string user, string password, string cargo)
        {
            User = user;
            Password = password;
            Cargo = cargo;
        }

        public Login()
        {

        }

        public string User { get; set; }
        public string Password { get; set; }
        public string Cargo { get; set; }

        
    }   
}