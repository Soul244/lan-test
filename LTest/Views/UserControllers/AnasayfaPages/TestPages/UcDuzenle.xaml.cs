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
    /// Interaction logic for UcDuzenle.xaml
    /// </summary>
    public partial class UcDuzenle : UserControl
    {
        public UcDuzenle()
        {
            InitializeComponent();
        }

        TestGoster tst;
        List<Test> testler;

        Test _seciliTest;
        List<Soru> _sorular;
        List<Cevap> _cevaplar;

        private void TestCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TestCombobox.SelectedValue==null)
            {
                return;
            }
            Global.GenelDurum = Global.Durum.TestDuzenleSecildi;
            (_seciliTest, _sorular, _cevaplar) = CRUD.Select(TestCombobox.SelectedValue.ToString());

            SureTextbox.Text = _seciliTest.Sure.ToString();
            SoruTextbox.Text = _seciliTest.SoruSayisi.ToString();
            CevapTextbox.Text = _seciliTest.CevapSayisi.ToString();

            SoruStack.Children.Clear();
            tst = new TestGoster(Convert.ToInt16(_seciliTest.SoruSayisi), Convert.ToInt16(_seciliTest.CevapSayisi));
            tst.ControlCreation();
            tst.AddControlsToDockPanel(SoruStack);

            int cevapIndex = 0;
            for (int i = 0; i < _sorular.Count; i++)
            {
                tst.SoruTextBoxes[i].Text = _sorular[i].SoruText;
                for (int k = 0; k < _seciliTest.CevapSayisi; k++)
                {
                    tst.CevapTextboxes[i, k].Text = _cevaplar[cevapIndex].CevapText;
                    tst.CevapCheckBoxes[i, k].IsChecked = _cevaplar[cevapIndex].Dogru == 1 ? true : false;
                    cevapIndex++;
                }
            }
        }

        private void Duzelt(object sender, RoutedEventArgs e)
        {
            if (Global.GenelDurum!=Global.Durum.TestDuzenleSecildi)
            {
                UcAnasayfa.Durum("Herhangi bir test seçilmedi.", Global.Warning);
                return;
            }

            _seciliTest.Sure = Convert.ToInt32(SureTextbox.Text);

            int cevapIndex = 0;
            for (var i = 0; i < _seciliTest.SoruSayisi; i++)
            {
                _sorular[i].SoruText = tst.SoruTextBoxes[i].Text;
                for (var j = 0; j < _seciliTest.CevapSayisi; j++)
                {
                    _cevaplar[cevapIndex].CevapText = tst.CevapTextboxes[i, j].Text;
                    _cevaplar[cevapIndex].Dogru = tst.CevapCheckBoxes[i, j].IsChecked == false ? 0 : 1;
                    cevapIndex++;
                }
            }
            var a=_cevaplar;
            int sonuc= CRUD.Update(_seciliTest, _sorular, _cevaplar);
            if (sonuc>0)
            {
                UcAnasayfa.Durum("Test Güncellendi", Global.Done);
            }
            else
            {
                UcAnasayfa.Durum("Test Güncellenemedi", Global.Failed);
            }
        }
        void TestAdiDoldur()
        {
            TestCombobox.Items.Clear();
            testler = new List<Test>();
            testler = FTest.SelectAll(Global.ServerKullanici.KullaniciId);
            if (testler == null) return;
            foreach (var test in testler)
            {
                TestCombobox.Items.Add(test.TestAdi);
            }
        }
        private void SayiKontrol(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Duzelt_Loaded(object sender, RoutedEventArgs e)
        {
            TestAdiDoldur();
        }

        private void Test_Sil(object sender, RoutedEventArgs e)
        {
            if (Global.GenelDurum != Global.Durum.TestDuzenleSecildi)
            {
                UcAnasayfa.Durum("Herhangi bir test seçilmedi.", Global.Warning);
                return;
            }
            int sonuc=CRUD.Delete(_seciliTest);
            if (sonuc>0)
            {
                UcAnasayfa.Durum("Test Başarıyla Silindi", Global.Done);
                SoruStack.Children.Clear();
                TestAdiDoldur();
                SureTextbox.Text = string.Empty;
                CevapTextbox.Text = string.Empty;
                SoruTextbox.Text = string.Empty;
            }
            else
            {
                UcAnasayfa.Durum("Test Silinemedi", Global.Failed);
            }
        }

        private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Global.GenelDurum != Global.Durum.TestDuzenleSecildi || TestCombobox.SelectedValue == null ||
                string.IsNullOrEmpty(SoruTextbox.Text) ||
                string.IsNullOrEmpty(CevapTextbox.Text))
            {
                return;
            }

            int soruSayisi = Convert.ToInt16(SoruTextbox.Text);
            int cevapSayisi = Convert.ToInt16(CevapTextbox.Text);
            //if (soruSayisi<_seciliTest.SoruSayisi || cevapSayisi<_seciliTest.CevapSayisi)
            //{
            //    var result=MessageBox.Show("Soru sayısı ya da cevap sayısını azaltıyorsunuz. Bu yüzden bazı bilgiler kaybolacak. " +
            //        "Emin misiniz?","Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            //    if (result==MessageBoxResult.No)
            //    {
            //        return;
            //    }
            //}
            Test test = new Test
            {
                SoruSayisi = soruSayisi,
                CevapSayisi = cevapSayisi
            };
            SoruStack.Children.Clear();

            tst = new TestGoster(Convert.ToInt16(test.SoruSayisi), Convert.ToInt16(test.CevapSayisi));
            tst.ControlCreation();
            tst.AddControlsToDockPanel(SoruStack);

            int cevapIndex = 0;
            for (int i = 0; i < _sorular.Count; i++)
            {
                if (i == test.SoruSayisi)
                {
                    break;
                }
                tst.SoruTextBoxes[i].Text = _sorular[i].SoruText;
                for (int k = 0; k < _seciliTest.CevapSayisi; k++)
                {
                    if (k == test.CevapSayisi)
                    {
                        break;
                    }
                    tst.CevapTextboxes[i, k].Text = _cevaplar[cevapIndex].CevapText;
                    tst.CevapCheckBoxes[i, k].IsChecked = _cevaplar[cevapIndex].Dogru == 1 ? true : false;
                    cevapIndex++;
                }
            }

        }
    }
}
