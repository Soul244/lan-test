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
    /// Interaction logic for UcYeniUyelik.xaml
    /// </summary>
    public partial class UcYeniUyelik : UserControl
    {
        public UcYeniUyelik()
        {
            InitializeComponent();
        }

        private void YeniUyelik_Click(object sender, RoutedEventArgs e)
        {
            var serverKullanici = new ServerKullanici
            {
                KullaniciAdi = KullaniciAdi.Text,
                Sifre=Sifre.Password,
                Mail=MailAdresi.Text
            };

            var result = FServerKullanici.Insert(serverKullanici);
            if (result>0)
            {
                UcGiris.GirisInfo("Kayıt Oluşturuldu",true);
            }
            else
            {
                UcGiris.GirisInfo("Kayıt Başarısız",true);
            }
        }

        private void MailValidate_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool result = Validator.IsValidEmailAddress(MailAdresi.Text);
            if (!result)
            {
                UcGiris.GirisInfo("Lütfen doğru mail adresini formatı giriniz.",false);
            }
            else
            {
                UcGiris.GirisInfo("",false);
            }
        }
    }
}
