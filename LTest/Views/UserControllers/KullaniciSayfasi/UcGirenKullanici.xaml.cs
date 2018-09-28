using System.Windows.Controls;

namespace LTest.Views.UserControllers.KullaniciSayfasi
{
    /// <summary>
    /// Interaction logic for UcGirenKullanici.xaml
    /// </summary>
    public partial class UcGirenKullanici : UserControl
    {
        public UcGirenKullanici()
        {
            InitializeComponent();
        }

        public int PanelCount { get; internal set; }
    }
}
