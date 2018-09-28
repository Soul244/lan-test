using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using LTest.Classes;
using LTest.Models;
using LTest.Properties;
using LTest.Views.UserControllers.AnasayfaPages;
using LTest.Views.UserControllers.GirisPages;
using WpfAnimatedGif;
using UcAyarlar = LTest.Views.UserControllers.AnasayfaPages.UcAyarlar;
using UcTestler = LTest.Views.UserControllers.AnasayfaPages.UcTestler;

namespace LTest.Views.UserControllers
{
    public partial class UcAnasayfa : UserControl
    {
        static UcAnasayfa _main;
        public static bool durum;

        PermanentTrigger _permanent = new PermanentTrigger();
        public UcAnasayfa()
        {
            _main = this;
            InitializeComponent();
            UcAnasayfaIcerik.Content = new UcBaslangic();
            KullaniciAdi.Text = "Hoşgeldin, " + Global.ServerKullanici.KullaniciAdi;
            Global.ChangeColour((SolidColorBrush)(new BrushConverter().ConvertFrom(Settings.Default.HexColor)));
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            DatabaseManager.CreateDatabase();
        }

        private void AnaGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Active("Offline");
        }
        #region Menü Buttonları       
        private void Baglangic(object sender, RoutedEventArgs e)
        {
            //permanent.ChangeStack(menuStack,(Button)sender);
            UcAnasayfaIcerik.Content = new UcBaslangic();
        }

        private void Testler(object sender, RoutedEventArgs e)
        {
            //permanent.ChangeStack(menuStack, (Button)sender);
            UcAnasayfaIcerik.Content = new UcTestler();
        }

        private void Ayarlar(object sender, RoutedEventArgs e)
        {
            UcAnasayfaIcerik.Content = new UcAyarlar();
        }

        private void Sonuclar(object sender, RoutedEventArgs e)
        {
            //permanent.ChangeStack(menuStack, (Button)sender);
            //UserControlClass.ControlAdd(Icerik, new UcSonuclar());
        }

        private void Toggle(object sender, RoutedEventArgs e)
        {
            switch (sideBar.MaxWidth)
            {
                case max:
                    sideBar.MaxWidth = min;
                    break;
                default:
                    sideBar.MaxWidth = max;
                    break;
            }
        }

        #endregion
        #region Otomatik Sidebar
        /// <summary>
        /// Her çözünürlük değişikliğinde pencere genişliğini alır ve buna göre dizaynı şekillendirir. Temel olarak 3 farklı dizayn şekli var.
        ///         
        /// Dizayn Tasarımı 1 --> min 1024px genişlik ve max yok  --> 
        /// Sol Menü 320 px - Iconlar 16px,  16 px margin var
        /// 
        /// Dizayn Tasarımı 2 --> max 1024px genişlik min 768px 
        /// Sol Menü 48 px - Iconlar 16px, 16 px Margin var
        /// 
        /// Dizayn Tasarımı 3 --> max 768px min 360px
        /// Menü Üstte taşınıyor.
        /// Sol Menü 48 px - Icon 16 px, 16 px margin var.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        const int min = 53; // +5 Margin
        const int max = 320;
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = ActualWidth;

            //Dizayn 1 --> Genişlik 1024'ten büyük ise
            if (width > 1024)
            {
                sideBar.MaxWidth = max;
            }
            // Dizayn 2 --> Genişlik 768'e eşit veya büyükse ve 1024'e eşit veya küçükse
            else if (width <= 1024)
            {
                sideBar.MaxWidth = min;
            }
        }
        #endregion Otomatik Sidebar Bitiş
        #region Notification Bar
        public static void Durum(string drm, Path icon)
        {
            _main.NotificationLabel.Content = drm;
            _main.NotificationIcon.OpacityMask = new VisualBrush
            {
                Visual = icon
            };
        }
        public static void Active(string active)
        {
            _main.ActiveLabel.Content = active;
            switch (active)
            {
                case "Online":
                    durum = true;
                    _main.ActiveLabel.Foreground = new SolidColorBrush(Colors.DarkGreen);
                    ImageBehavior.SetAnimatedSource(_main.ActiveImage, new BitmapImage(new Uri("Images/online.gif", UriKind.Relative)));
                    Global.GenelDurum = Global.Durum.OdaOnline;
                    break;
                case "Offline":
                    durum = false;
                    _main.ActiveLabel.Foreground = new SolidColorBrush(Colors.Black);
                    ImageBehavior.SetAnimatedSource(_main.ActiveImage, new BitmapImage(new Uri("Images/offline.gif", UriKind.Relative)));
                    Global.GenelDurum = Global.Durum.OdaOffline;
                    break;
            }
        }
        #endregion Notification Bar End
    }
}