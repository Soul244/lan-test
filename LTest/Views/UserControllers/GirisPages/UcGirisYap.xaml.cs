using Entity;
using LTest.Classes;
using LTest.Models.FacadeLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LTest.Views.UserControllers.GirisPages
{
    /// <summary>
    /// Interaction logic for UcGirisYap.xaml
    /// </summary>
    public partial class UcGirisYap : UserControl
    {
        public UcGirisYap()
        {
            InitializeComponent();
        }

        private void GirisYap_Click(object sender, RoutedEventArgs e)
        {
            // Kullanıcı Datasını Çek ve Kontrol Et
            var serverKullanici = new ServerKullanici
            {
                KullaniciAdi = KullaniciAdi.Text,
                Sifre = Sifre.Password
            };

            var serverKullaniciData = FServerKullanici.Select(serverKullanici);

            if (serverKullaniciData!=null)
            {
                Global.AktifKullanici(serverKullaniciData);
                Anasayfa.Yonlendir(new UcAnasayfa());
            }
            else
            {
                UcGiris.GirisInfo("Böyle Bir Kullanıcı Yok",false);
            }
        }

        private void YeniUyelik_Click(object sender, RoutedEventArgs e)
        {
            UcGiris.Navigate(new UcYeniUyelik());
        }

        private void SifremiUnuttum_Click(object sender, RoutedEventArgs e)
        {
            UcGiris.Navigate(new UcYeniSifre());
        }
    }
}
