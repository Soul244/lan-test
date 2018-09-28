using System.Collections.Generic;


namespace Entity
{
    [System.Serializable]
    public class TotalData
    {
        public Sure Sure { get; set; }
        public Test Test { get; set; }
        public List<Soru> Sorular { get; set; }
        public List<Cevap> Cevaplar { get; set; }
    }
}
