using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using LTest.Classes;
using LTest.Properties;

namespace LTest.Views.UserControllers.AnasayfaPages
{
    /// <summary>
    /// Interaction logic for UcAyarlar.xaml
    /// </summary>
    public partial class UcAyarlar : UserControl
    {
        public UcAyarlar()
        {
            InitializeComponent();
            for (var i = 0; i < 8; i++)
            {
                BaslangicSure.Items.Add(i+1);
                SkorSure.Items.Add(i+1);
            }
            BaslangicSure.SelectedIndex = Settings.Default.BaslangicSure-1;
            SkorSure.SelectedIndex = Settings.Default.SkorSure-1;
        }

        private new void MouseUp(object sender, MouseButtonEventArgs e)
        {
            var ellipse = (Ellipse)e.Source;
            var solidColor = (SolidColorBrush)ellipse.Fill;
            Global.ChangeColour(solidColor);
        }

        private void SkorSure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.SkorSure = Convert.ToByte(SkorSure.SelectedValue);
            Settings.Default.Save();
        }

        private void BaslangicSure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.BaslangicSure = Convert.ToByte(BaslangicSure.SelectedValue);
            Settings.Default.Save();
        }
    }
}
