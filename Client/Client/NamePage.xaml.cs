using System;
using Xamarin.Forms;
using Entity;
using LTest.Classes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    public partial class NamePage : ContentPage
	{
        byte[] receivedBuf = new Byte[1024*1024*50];
        byte[] buffer = new Byte[1024];
        public static ClientKullanici Kullanici;
        public NamePage()
		{
			InitializeComponent();
            ClientListener.ObjectArrived += TestBaslat;
        }
        ~NamePage()
        {
            ClientListener.ObjectArrived -= TestBaslat;

        }
        private void SendName_Clicked(object sender, EventArgs e)
        {
            ClientListener.Kullanici= new ClientKullanici
            {
                KullaniciAdi = Name.Text,
                Sorular=new List<ClientKullanici.SoruOzellikleri>()
            };
            
            ClientListener.SendObject();


            //Bekle();
        }

        private void TestBaslat()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Navigation.PushModalAsync(new BaslangicSure());
            });
        }


        public static object GetObject(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(data);
            return formatter.Deserialize(ms);
        }
        public static void SendObject(object obj)
        {
            try
            {
                BinaryFormatter _formatter = new BinaryFormatter();
                MemoryStream _memoryStream = new MemoryStream();
                _formatter.Serialize(_memoryStream, obj);
                byte[] buffer = _memoryStream.ToArray();
                IpPage.Socket.Send(buffer);
            }
            catch (Exception)
            {

            }
        }
        //void Bekle()
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            var buf = new byte[8192];
        //            var recData = IpPage.Socket.Receive(buf);

        //            obj = GetObject(buf);

        //            if (obj.GetType() == typeof(Test))
        //            {
        //                Test = (Test)obj;
        //            }
        //            else if (obj.GetType() == typeof(Sure))
        //            {
        //                Sure = (Sure)obj;
        //            }
        //            else if (obj.GetType()==typeof(Soru))
        //            {
        //                Sorular = (List<Soru>)obj;
        //            }
        //            else if (obj.GetType() == typeof(Cevap))
        //            {
        //                Cevaplar = (List<Cevap>)obj;
        //            }
        //            if (Test!=null && Sure!=null && Sorular!=null && Cevaplar!=null)
        //            {
        //                buffer = Encoding.UTF8.GetBytes("Nesneler Alındı");
        //                IpPage.Socket.Send(buffer);
        //                break;
        //            }
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }
        //    Navigation.PushModalAsync(new BaslangicSure(Sure));
        //}
    }
}
