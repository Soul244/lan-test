namespace Entity
{
    [System.Serializable]

    public class Rapor
    {
        public int RaporId { get; set; }

        public int TestId { get; set; }

        public int KullaniciId { get; set; }

        public int DogruSayisi { get; set; }

        public int YanlisSayisi { get; set; }
    }
}
