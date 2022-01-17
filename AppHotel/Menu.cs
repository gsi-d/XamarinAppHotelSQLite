using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using APPHotel;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppHotel
{
    [Activity(Label = "Menu")]
    public class Menu : Activity
    {
        Conexao con = new Conexao();
        Variaveis var = new Variaveis();

        double totalEntradas;
        double totalSaidas;
        double total;

        ImageView imgUsuario, imgMov, imgMovimentacoes, imgGastos, imgCheckIn, imgReservas;
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
            imgMovimentacoes = FindViewById<ImageView>(Resource.Id.imgMovimentacoes);
            imgGastos = FindViewById<ImageView>(Resource.Id.imgGastos);
            imgCheckIn = FindViewById<ImageView>(Resource.Id.imgCheckIn);
            imgReservas = FindViewById<ImageView>(Resource.Id.imgReservas);
            txtEntrada = FindViewById<TextView>(Resource.Id.txtEntrada);
            txtSaida = FindViewById<TextView>(Resource.Id.txtSaida);
            txtTotal = FindViewById<TextView>(Resource.Id.txtTotal);

            var.userLogado = Intent.GetStringExtra("nome");
            var.cargoUser = Intent.GetStringExtra("cargo");

            imgUsuario.SetImageResource(Resource.Drawable.usuarios);
            imgMov.SetImageResource(Resource.Drawable.Movimentacao);
            imgMovimentacoes.SetImageResource(Resource.Drawable.Movimentacao);
            imgGastos.SetImageResource(Resource.Drawable.Gastos);
            imgCheckIn.SetImageResource(Resource.Drawable.CheckIn);
            imgReservas.SetImageResource(Resource.Drawable.Reservas);

            txtUsuario.Text = "Usuário: " + var.userLogado;
            txtCargo.Text = "Cargo: " + var.cargoUser;

            imgMovimentacoes.Click += ImgMovimentacoes_Click;
            imgGastos.Click += ImgGastos_Click;
            imgCheckIn.Click += ImgCheckIn_Click;
            imgReservas.Click += ImgReservas_Click;

            TotalizarEntradas();
            TotalizarSaidas();
            Totalizar();
        }

        private void ImgReservas_Click(object sender, EventArgs e)
        {
            var tela = new Intent(this, typeof(Reservas));
            //tela.PutExtra("nome", var.userLogado);
            //tela.PutExtra("cargo", var.cargoUser);
            StartActivity(tela);
        }

        private void ImgCheckIn_Click(object sender, EventArgs e)
        {
            var tela = new Intent(this, typeof(CheckIn));
            //tela.PutExtra("nome", var.userLogado);
            //tela.PutExtra("cargo", var.cargoUser);
            StartActivity(tela);
        }

        private void ImgGastos_Click(object sender, EventArgs e)
        {
            var tela = new Intent(this, typeof(Gastos));
            tela.PutExtra("nome", var.userLogado);
            tela.PutExtra("cargo", var.cargoUser);
            StartActivity(tela);
        }

        private void ImgMovimentacoes_Click(object sender, EventArgs e)
        {
            var tela = new Intent(this, typeof(Movimentacoes));
            //tela.PutExtra("nome", var.userLogado);
            //tela.PutExtra("cargo", var.cargoUser);
            StartActivity(tela);
        }

        private void TotalizarEntradas()
        {
            MySqlCommand cmdVerificar;
            MySqlDataReader reader;

            con.AbrirCon();
            cmdVerificar = new MySqlCommand("SELECT id, sum(valor) as valor_total FROM movimentacoes where data = curDate() and tipo = @tipo", con.con);
            cmdVerificar.Parameters.AddWithValue("@tipo", "Entrada");

            reader = cmdVerificar.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //RECUPERAR OS DADOS DO USUÁRIO

                    if (reader["valor_total"].ToString() == "")
                    {
                        totalEntradas = 0;
                    }
                    else
                    {
                        totalEntradas = Convert.ToDouble(reader["valor_total"]);
                    }
                    txtEntrada.Text = "Entradas " + totalEntradas.ToString("C2");
                }
            }
            con.FecharCon();

        }
        private void TotalizarSaidas()
        {
            MySqlCommand cmdVerificar;
            MySqlDataReader reader;

            con.AbrirCon();
            cmdVerificar = new MySqlCommand("SELECT id, sum(valor) as valor_total FROM movimentacoes where data = curDate() and tipo = @tipo", con.con);
            cmdVerificar.Parameters.AddWithValue("@tipo", "Saída");

            reader = cmdVerificar.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //RECUPERAR OS DADOS DO USUÁRIO

                    if (reader["valor_total"].ToString() == "")
                    {
                        totalSaidas = 0;
                    }
                    else
                    {
                        totalSaidas = Convert.ToDouble(reader["valor_total"]);
                    }
                    txtSaida.Text = "Saídas " + totalSaidas.ToString("C2");


                }
            }
            con.FecharCon();
        }

        private void Totalizar()
        {
            total = totalEntradas - totalSaidas;
            txtTotal.Text = "Total " + total.ToString("C2");

            if (total < 0)
            {
                txtTotal.SetTextColor(Android.Graphics.Color.Red);
            }
            else
            {
                txtTotal.SetTextColor(Android.Graphics.Color.DarkGreen);
            }

        }
    }
}