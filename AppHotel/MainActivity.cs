using Android.App;
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
            for (int i = 0; i < logins.Count; i++)
            {
                if(txtUser.Text.ToString().Trim() == "")
                    Toast.MakeText(Application.Context, "Preencha o usuário!", ToastLength.Long).Show();
                if(txtSenha.Text.ToString().Trim() == "")
                    Toast.MakeText(Application.Context, "Preencha a senha!", ToastLength.Long).Show();
                else if ((txtUser.Text == logins[i].User) && (txtSenha.Text == logins[i].Password))
                {
                    Toast.MakeText(Application.Context, "Login sucessfull!", ToastLength.Long).Show();
                    StartActivity(typeof(Menu));
                }
                else
                {
                    Toast.MakeText(Application.Context, "Invalid login!", ToastLength.Long).Show();
                    Limpar();
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void geraLogins(List<Login> logins)
        {
            logins.Add(new Login() { User = "Guilherme", Password = "14052001" });
            logins.Add(new Login() { User = "Maicon", Password = "25021997" });
            logins.Add(new Login() { User = "Camila", Password = "21102003" });
            logins.Add(new Login() { User = "Gustavo", Password = "21062002" });
            logins.Add(new Login() { User = "Cleusa", Password = "14111978" });
            logins.Add(new Login() { User = "Francisco", Password = "02021900" });
        }

        private void Limpar()
        {
            txtUser.Text = "";
            txtSenha.Text = "";
        }
    }
}