using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Entity;
using LTest.Classes;
using LTest.Models.FacadeLayer;
using LTest.Properties;
using LTest.Views.UserControllers;
using LTest.Views.UserControllers.KullaniciSayfasi;
using UcSkorListesi = LTest.Views.UserControllers.KullaniciSayfasi.UcSkorListesi;

namespace LTest.Views
{
    /// <summary>
    /// Interaction logic for KullaniciSayfasi.xaml
    /// </summary>
    public partial class KullaniciSayfasi : Window
    {
        #region Definitions
        private Listener _listener;
        private readonly List<StackPanel> _stackPanels = new List<StackPanel>();
        private readonly List<UcGirenKullanici> _ucGirenKullanici = new List<UcGirenKullanici>();
        private readonly List<ClientKullanici> _kullanicilar = new List<ClientKullanici>();
        private readonly List<Client> _clients = new List<Client>();

        private readonly List<SolidColorBrush> _colors;
        private readonly List<System.Windows.Shapes.Path> _icons;

        private UcSoru _ucSoru;
        private List<Soru> _sorular;
        private List<Cevap> _cevaplar;
        
        private Test _test;
        private byte _soruIndex;
        Sure _sure = new Sure();
        private const int ColumnCount = 5;
        private int _userCount = 0;
        private int _panel = 0;
        private readonly string _testAdi;
        byte[] receivedBuf = new Byte[1024 * 1024 * 50];
        private bool sureBitti = false;
        private Message _message=new Message();
        #endregion

        public KullaniciSayfasi(string testAdi)
        {
            InitializeComponent();
            _colors = Global.Colors();
            _icons = Global.Icons();
            _testAdi = testAdi;
            _test = FTest.Select(_testAdi); // Boş Yapma
        }

        //public static string ByteArrayToString(byte[] ba)
        //{
        //    string hex = BitConverter.ToString(ba);
        //    return hex.Replace("-", "");
        //}

        //public static string ToAddress(string address)
        //{
        //    char[] getIp = new IPAddress(long.Parse(address, System.Globalization.NumberStyles.AllowHexSpecifier)).ToString().ToCharArray();
        //    return new string(getIp);
        //}

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Static listeler olduğu için sıfırlıyorum.
                _ucGirenKullanici.Clear();
                _clients.Clear();
                //ipRow.Height = new GridLength(128);

                // Socket İşlemleri
                _listener = new Listener(100);
                _listener.SocketAccepted += Listener_SocketAccepted;
                _listener.Start();
            }
            catch (SocketException k)
            {
                MessageBox.Show(k.ToString());
            }

            // Kullanıcıların eklenmesi için stackpanel oluşturuyorum.
            for (int i = 0; i < ColumnCount; i++)
            {
                _stackPanels.Add(new StackPanel());
                KullaniciGrid.Children.Add(_stackPanels[i]);
                Grid.SetColumn(_stackPanels[i], i);
            }

            _sure.BaslangicSure = Settings.Default.BaslangicSure;
            _sure.SkorSure = Settings.Default.SkorSure;
            _sure.DogruSure = Settings.Default.DogruSure;
            // Ip Adresi bulup, ekrana yazdırıyorum.
            var ipv4Addresses = Array.FindAll(
                Dns.GetHostEntry(string.Empty).AddressList,
                a => a.AddressFamily == AddressFamily.InterNetwork);

            // Son ip adresini çekiyorum. Makinede virtual box ya da başka emülatör var ise onun iplerini de çekiyor. Sonuncu ip bilgisayarın ip'si.
            IpAdresi.Text += " " + ipv4Addresses[ipv4Addresses.Length - 1].ToString();

            //Eğer kullanıcı yoksa ekranda kullanıcıların beklendiğini gösteren döngü 
            while (_clients.Count < 1 && Global.GenelDurum != Global.Durum.TestBaslatildi)
            {
                await Bekle();
            }


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Sayfayı Kapatırsanız, Odada Kapanacaktır. Kapatmak İstediğinizi Emin Misiniz?",
                    "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Global.GenelDurum = Global.Durum.OdaOffline;
                _listener.Stop();
                UcAnasayfa.Active("Offline");
            }
            //CustomMessageBox.Show("Merhaba Dünyalı");
        }

        #region Client İşlemleri

        private void Listener_SocketAccepted(Socket e)
        {
            var rnd = new Random();
            _clients.Add(new Client(e));
            _clients[_userCount].Received += Client_Received;
            _clients[_userCount].Disconnected += Client_Disconnected;
            Dispatcher.BeginInvoke(new Action(delegate
            {
                //Giren kullanıcıyla beraber bir UcGirenKullanici nesneyi türetiyor ve girdilerini ekliyorum.
                _ucGirenKullanici.Add(new UcGirenKullanici());
                _ucGirenKullanici[_userCount].Name.Text = "Bekleniyor...";
                _ucGirenKullanici[_userCount].EndPoint.Text = _clients[_userCount].GetEndPoint().ToString();
                _ucGirenKullanici[_userCount].Sira.Text = (_userCount + 1).ToString();
                _ucGirenKullanici[_userCount].SiraBorder.Background = _colors[rnd.Next(0, 8)];

                Info.Text = "Kullanıcı Sayısı: " + (_userCount + 1).ToString();

                _stackPanels[_panel].Children.Add(_ucGirenKullanici[_userCount]);
                _ucGirenKullanici[_userCount].PanelCount = _panel;
                _userCount++;
                _panel++;
                if (_panel == 5)
                {
                    _panel = 0;
                }
            }));
        }

        private void Client_Disconnected(Client sender)
        {
            _clients.Remove(sender);
            for (var i = 0; i < _ucGirenKullanici.Count; i++)
            {
                if (_ucGirenKullanici[i].EndPoint.Text != sender.GetEndPoint().ToString()) continue;
                _stackPanels[_ucGirenKullanici[i].PanelCount].Children.Remove(_ucGirenKullanici[i]);
                _ucGirenKullanici.RemoveAt(i);
            }
        }

        // Kullanıcıdan VERİ AL
        private void Client_Received(Client sender, byte[] data)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                for (var i = 0; i < _userCount; i++)
                {
                    if (_clients[i].GetId() != sender.GetId()) continue;
                    object obj = _listener.GetObject(data);
                    if (obj == null) continue;
                    if (obj.GetType() == typeof(ClientKullanici))
                    {
                        ClientKullanici gelenKullanici = ((ClientKullanici)obj);
                        bool yeniKullanici = true;
                        foreach (var kullanici in _kullanicilar)
                        {
                            if (kullanici.KullaniciAdi == gelenKullanici.KullaniciAdi)
                            {
                                _kullanicilar[_kullanicilar.IndexOf(kullanici)] = gelenKullanici;
                                yeniKullanici = false;
                                break;
                            }
                            else
                            {
                                yeniKullanici = true;
                            }
                        }
                        if (yeniKullanici)
                        {
                            _kullanicilar.Add(gelenKullanici);
                            _kullanicilar[_userCount - 1].Sorular = new List<ClientKullanici.SoruOzellikleri>();
                        }
                    }
                    _ucGirenKullanici[i].Name.Text = _kullanicilar[i].KullaniciAdi;
                    break;
                }
            }));
        }
        #endregion

        #region Bekleniyor Yazısı
        int _k = 0;
        string _nokta = string.Empty;
        string _text = "Kullanıcılar Bekleniyor";
        async Task Bekle()
        {
            _nokta = _nokta + ".";
            Info.Text = _text + _nokta;
            _k++;
            if (_k > 10)
            {
                _k = 0;
                Info.Text = _text;
                _nokta = "";
            }
            await Task.Delay(100);
        }
        #endregion


        private void TestBaslat_Click(object sender, RoutedEventArgs e)
        {

            foreach (var item in KullaniciGrid.Children)
            {
                StackPanel stack = (StackPanel)item;
                stack.Children.Clear();
            }
            IpRow.Height = new GridLength(0);
            CountDownGrid.Visibility = Visibility.Visible;
            TestBaslatButton.Visibility = Visibility.Hidden;
            Global.GenelDurum = Global.Durum.TestBaslatildi;

            // Anasayfadan seçilen test adi ile tüm test verileri çağrılıyor

            Info.Text = "Kullanıcı Sayısı: " + _userCount;
            _ucSoru = new UcSoru(_test.CevapSayisi);
            Sorular.Children.Add(_ucSoru);
            _sorular = FSoru.SelectAll(_test.TestId); // Tüm Sorular

            _listener.Clients = _clients;

            var cevaplar = FCevap.SelectByTestId(_test.TestId);

            TotalData data = new TotalData
            {
                Sure = _sure,
                Test = _test,
                Sorular = _sorular,
                Cevaplar = cevaplar
            };


            _listener.SendObject(data); // Her şeyi gönder


             BaslangicGeriSayim();
        }

        private void SiradakiSoru_Click(object sender, RoutedEventArgs e)
        {
            _kullanicilar.OrderBy(x => x.TotalPuan);
            if (_soruIndex < _sorular.Count)
            {
                if (sureBitti)
                {
                    _listener.SendObject(_message.Txt = "Sure Bitti Siradaki Soru");
                    SoruGoster();
                }
                else
                {
                    _listener.SendObject(_message.Txt = "Sure Bitmedi Siradaki Soru");
                    DogruGosterWithTimer();
                }
            }
            else
            {
                _listener.SendObject(_message.Txt = "Sorular Bitti");
                Global.GenelDurum = Global.Durum.TestBitti;
                Sorular.Children.Clear();
                UcSkorListesiGoster();
            }
        }

        private void UcSkorListesiGoster()
        {
            UcSkorListesi ucSkorListesi = new UcSkorListesi();
            int count = _kullanicilar.Count;
            if (count>0)
            {
                ucSkorListesi.Birinci.Text = _kullanicilar[0].KullaniciAdi;
                if (count>1)
                {
                    ucSkorListesi.Ikinci.Text = _kullanicilar[1].KullaniciAdi;
                    if (count>2)
                    {
                        ucSkorListesi.Ucuncu.Text = _kullanicilar[2].KullaniciAdi;
                    }
                }
            }
            ucSkorListesi.TumSkorlar.DataContext = _kullanicilar;
            Sorular.Children.Add(ucSkorListesi);

        }

        private void UcSkorGoster()
        {
            UcSkor ucSkor = new UcSkor();
            int count = _kullanicilar.Count;
            if (count > 0)
            {
                ucSkor.Birinci.Text = _kullanicilar[0].KullaniciAdi;
                ucSkor.BirinciSkor.Text = _kullanicilar[0].TotalPuan.ToString();
                if (count > 1)
                {
                    ucSkor.Ikinci.Text = _kullanicilar[1].KullaniciAdi;
                    ucSkor.IkinciSkor.Text = _kullanicilar[1].TotalPuan.ToString();

                    if (count > 2)
                    {
                        ucSkor.Ucuncu.Text = _kullanicilar[2].KullaniciAdi;
                        ucSkor.UcuncuSkor.Text = _kullanicilar[2].TotalPuan.ToString();

                    }
                }
            }
            Sorular.Children.Add(ucSkor);

        }

        public void SendObject(object obj)
        {
            BinaryFormatter _formatter = new BinaryFormatter();
            MemoryStream _memoryStream = new MemoryStream();
            _formatter.Serialize(_memoryStream, obj);
            byte[] buffer = _memoryStream.ToArray();
            foreach (var client in _clients)
            {
                client.Socket.Send(buffer);
            }
        }

        public void SendText(string txt)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(txt);
            foreach (var client in _clients)
            {
                client.Socket.Send(buffer);
            }
        }

        private void SoruYazdir()
        {
            if (Global.GenelDurum == Global.Durum.TestBitti) return;
            SoruGeriSayim();
            _cevaplar = FCevap.SelectBySoruId(_sorular[_soruIndex].SoruId);
            _ucSoru.Soru.Text = _sorular[_soruIndex].SoruText;
            for (var i = 0; i < _ucSoru.textBlocks.Length; i++)
            {
                _ucSoru.borders[i].Background = _colors[i];
                _ucSoru.textBlocks[i].Text = _cevaplar[i].CevapText;
                _ucSoru.bordersDogru[i].Text = _cevaplar[i].Dogru.ToString();
            }
            _soruIndex++;
            //for (int i = 0; i < _kullanicilar.Count; i++)
            //{
            //    _kullanicilar[i].TestId = _test.TestId;
            //    for (int j = 0; j < _sorular.Count; j++)
            //    {
            //        _kullanicilar[i].Sorular.Add(new Kullanici.SoruOzellikleri
            //        {
            //            SoruId = _sorular[_soruIndex].SoruId,
            //            Dogru= _cevaplar[i].Dogru,
            //        });
            //        _soruIndex = 0;
            //    }
            //_listener.SendObject(_kullanicilar[i]);
            //}
        }



        #region Geri Sayımlar

        private DispatcherTimer _timer;

        // Soru Yazdıra Gidiyor
        private void BaslangicGeriSayim()
        {
            // Timer başlamadan süreyi yazdırdım.
            AnimationRectangle.OpacityMask = new VisualBrush
            {
                Visual = _icons[_sure.BaslangicSure]
            };
            CountDown.Content = _sure.BaslangicSure;

            TimeSpan time;
            time = TimeSpan.FromSeconds(_sure.BaslangicSure);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                time = time.Add(TimeSpan.FromSeconds(-1));
                CountDown.Content = time.Seconds;
                if (time.Seconds < 0)
                {
                    _timer.Stop();
                    CountDownGrid.Visibility = Visibility.Hidden;
                    SonrakiSoru.Visibility = Visibility.Visible;
                    SoruYazdir();
                }
                else
                {
                    AnimationRectangle.OpacityMask = new VisualBrush
                    {
                        Visual = _icons[time.Seconds]
                    };
                    AnimationRectangle.Fill = _colors[time.Seconds];
                }
            }, Application.Current.Dispatcher);
            _timer.Start();
        }

        // Soru Yazdıra Gidiyor.
        private void SoruGoster()
        {
            TimerControl();
            SonrakiSoru.Visibility = Visibility.Hidden;
            Info.Visibility = Visibility.Hidden;

            // Skorları göster, sonra soruyu yaz
            Sorular.Children.Clear();

            UcSkorGoster();

            var time = TimeSpan.FromSeconds(_sure.SkorSure);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (time.Seconds == 0)
                {
                    _timer.Stop();
                    SonrakiSoru.Visibility = Visibility.Visible;
                    Info.Visibility = Visibility.Visible;
                    Sorular.Children.Clear();
                    Sorular.Children.Add(_ucSoru);
                    SoruYazdir();
                }
                time = time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            _timer.Start();

            //Sorular Bitti, Tüm Skorları Göster
        }

        // Doğru Göster'e Gidiyor
        private void SoruGeriSayim()
        {
            TimerControl();
            sureBitti = false;
            _ucSoru.SureProgress.Maximum = _test.Sure;
            var time = TimeSpan.FromSeconds(_test.Sure);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                _ucSoru.SureProgress.Value = time.Seconds;
                if (time.Seconds <= 0)
                {
                    _timer.Stop();
                    sureBitti = true;
                    DogruGoster();
                }
                time = time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            _timer.Start();
        }

        // Soru Göstere Gidiyor.
        private void DogruGosterWithTimer()
        {
            TimerControl();
            DogruGoster();
            TimeSpan time = TimeSpan.FromSeconds(Settings.Default.DogruSure);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (time.Seconds <= 0)
                {
                    for (int i = 0; i < _cevaplar.Count; i++)
                    {
                        _ucSoru.borders[i].Background = _colors[i];
                    }
                    SoruGoster();
                }
                time = time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            _timer.Start();
        }

        private void DogruGoster()
        {
            for (int i = 0; i < _cevaplar.Count; i++)
            {
                if (_ucSoru.bordersDogru[i].Text == "0")
                {
                    _ucSoru.borders[i].Background = Brushes.LightGray;
                }
            }
        }

        private void TimerControl()
        {
            if (_timer != null)
            {
                if (_timer.IsEnabled)
                {
                    _timer.Stop();
                }
            }
        }
        #endregion
    }
}
