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
using MySql.Data.MySqlClient;

namespace APPHotel
{
    class Conexao
    {

        //CONEXAO COM O BANCO DE DADOS HOSPEDADO
        public string conec = "SERVER=mysql746.umbler.com; DATABASE=sistemahotelerio; UID=rensid; PWD=guilherme14; PORT=41890;";

        public MySqlConnection con = null;

        public void AbrirCon()
        {
            try
            {
                con = new MySqlConnection(conec);
                con.Open();
                //Toast.MakeText(Application.Context, "Conectado", ToastLength.Long).Show();
            }
            catch (Exception ex)
            {

                Toast.MakeText(Application.Context, "Erro ao conectar" + ex, ToastLength.Long).Show();
            }


        }


        public void FecharCon()
        {
            try
            {
                con = new MySqlConnection(conec);
                con.Close();


            }
            catch (Exception ex)
            {

                Toast.MakeText(Application.Context, "Erro ao conectar" + ex, ToastLength.Long).Show();
            }
        }



    }
}