using System.Windows;
using System.Windows.Controls;
using LTest.Classes;

namespace LTest.Views.UserControllers.AnasayfaPages
{
    /// <summary>
    /// ucBaslangic.xaml etkileşim mantığı
    /// </summary>
    public partial class UcBaslangic : UserControl
    {
        PermanentTrigger permanent = new PermanentTrigger();

        public UcBaslangic() 
        {
            InitializeComponent();
        }


        private void UcOdaBaslat(object sender, RoutedEventArgs e)
        {
            permanent.Change(dockPanel, (Button)sender);
            Icerik.Content =new UcOdaBaslat();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PermanentTrigger permanent = new PermanentTrigger();
            permanent.Change(dockPanel, OdaBaslat);
            Icerik.Content = new UcOdaBaslat();
        }
    }
}
