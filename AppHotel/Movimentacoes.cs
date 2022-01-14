using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppHotel;
using MySqlConnector;

namespace APPHotel
{
    [Activity(Label = "Movimentacoes")]
    public class Movimentacoes : Activity
    {

        Conexao con = new Conexao();
        Variaveis var = new Variaveis();

        double totalEntradas;
        double totalSaidas;
        double total;

        CalendarView data;
        ListView lista;
        TextView txtEntrada, txtSaida, txtTotal;

        string dataRecuperada;
        List<string> listaMov = new List<string>();
        ArrayAdapter<string> adapter;
        ArrayAdapter<string> adapterSemDados;

        /*protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Movimentacoes);

            txtEntrada = FindViewById<TextView>(Resource.Id.txtEntrada);
            txtSaida = FindViewById<TextView>(Resource.Id.txtSaida);
            txtTotal = FindViewById<TextView>(Resource.Id.txtTotal);

            data = FindViewById<CalendarView>(Resource.Id.data);
            lista = FindViewById<ListView>(Resource.Id.lista);

            data.DateChange += Data_DateChange;
            dataRecuperada = DateTime.Today.ToString();
            BuscarData();
        }

        private void Data_DateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            dataRecuperada = e.DayOfMonth + "/" + (e.Month + 1) + "/" + e.Year;
            // Toast.MakeText(Application.Context, dataRecuperada, ToastLength.Long).Show();
            BuscarData();
        }

        private void BuscarData()
        {
            string sql;
            MySqlCommand cmd;
            MySqlDataReader reader;
            con.AbrirCon();


            sql = "SELECT * FROM movimentacoes where data = @data";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@data", Convert.ToDateTime(dataRecuperada));
            reader = cmd.ExecuteReader();


            lista.Adapter = adapterSemDados;
            if (reader.HasRows)
            {
                listaMov.Clear();


                while (reader.Read())
                {



                    listaMov.Add(reader["tipo"].ToString() + "   |   " + reader["movimento"].ToString() + "   |   " + reader["valor"].ToString());
                    adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listaMov);
                    lista.Adapter = adapter;
                }
            }


            con.FecharCon();

            TotalizarEntradas();
            TotalizarSaidas();
            Totalizar();
        }


        private void TotalizarEntradas()
        {
            MySqlCommand cmdVerificar;
            MySqlDataReader reader;

            con.AbrirCon();
            cmdVerificar = new MySqlCommand("SELECT id, sum(valor) as valor_total FROM movimentacoes where data = @data and tipo = @tipo", con.con);
            cmdVerificar.Parameters.AddWithValue("@tipo", "Entrada");
            cmdVerificar.Parameters.AddWithValue("@data", Convert.ToDateTime(dataRecuperada));
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
            cmdVerificar = new MySqlCommand("SELECT id, sum(valor) as valor_total FROM movimentacoes where data = @data and tipo = @tipo", con.con);
            cmdVerificar.Parameters.AddWithValue("@tipo", "Saída");
            cmdVerificar.Parameters.AddWithValue("@data", Convert.ToDateTime(dataRecuperada));
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

        }*/




    }
}