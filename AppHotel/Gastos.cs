using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppHotel;
using Mono.Data.Sqlite;
using MySqlConnector;

namespace APPHotel
{
    [Activity(Label = "Gastos")]
    public class Gastos : Activity
    {
        //Conexao con = new Conexao();
        Variaveis var = new Variaveis();

        string ultimoIdGasto, base_dados;   
       
        EditText txtGasto, txtValor;
        ListView lista;
        Button btnSalvar;

        List<string> listaGasto = new List<string>();
        ArrayAdapter<string> adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Gastos);
            var.userLogado = Intent.GetStringExtra("nome");
           
            txtGasto = FindViewById<EditText>(Resource.Id.txtGasto);
            txtValor = FindViewById<EditText>(Resource.Id.txtValor);

            lista = FindViewById<ListView>(Resource.Id.lista);
            btnSalvar = FindViewById<Button>(Resource.Id.btnSalvar);

            btnSalvar.Click += BtnSalvar_Click;
            Listar();
        }

        private void Listar()
        {
            base_dados = Intent.GetStringExtra("conexao");
            SqliteConnection con = new SqliteConnection("Data Source = " + base_dados);
            con.Open();

            SqliteCommand command = new SqliteCommand(con);
            SqliteDataReader reader;

            command.CommandText = "SELECT * FROM gastos where data = date('now')";
            command.ExecuteNonQuery();
            //cmd.Parameters.AddWithValue("@data", Convert.ToDateTime(dataRecuperada));
            reader = command.ExecuteReader();



            if (reader.HasRows)
            {
                listaGasto.Clear();
                while (reader.Read())
                {
                    listaGasto.Add(reader["descricao"].ToString() + "   |   " + reader["valor"].ToString());
                    adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listaGasto);
                    lista.Adapter = adapter;
                }
            }
            con.Close();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            base_dados = Intent.GetStringExtra("conexao");
            SqliteConnection con = new SqliteConnection("Data Source = " + base_dados + "; Version = 3");
            SqliteCommand command = new SqliteCommand(con);

            con.Open();

            command.CommandText = "INSERT INTO gastos (descricao, valor, funcionario, data) VALUES (@descricao, @valor, @funcionario, date('now'))";
            
            command.Parameters.AddWithValue("@descricao", txtGasto.Text);
            command.Parameters.AddWithValue("@valor", Convert.ToDouble(txtValor.Text));
            command.Parameters.AddWithValue("@funcionario", var.userLogado);
            command.ExecuteNonQuery();


            //RECUPERAR O ULTIMO ID DO GASTO
            SqliteDataReader reader; 
            command.CommandText = "SELECT id FROM gastos order by id desc LIMIT 1";

            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                //EXTRAINDO INFORMAÇÕES DA CONSULTA DO LOGIN
                while (reader.Read())
                {
                    ultimoIdGasto = Convert.ToString(reader["id"]);
                }
            }
            reader.Close();
            //LANÇAR O GASTO NAS MOVIMENTAÇÕES
            command.CommandText = "INSERT INTO movimentacoes (tipo, movimento, valor, funcionario, data, id_movimento) VALUES (@tipo, @movimento, @valor, @funcionario, date('now'), @id_movimento)";

            command.Parameters.AddWithValue("@tipo", "Saída");
            command.Parameters.AddWithValue("@movimento", "Gasto");
            command.Parameters.AddWithValue("@valor", Convert.ToDouble(txtValor.Text));
            command.Parameters.AddWithValue("@funcionario", var.userLogado);
            command.Parameters.AddWithValue("@id_movimento", ultimoIdGasto);

            command.ExecuteNonQuery();
            con.Close();

            Listar();
            Toast.MakeText(Application.Context, "Salvo com Sucesso!!", ToastLength.Long).Show();
            txtGasto.Text = "";
            txtValor.Text = "";
            txtGasto.RequestFocus();
        }
    }
}