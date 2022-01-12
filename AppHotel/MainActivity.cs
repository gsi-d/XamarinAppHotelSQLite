using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AppHotel.Resources;
using System;
using System.Collections.Generic;

namespace AppHotel
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public List<Login> logins = new List<Login>();
        Variaveis var = new Variaveis();

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
            geraLogins(logins);
            

            btnLogin.Click += BtnEntrar_Click;
        }

        private void BtnEntrar_Click(object sender, System.EventArgs e)
        {
            verificaLogin();
        }

        private void verificaLogin()
        {
            int contador = 0;
            for (int i = 0; i < logins.Count; i++)
            {
                /*if(txtUser.Text.ToString().Trim() == "")
                {
                    Toast.MakeText(Application.Context, "Preencha o usuário!", ToastLength.Long).Show();
                    txtUser.RequestFocus();
                }      
                if(txtSenha.Text.ToString().Trim() == "")
                {
                    Toast.MakeText(Application.Context, "Preencha a senha!", ToastLength.Long).Show();
                    txtSenha.RequestFocus();
                }*/
                if ((txtUser.Text == logins[i].User) && (txtSenha.Text == logins[i].Password))
                {
                    contador += 1;
                    Toast.MakeText(Application.Context, "Login sucessfull!", ToastLength.Long).Show();

                    string user = logins[i].User.ToString();
                    string cargo = logins[i].Cargo.ToString();

                    var.userLogado = user;
                    var.cargoUser = cargo;

                    var tela = new Intent(this, typeof(Menu));

                    tela.PutExtra("user", var.userLogado);
                    tela.PutExtra("cargo", var.cargoUser);

                    StartActivity(tela);
                    Limpar();
                }  
            }
            if (contador == 0)
            {
                Toast.MakeText(Application.Context, "Invalid login!", ToastLength.Long).Show();
                Limpar();
                txtUser.RequestFocus();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void geraLogins(List<Login> logins)
        {
            logins.Add(new Login() { User = "Guilherme", Password = "14052001", Cargo = "Estagiário" });
            logins.Add(new Login() { User = "Maicon", Password = "25021997", Cargo = "Motoboy" });
            logins.Add(new Login() { User = "Camila", Password = "21102003", Cargo = "Relações Internacionais" });
            logins.Add(new Login() { User = "Gustavo", Password = "21062002", Cargo = "Motoboy" });
            logins.Add(new Login() { User = "Cleusa", Password = "14111978", Cargo = "Costureira" });
            logins.Add(new Login() { User = "Francisco", Password = "02021900", Cargo = "Aposentado" });
        }

        private void Limpar()
        {
            txtUser.Text = "";
            txtSenha.Text = "";
        }
    }
}