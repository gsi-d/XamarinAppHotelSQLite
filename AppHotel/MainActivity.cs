using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AppHotel.Resources;
using APPHotel;
using MySqlConnector;
using System;
using System.Collections.Generic;

namespace AppHotel
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public List<Login> logins = new List<Login>();
        Variaveis var = new Variaveis();
        Conexao con = new Conexao();

        ImageView imgLogo, imgUser, imgSenha;
        EditText txtUser, txtSenha;
        Button btnLogin;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            
            imgLogo = FindViewById < ImageView > (Resource.Id.imgLogo);
            imgUser = FindViewById < ImageView > (Resource.Id.imgUser);
            imgSenha = FindViewById < ImageView > (Resource.Id.imgSenha);
            txtUser = FindViewById <EditText> (Resource.Id.txtUser);
            txtSenha = FindViewById <EditText> (Resource.Id.txtSenha);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

            imgLogo.SetImageResource(Resource.Drawable.logo);
            imgSenha.SetImageResource(Resource.Drawable.senha);
            imgUser.SetImageResource(Resource.Drawable.usuarios);  

            btnLogin.Click += BtnEntrar_Click;
        }

        private void BtnEntrar_Click(object sender, System.EventArgs e)
        {
            verificaLogin();
        }

        private void verificaLogin()
        {
            con.AbrirCon();

            if (txtUser.Text.ToString().Trim() == "")
            {
                Toast.MakeText(Application.Context, "Insira o usuário", ToastLength.Long).Show();
                txtUser.Text = "";
                txtUser.RequestFocus();
                return;
            }

            if (txtSenha.Text.ToString().Trim() == "")
            {
                Toast.MakeText(Application.Context, "Preencha a senha!", ToastLength.Long).Show();
                txtSenha.Text = "";
                txtSenha.RequestFocus();
                return;
            }
            //AQUI VAI O CÓDIGO PARA O LOGIN
            MySqlCommand cmdVerificar;
            MySqlDataReader reader;
            
            cmdVerificar = new MySqlCommand("SELECT * FROM usuarios where usuario = @usuario and senha = @senha", con.con);
            cmdVerificar.Parameters.AddWithValue("@usuario", txtUser.Text);
            cmdVerificar.Parameters.AddWithValue("@senha", txtSenha.Text);
            reader = cmdVerificar.ExecuteReader();

            if (reader.HasRows)
            {
                //EXTRAINDO INFORMAÇÕES DA CONSULTA DO LOGIN
                while (reader.Read())
                {
                    //RECUPERAR OS DADOS DO USUÁRIO
                    var.userLogado = reader["nome"].ToString();
                    var.cargoUser = reader["cargo"].ToString();


                }

                //Toast.MakeText(Application.Context, "Login Efetuado com Sucesso!", ToastLength.Long).Show();

                //INTENT PARA PASSAR PARAMETROS ENTRE ACT

                var tela = new Intent(this, typeof(Menu));
                tela.PutExtra("nome", var.userLogado);
                tela.PutExtra("cargo", var.cargoUser);
                StartActivity(tela);
                Limpar();

            }
            else
            {
                Toast.MakeText(Application.Context, "Dados Incorretos!", ToastLength.Long).Show();
                Limpar();
            }

            con.FecharCon();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void Limpar()
        {
            txtUser.Text = "";
            txtSenha.Text = "";
        }
    }
}