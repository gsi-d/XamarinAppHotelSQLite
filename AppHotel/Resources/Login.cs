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
        public Login(string user, string password)
        {
            User = user;
            Password = password;
        }

        public Login()
        {

        }

        public string User { get; set; }
        public string Password { get; set; }

        
    }   
}