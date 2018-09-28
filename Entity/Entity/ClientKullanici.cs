using System.Collections.Generic;

namespace Entity
{
    [System.Serializable]
    public class ClientKullanici
    {
        public string KullaniciAdi { get; set; }
        public float TotalPuan { get; set; }
        public List<SoruOzellikleri> Sorular { get; set; }
        [System.Serializable]
        public class SoruOzellikleri
        {
            public int TestId { get; set; }
            public int SoruId { get; set; }
            public int Cevap { get; set; }
            public int Dogru { get; set; }
            public float CevapSuresi { get; set; }
            public float Puan { get; set; }

        }
    }
}
