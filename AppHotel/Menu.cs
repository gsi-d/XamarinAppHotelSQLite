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

namespace AppHotel
{
    [Activity(Label = "Menu")]
    public class Menu : Activity
    {
        MainActivity main = new MainActivity();

        ImageView imgUsuario;
        TextView txtUsuario, txtCargo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Menu);
            imgUsuario = FindViewById<ImageView>(Resource.Id.imgUser);
            txtUsuario = FindViewById<TextView>(Resource.Id.txtUser);
            txtCargo = FindViewById<TextView>(Resource.Id.txtUser);

            imgUsuario.SetImageResource(Resource.Drawable.usuarios);
            txtUsuario.Text = "Usuário: " + Intent.GetStringExtra("user");
            txtCargo.Text = "Cargo: " + Intent.GetStringExtra("cargo");
        }
    }
}