using Entity;
using LTest.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Client
{
	public class SoruSonuc : ContentPage
	{
        public Label Puan = new Label
        {
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.Center,
            FontSize = 44
        };

        public Label Icon = new Label
        {
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.Center,
            FontSize = 144
        };
        public Label Sonuc = new Label
        {
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.Center,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
        };

        public StackLayout StackLayout = new StackLayout();
        public SoruSonuc(int sonuc, float puan)
		{
            ClientListener.MessageArrived += ButonaBasildi;

            switch (sonuc)
            {
                case 1:
                    Icon.Text = "✓";
                    Sonuc.Text = "Doğru Cevap, Tebrikler!";
                    StackLayout.BackgroundColor = Color.FromHex("#5cb85c");
                    break;
                case 0:
                    Icon.Text = "✗";
                    Sonuc.Text = "Yanlış Cevap, Dikkatli Ol!";
                    StackLayout.BackgroundColor = Color.FromHex("#d9534f");
                    break;
                case -1:
                    Icon.Text = "⚫";
                    Sonuc.Text = "Zamanında Cevap Veremedin!";
                    StackLayout.BackgroundColor = Color.FromHex("#F7F7F7");
                    break;
            }

            Puan.Text = puan.ToString();
            StackLayout.Children.Add(Puan);
            StackLayout.Children.Add(Icon);
            StackLayout.Children.Add(Sonuc);
            Content = StackLayout;
        }

        ~SoruSonuc()
        {
            ClientListener.MessageArrived -= ButonaBasildi;

        }

        private void ButonaBasildi(String message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                int sure=0;
                if (message=="Sure Bitti Siradaki Soru")
                {
                    Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                    {
                        sure += 1;
                        if (sure >= ClientListener.Data.Sure.SkorSure)
                        {
                            Navigation.PushModalAsync(new SoruGoster());
                            return false;
                        }
                        return true; // True = Repeat again, False = Stop the timer
                    });
                }
                else if (message == "Sure Bitmedi Siradaki Soru")
                {
                    Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                    {
                        sure += 1;
                        if (sure >= ClientListener.Data.Sure.SkorSure + ClientListener.Data.Sure.DogruSure)
                        {
                            Navigation.PushModalAsync(new SoruGoster());
                            return false;
                        }
                        return true; // True = Repeat again, False = Stop the timer
                    });
                }
                else if (message== "Sorular Bitti")
                {
                    Navigation.PushModalAsync(new Bitis());
                }
            });
        }
    }
}