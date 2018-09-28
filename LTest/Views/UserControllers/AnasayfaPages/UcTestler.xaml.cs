using System.Windows;
using System.Windows.Controls;
using LTest.Classes;
using LTest.Views.UserControllers.AnasayfaPages.TestPages;

namespace LTest.Views.UserControllers.AnasayfaPages
{
    /// <summary>
    /// UcTestler.xaml etkileşim mantığı
    /// </summary>
    public partial class UcTestler
    {
        readonly PermanentTrigger _permanent = new PermanentTrigger();

        public UcTestler()
        {
            InitializeComponent();
        }

        private void UcOlustur(object sender, RoutedEventArgs e)
        {
            _permanent.Change(dockPanel, (Button)sender);
            Icerik.Content = new UcOlustur();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var permanent = new PermanentTrigger();
            permanent.Change(dockPanel, Olustur);
            Icerik.Content= new UcOlustur();
        }

        private void UcDuzenle(object sender, RoutedEventArgs e)
        {
            _permanent.Change(dockPanel, (Button)sender);
            Icerik.Content = new UcDuzenle();

        }

        //private int _cevap;
        //private int _soru;
        //int cevapSayisi;
        //int soruSayisi;
        //private TextBox[] _soruTextBox;
        //private TextBox[,] _cevapTextBox;
        //private CheckBox[,] _cevapCheckBox;
        //List<Testler> testler;
        //private void Olustur_Click(object sender, RoutedEventArgs e)
        //{
        //    var myBinding = new Binding // Bir üst kontrolün genişliğini almanı sağlayan kod.
        //    {
        //        Path = new PropertyPath("soruDock")
        //    };
        //    _soru = Convert.ToInt16(SoruTextbox.Text);
        //    _cevap = Convert.ToInt16(CevapTextbox.Text);

        //    SoruDock.Children.Clear();
        //    var tst = new TestGoster(_soru, _cevap);
        //    tst.ControlCreation();
        //    (_soruTextBox, _cevapTextBox, _cevapCheckBox) = tst.AddControlsToDockPanel(myBinding, SoruDock);
        //}
        //private void Kaydet(object sender, RoutedEventArgs e)
        //{
        //    cevapSayisi = Convert.ToInt16(CevapTextbox.Text);
        //    soruSayisi = Convert.ToInt16(SoruTextbox.Text);
        //    var item = new Testler
        //    {
        //        TestAdi = TestTextbox.Text,
        //        CevapSayisi = cevapSayisi,
        //        SoruSayisi = soruSayisi,
        //        Sure = Convert.ToInt32(SureTextbox.Text)
        //    };
        //    var testId = BTestler.Insert(item);
        //    if (testId >= 0)
        //    {
        //        for (var i = 0; i < soruSayisi; i++)
        //        {
        //            var soru = new Sorular
        //            {
        //                TestId = testId,
        //                Soru = _soruTextBox[i].Text
        //            };
        //            var soruId = BSorular.Insert(soru);

        //            if (soruId >= 0)
        //            {
        //                for (var j = 0; j < cevapSayisi; j++)
        //                {
        //                    var cevap = new Cevaplar
        //                    {
        //                        SoruId = soruId,
        //                        Cevap = _cevapTextBox[i, j].Text,
        //                        Dogru = _cevapCheckBox[i, j].IsChecked == false ? 0 : 1
        //                    };
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Anasayfa.Durum(testId >= 0 ? "Test Kayıtları Başarılı" : "Kayıt Başarısız");
        //    }
        //    TestAdiDoldur();
        //}

        //public static void SayiKontrol(object sender, TextCompositionEventArgs e)
        //{
        //    var regex = new Regex("[^0-9]+");
        //    e.Handled = regex.IsMatch(e.Text);
        //}

        //private void UserControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    TestAdiDoldur();
        //}

        //private void TestDuzeltCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var testler = BTestler.Select(TestDuzeltCombobox.SelectedValue.ToString());
        //    int testId = Convert.ToInt16(testler.TestId);
        //    SureDuzeltTextbox.Text = testler.Sure.ToString();
        //    SoruDuzeltTextbox.Text = testler.SoruSayisi.ToString();
        //    CevapDuzeltTextbox.Text = testler.CevapSayisi.ToString();

        //    var myBinding = new Binding // Bir üst kontrolün genişliğini almanı sağlayan kod.
        //    {
        //        Path = new PropertyPath("soruDock")
        //    };

        //    SoruDock.Children.Clear();
        //    var tst = new TestGoster(Convert.ToInt16(testler.SoruSayisi), Convert.ToInt16(testler.CevapSayisi));
        //    tst.ControlCreation();
        //    (_soruTextBox, _cevapTextBox, _cevapCheckBox) = tst.AddControlsToDockPanel(myBinding, SoruDock);
        //    var sorular = BSorular.SelectAll(testId);

        //    var i = 0;
        //    var q = 0;
        //    if (sorular == null) return;


        //    foreach (var item in sorular)
        //    {
        //        tst.SoruTextBoxes[i].Text = item.Soru;
        //        var cevaplar = BCevaplar.SelectAll(item.SoruId);
        //        if (cevaplar == null) return;
        //        foreach (var item2 in cevaplar)
        //        {
        //            _cevapTextBox[i, q].Text = item2.Cevap;
        //            _cevapCheckBox[i, q].IsChecked = item2.Dogru == 1;
        //            q++;
        //        }
        //        q = 0;
        //        i++;
        //    }
        //}

        //private void Duzelt(object sender, RoutedEventArgs e)
        //{
        //    cevapSayisi = Convert.ToInt16(CevapTextbox.Text);
        //    soruSayisi = Convert.ToInt16(SoruTextbox.Text);
        //    var testId = testler[TestDuzeltCombobox.SelectedIndex].TestId;
        //    var item = new Testler
        //    {
        //        TestId = testId,
        //        TestAdi = TestTextbox.Text,
        //        CevapSayisi = cevapSayisi,
        //        SoruSayisi = soruSayisi,
        //        Sure = Convert.ToInt32(SureTextbox.Text)
        //    };

        //    if (testId >= 0)
        //    {
        //        for (var i = 0; i < soruSayisi; i++)
        //        {
        //            var soru = new Sorular
        //            {
        //                TestId = testId,
        //                Soru = _soruTextBox[i].Text
        //            };
        //            var soruId = FSorular.Update(soru);

        //            if (soruId >= 0)
        //            {
        //                for (var j = 0; j < cevapSayisi; j++)
        //                {
        //                    var cevap = new Cevaplar
        //                    {
        //                        SoruId = soruId,
        //                        Cevap = _cevapTextBox[i, j].Text,
        //                        Dogru = _cevapCheckBox[i, j].IsChecked == false ? 0 : 1
        //                    };
        //                    FCevaplar.Update(cevap);
        //                }
        //            }
        //        }

        //    }
        //}

        //void TestAdiDoldur()
        //{
        //    testler = new List<Testler>();
        //    testler = BTestler.SelectAll();
        //    if (testler == null) return;
        //    foreach (var test in testler)
        //    {
        //        TestDuzeltCombobox.Items.Add(test.TestAdi);
        //    }
        //}

    }
}
