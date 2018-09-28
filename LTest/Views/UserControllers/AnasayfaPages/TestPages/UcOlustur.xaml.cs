using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Entity;
using LTest.Classes;
using LTest.Models.FacadeLayer;

namespace LTest.Views.UserControllers.AnasayfaPages.TestPages
{
    /// <summary>
    /// Interaction logic for UcOlustur.xaml
    /// </summary>
    public partial class UcOlustur : UserControl
    {
        public UcOlustur()
        {
            InitializeComponent();
        }

        private int _cevap;
        private int _soru;
        TestGoster tst;

        private void Olustur_Click(object sender, RoutedEventArgs e)
        {
            if (SoruTextbox.Text==string.Empty || CevapTextbox.Text==string.Empty || SureTextbox.Text == string.Empty || TestTextbox.Text==string.Empty)
            {
                UcAnasayfa.Durum("Lütfen tüm alanları doldurun", Global.Warning);
                return;
            }
            Global.GenelDurum = Global.Durum.TestOlusturuldu;

            _soru = Convert.ToInt16(SoruTextbox.Text);
            _cevap = Convert.ToInt16(CevapTextbox.Text);
            SoruStack.Children.Clear();
            tst = new TestGoster(_soru, _cevap);
            tst.ControlCreation();
            tst.AddControlsToDockPanel(SoruStack);
            UcAnasayfa.Durum("Test Şablonu Oluşturuldu", Global.Done);
        }

        private void Kaydet(object sender, RoutedEventArgs e)
        {
            if (Global.GenelDurum!=Global.Durum.TestOlusturuldu)
            {
                UcAnasayfa.Durum("Kaydedilecek bir şey yok!", Global.Warning);
                return;
            }

            var test = new Test
            {
                TestAdi = TestTextbox.Text,
                CevapSayisi = _cevap,
                SoruSayisi = _soru,
                Sure = Convert.ToInt32(SureTextbox.Text),
                KullaniciId=Global.ServerKullanici.KullaniciId
            };

            List<Soru> sorular = new List<Soru>();
            List<Cevap> cevaplar = new List<Cevap>();
            for (var i = 0; i < _soru; i++)
            {
                sorular.Add(new Soru
                {
                    SoruText = tst.SoruTextBoxes[i].Text,
                });

                for (var j = 0; j < _cevap; j++)
                {
                    cevaplar.Add(new Cevap
                    {
                        CevapText = tst.CevapTextboxes[i, j].Text,
                        Dogru = tst.CevapCheckBoxes[i, j].IsChecked == false ? 0 : 1
                    });
                }
            }

            int sonuc=CRUD.Insert(test, sorular, cevaplar);
            SoruStack.Children.Clear();
            if (sonuc>=0)
            {
                UcAnasayfa.Durum("Test Başarıyla Kaydedildi", Global.Done);
            }
            else
            {
                UcAnasayfa.Durum("Test Kaydı Başarısız", Global.Failed);
            }
        }

        private void SayiKontrol(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
