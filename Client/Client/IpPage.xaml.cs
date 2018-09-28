using System;
using System.Net.Sockets;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LTest.Classes;
using System.Threading;

namespace Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IpPage : ContentPage
	{
        public static Socket Socket;

        public IpPage ()
		{
			InitializeComponent ();
        }
        private void Giris_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Ip.Text))
            {

                //Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientListener.Ip = Ip.Text;
                var result=ClientListener.Start();
                if (result)
                {
                    Navigation.PushModalAsync(new NamePage());
                }
                else
                {
                    Info.Text = "Ağa bağlanılamadı";
                }
                //try
                //{
                //    var result = Socket.BeginConnect(ip, 100, null, null);
                //    bool success = result.AsyncWaitHandle.WaitOne(500);
                //    if (success)
                //    {
                //        Socket.EndConnect(result);
                //        Navigation.PushModalAsync(new NamePage());
                //    }
                //    else
                //    {
                //        throw new SocketException(10060);
                //    }
                //}
                //catch (Exception)
                //{
                //    Socket.Close();
                //    Info.Text = "Ağa bağlanılamadı";
                //}
            }
        }
    }
}