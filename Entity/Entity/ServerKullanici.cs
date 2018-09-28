namespace Entity
{
    [System.Serializable]
    public class ServerKullanici
    {
        public int KullaniciId { get; set; }

        public string KullaniciAdi { get; set; }

        public string Sifre { get; set; }

        public string Mail { get; set; }
    }
}
