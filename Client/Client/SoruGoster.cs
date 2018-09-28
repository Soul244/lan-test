using Entity;
using LTest.Classes;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Client
{
    public class SoruGoster : ContentPage
	{
        List<Button> buttons=new List<Button>();
        List<Rectangle> rectangles = new List<Rectangle>();
        List<Color> colors = new List<Color>();
        private Test _test=ClientListener.Data.Test;
        private Sure _sureler= ClientListener.Data.Sure;
        private List<Soru> _sorular= ClientListener.Data.Sorular;
        private List<Cevap> _cevaplar = ClientListener.Data.Cevaplar;
        float sure=0;
        float totalPuan = 0;
        int sonuc = -1;
        bool sureBitti = false;
        public SoruGoster()
		{

            SoruYukle();
        }

        private void SoruYukle()
        {

            int gridRowCount = Convert.ToInt16(Math.Ceiling((decimal)_test.CevapSayisi / 2));
            int gridColumnCount = 2;
            // Grid
            Grid mainGrid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)  },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                }
            };

            for (int i = 0; i < gridRowCount; i++)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star)
                });
            }

            // COLORS ADD
            #region Color
            colors.Add(Color.Red);
            colors.Add(Color.CornflowerBlue);
            colors.Add(Color.Orange);
            colors.Add(Color.MediumVioletRed);
            colors.Add(Color.ForestGreen);
            colors.Add(Color.Gold);
            colors.Add(Color.HotPink);
            colors.Add(Color.Teal);
            #endregion


            // OTHER COMPENENTS
            for (int i = 0; i < _test.CevapSayisi; i++)
            {
                buttons.Add(new Button
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = colors[i],
                });

                buttons[i].Clicked += new EventHandler(Button_Clicked);
            }

            // ADDING COMPENENTS TO STACKLAYOUTS
            byte btnIndex = 0;
            for (int i = 0; i < gridRowCount; i++)
            {
                for (int k = 0; k < gridColumnCount; k++)
                {
                    if (btnIndex >= _test.CevapSayisi)
                    {
                        break;
                    }
                    Grid.SetRow(buttons[btnIndex], i);
                    Grid.SetColumn(buttons[btnIndex], k);
                    btnIndex++;
                }
            }

            for (int i = 0; i < _test.CevapSayisi; i++)
            {
                mainGrid.Children.Add(buttons[i]);
            }

            Content = mainGrid;

            Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
            {
                sure += 0.1f;
                if (sure >= _test.Sure)
                {
                    if (sonuc == -1)
                    {
                        ClientListener.Kullanici.Sorular.Add(new ClientKullanici.SoruOzellikleri
                        {
                            Cevap = 99,
                            CevapSuresi = sure,
                        });
                        ClientListener.SendObject();
                        Navigation.PushModalAsync(new SoruSonuc(sonuc, totalPuan));
                        sure = 0;
                        IndexArtır();
                        sureBitti = true;
                        return false;
                    }
                }
                return true; // True = Repeat again, False = Stop the timer
            });
        }

        private void IndexArtır()
        {
            if (ClientListener.SoruIndex<=ClientListener.Data.Sorular.Count)
            {
                ClientListener.SoruIndex++;
            }
            else
            {
                Navigation.PushModalAsync(new Bitis());
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            // Soruya Ait Cevapları Bul
            List<Cevap> cevaplar = new List<Cevap>();
            int soruId = _sorular[ClientListener.SoruIndex].SoruId;
            foreach (Cevap cevap in _cevaplar)
            {
                if (cevap.SoruId==soruId)
                {
                    cevaplar.Add(cevap);
                }
            }

            Button button = (Button)sender;

            // Tıklanılan butonun indexini bul ve Hangi Cevabın verildiğini tespit et.
            int cevapIndex = buttons.IndexOf(button);

            if (Convert.ToBoolean(cevaplar[cevapIndex].Dogru))
            {
                sonuc = 1;
            }
            else
            {
                sonuc = 0;
            }

            float puan;
            if (!sureBitti && sonuc==1)
            {
                puan = 250 / sure;
            }
            else
            {
                puan = 0;
            }


            ClientListener.Kullanici.Sorular.Add(new ClientKullanici.SoruOzellikleri
            {
                Puan = puan,
                CevapSuresi = sure,
            });

            if (ClientListener.Kullanici.Sorular.Count > 0)
            {
                foreach (var item in ClientListener.Kullanici.Sorular)
                {
                    totalPuan += item.Puan;
                }
            }

            ClientListener.Kullanici.TotalPuan = totalPuan;

            // Cevabı Gönder
            ClientListener.SendObject();

            IndexArtır();
            // Bekleme Ekranını AÇ
            Navigation.PushModalAsync(new SoruSonuc(sonuc,totalPuan));

        }
    }
}