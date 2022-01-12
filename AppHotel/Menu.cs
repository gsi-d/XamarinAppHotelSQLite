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
        Variaveis var = new Variaveis();

        ImageView imgUsuario, imgMov;
        TextView txtUsuario, txtCargo, txtEntrada, txtSaida, txtTotal;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Menu);
            imgUsuario = FindViewById<ImageView>(Resource.Id.imgUser);
            txtUsuario = FindViewById<TextView>(Resource.Id.txtUsuario);
            txtCargo = FindViewById<TextView>(Resource.Id.txtCargo);
            imgMov = FindViewById<ImageView>(Resource.Id.imgMov);
            txtEntrada = FindViewById<TextView>(Resource.Id.txtEntrada);
            txtSaida = FindViewById<TextView>(Resource.Id.txtSaida);
            txtTotal = FindViewById<TextView>(Resource.Id.txtTotal);

            var.userLogado = Intent.GetStringExtra("user");
            var.cargoUser = Intent.GetStringExtra("cargo");

            imgUsuario.SetImageResource(Resource.Drawable.usuarios);
            imgMov.SetImageResource(Resource.Drawable.Movimentacao);
            txtUsuario.Text = "Usuário: " + var.userLogado;
            txtCargo.Text = "Cargo: " + var.cargoUser;

            TotalizarEntradas();
            TotalizarSaidas();
            Totalizar();
        }

        private void TotalizarEntradas()
        {

        }

        private void TotalizarSaidas()
        {

        }

        private void Totalizar()
        {

        }
    }
}