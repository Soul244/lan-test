using Entity;
using LTest.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Client
{

	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            MainPage = new IpPage();

        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
            //Listene?.Send(Encoding.UTF8.GetBytes("Disconnected"));
            //ClientListener.Stop();
		}

		protected override void OnResume ()
		{
        }
    }
}
