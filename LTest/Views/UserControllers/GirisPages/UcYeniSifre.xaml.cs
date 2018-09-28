using LTest.Classes;
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
    /// Interaction logic for UcYeniSifre.xaml
    /// </summary>
    public partial class UcYeniSifre : UserControl
    {
        public UcYeniSifre()
        {
            InitializeComponent();
        }

        private void MailText_Changed(object sender, TextChangedEventArgs e)
        {
            bool result = Validator.IsValidEmailAddress(MailTextBox.Text);
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
