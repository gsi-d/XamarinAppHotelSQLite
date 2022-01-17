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
    [Activity(Label = "Gastos")]
    public class Gastos : Activity
    {
        Conexao con = new Conexao();
        Variaveis var = new Variaveis();

        EditText txtGasto, txtValor;
        ListView lista;
        Button btnSalvar;

        string ultimoIdGasto;

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
            string sql;
            MySqlCommand cmd;
            MySqlDataReader reader;
            con.AbrirCon();


            sql = "SELECT * FROM gastos where data = curDate()";
            cmd = new MySqlCommand(sql, con.con);
            //cmd.Parameters.AddWithValue("@data", Convert.ToDateTime(dataRecuperada));
            reader = cmd.ExecuteReader();



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


            con.FecharCon();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            string sql;
            MySqlCommand cmd;

            con.AbrirCon();


            sql = "INSERT INTO gastos (descricao, valor, funcionario, data) VALUES (@descricao, @valor, @funcionario, curDate())";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@descricao", txtGasto.Text);
            cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(txtValor.Text));
            cmd.Parameters.AddWithValue("@funcionario", var.userLogado);
            cmd.ExecuteNonQuery();


            //RECUPERAR O ULTIMO ID DO GASTO
            MySqlCommand cmdVerificar;
            MySqlDataReader reader;
            con.AbrirCon();
            cmdVerificar = new MySqlCommand("SELECT id FROM gastos order by id desc LIMIT 1", con.con);

            reader = cmdVerificar.ExecuteReader();

            if (reader.HasRows)
            {
                //EXTRAINDO INFORMAÇÕES DA CONSULTA DO LOGIN
                while (reader.Read())
                {
                    ultimoIdGasto = Convert.ToString(reader["id"]);


                }
            }




            //LANÇAR O GASTO NAS MOVIMENTAÇÕES
            con.AbrirCon();
            sql = "INSERT INTO movimentacoes (tipo, movimento, valor, funcionario, data, id_movimento) VALUES (@tipo, @movimento, @valor, @funcionario, curDate(), @id_movimento)";
            cmd = new MySqlCommand(sql, con.con);

            cmd.Parameters.AddWithValue("@tipo", "Saída");
            cmd.Parameters.AddWithValue("@movimento", "Gasto");
            cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(txtValor.Text));
            cmd.Parameters.AddWithValue("@funcionario", var.userLogado);
            cmd.Parameters.AddWithValue("@id_movimento", ultimoIdGasto);


            cmd.ExecuteNonQuery();
            con.FecharCon();



            Listar();
            Toast.MakeText(Application.Context, "Salvo com Sucesso!!", ToastLength.Long).Show();
            txtGasto.Text = "";
            txtValor.Text = "";
            txtGasto.RequestFocus();
        }
    }
}