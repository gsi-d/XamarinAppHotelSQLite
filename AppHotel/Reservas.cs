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
    [Activity(Label = "Reservas")]
    public class Reservas : Activity
    {
        TextView txtNome, txtTelefone;
        CalendarView calendar1, calendar2;
        ListView listQuartos;
        string dataEntrada, dataSaida;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Reservas);

            txtNome = FindViewById<TextView>(Resource.Id.txtNome);
            txtTelefone = FindViewById<TextView>(Resource.Id.txtTelefone);
            listQuartos = FindViewById<ListView>(Resource.Id.listaQuartos);
            calendar1 = FindViewById<CalendarView>(Resource.Id.calendar1);
            calendar2 = FindViewById<CalendarView>(Resource.Id.calendar2);

            calendar1.DateChange += Calendar1_DateChange;
            calendar2.DateChange += Calendar2_DateChange;

            dataEntrada = DateTime.Today.ToString();
            dataSaida = DateTime.Today.ToString();
        }

        private void Calendar2_DateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            dataEntrada = e.DayOfMonth + "/" + (e.Month + 1) + "/" + e.Year;
        }

        private void Calendar1_DateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            dataSaida = e.DayOfMonth + "/" + (e.Month + 1) + "/" + e.Year;
        }
    }
}