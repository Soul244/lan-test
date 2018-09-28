using System;
using System.Diagnostics;
using System.Globalization;
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
using LTest.Views.UserControllers;
using LTest.Views.UserControllers.AnasayfaPages;
using LTest.Views.UserControllers.GirisPages;
using WpfAnimatedGif;
using ChangeColor = LTest.Views.UserControllers.AnasayfaPages.UcAyarlar;
using UcTestler = LTest.Views.UserControllers.AnasayfaPages.UcTestler;

namespace LTest.Views
{
    public partial class Anasayfa : Window
    {
        static Anasayfa _main;
        //public static bool durum;
        //DispatcherTimer timer;

        //PermanentTrigger _permanent = new PermanentTrigger();
        public Anasayfa()
        {
            if (Debugger.IsAttached)
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            _main = this;
            InitializeComponent();
            AnasayfaIcerik.Content = new UcGiris();
            //mainContent.Content = new UcGirisYap();
            //Global.ChangeColour((SolidColorBrush)(new BrushConverter().ConvertFrom(Settings.Default.HexColor)));
            //MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            //MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            //DatabaseManager.CreateDatabase();
        }

        public static void Yonlendir(UserControl userControl)
        {
            _main.AnasayfaIcerik.Content = userControl;
        }


        //public static void Navigate(UserControl userControl)
        //{
        //    _main.mainContent.Content = userControl;
        //    if (userControl.GetType() == typeof(UcGirisYap))
        //    {
        //        _main.NavigateButton.Visibility = Visibility.Hidden;
        //    }
        //    else
        //    {
        //        _main.NavigateButton.Visibility = Visibility.Visible;
        //    }
        //}

        //private void Navigate_Click(object sender, RoutedEventArgs e)
        //{
        //    Navigate(new UcGirisYap());
        //}


        //public static void GirisInfo(string text, bool result)
        //{

        //    if (result)
        //    {
        //        _main.WarningText.Foreground = (SolidColorBrush)Application.Current.FindResource("SuccessColor");
        //    }
        //    else
        //    {
        //        _main.WarningText.Foreground = (SolidColorBrush)Application.Current.FindResource("WarningColor");
        //    }

        //    _main.WarningText.Content = text;

        //    TimeSpan time = TimeSpan.FromSeconds(5);
        //    _main.timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
        //    {
        //        if (time.Seconds <= 0)
        //        {
        //            _main.WarningText.Content = "";
        //            _main.timer.Stop();
        //        }
        //        time = time.Add(TimeSpan.FromSeconds(-1));
        //    }, Application.Current.Dispatcher);
        //    _main.timer.Start();
        //}

        //public static void GirisVisibility(bool result)
        //{
        //    if (result)
        //    {
        //        _main.Giris.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        _main.Giris.Visibility = Visibility.Hidden;
        //        _main.Icerik.Content = new UcBaslangic();

        //    }
        //}

        //private void AnaGrid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Active("Offline");
        //}
        //#region Menü Buttonları       
        //private void Baglangic(object sender, RoutedEventArgs e)
        //{
        //    //permanent.ChangeStack(menuStack,(Button)sender);
        //    Icerik.Content= new UcBaslangic();
        //}

        //private void Testler(object sender, RoutedEventArgs e)
        //{
        //    //permanent.ChangeStack(menuStack, (Button)sender);
        //    Icerik.Content = new UcTestler();
        //}

        //private void Ayarlar(object sender, RoutedEventArgs e)
        //{
        //    Icerik.Content = new UcAyarlar();
        //}

        //private void Sonuclar(object sender, RoutedEventArgs e)
        //{
        //    //permanent.ChangeStack(menuStack, (Button)sender);
        //    //UserControlClass.ControlAdd(Icerik, new UcSonuclar());
        //}

        //private void Toggle(object sender, RoutedEventArgs e)
        //{
        //    switch (sideBar.MaxWidth)
        //    {
        //        case max:
        //            sideBar.MaxWidth = min;
        //            break;
        //        default:
        //            sideBar.MaxWidth = max;
        //            break;
        //    }
        //}

        //#endregion
        //#region Otomatik Sidebar
        ///// <summary>
        ///// Her çözünürlük değişikliğinde pencere genişliğini alır ve buna göre dizaynı şekillendirir. Temel olarak 3 farklı dizayn şekli var.
        /////         
        ///// Dizayn Tasarımı 1 --> min 1024px genişlik ve max yok  --> 
        ///// Sol Menü 320 px - Iconlar 16px,  16 px margin var
        ///// 
        ///// Dizayn Tasarımı 2 --> max 1024px genişlik min 768px 
        ///// Sol Menü 48 px - Iconlar 16px, 16 px Margin var
        ///// 
        ///// Dizayn Tasarımı 3 --> max 768px min 360px
        ///// Menü Üstte taşınıyor.
        ///// Sol Menü 48 px - Icon 16 px, 16 px margin var.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //const int min = 53; // +5 Margin
        //const int max = 320;
        //private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    double width = ActualWidth;

        //    //Dizayn 1 --> Genişlik 1024'ten büyük ise
        //    if (width > 1024)
        //    {
        //        sideBar.MaxWidth = max;
        //    }
        //    // Dizayn 2 --> Genişlik 768'e eşit veya büyükse ve 1024'e eşit veya küçükse
        //    else if (width <= 1024)
        //    {
        //        sideBar.MaxWidth = min;
        //    }
        //}
        //#endregion Otomatik Sidebar Bitiş
        //#region Notification Bar
        //public static void Durum(string drm, Path icon)
        //{
        //    _main.NotificationLabel.Content = drm;
        //    _main.NotificationIcon.OpacityMask = new VisualBrush
        //    {
        //        Visual = icon
        //    };
        //}
        //public static void Active(string active)
        //{
        //    _main.ActiveLabel.Content = active;
        //    switch (active)
        //    {
        //        case "Online":
        //            durum = true;
        //            _main.ActiveLabel.Foreground = new SolidColorBrush(Colors.DarkGreen);
        //            ImageBehavior.SetAnimatedSource(_main.ActiveImage, new BitmapImage(new Uri("Images/online.gif", UriKind.Relative)));
        //            Global.GenelDurum = Global.Durum.OdaOnline;
        //            break;
        //        case "Offline":
        //            durum = false;
        //            _main.ActiveLabel.Foreground = new SolidColorBrush(Colors.Black);
        //            ImageBehavior.SetAnimatedSource(_main.ActiveImage, new BitmapImage(new Uri("Images/offline.gif", UriKind.Relative)));
        //            Global.GenelDurum = Global.Durum.OdaOffline;
        //            break;
        //    }
        //}
        //#endregion Notification Bar End
    }
}