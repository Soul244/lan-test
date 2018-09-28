using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTest.Models.FacadeLayer
{
    class CRUD
    {
        public static int Insert(Test test, List<Soru> sorular, List<Cevap> cevaplar)
        {
            int cevapId=0;
            int q = 0,x=0;
            int testId = FTest.Insert(test);
            if (testId >= 0)
            {
                for (int i = 0; i < sorular.Count; i++)
                {
                    sorular[i].TestId = testId;
                    sorular[i].SoruId=FSoru.Insert(sorular[i]);
                }
                for (int k = 0; k < cevaplar.Count; k++)
                {
                    cevaplar[k].TestId = testId;
                    cevaplar[k].SoruId = sorular[q].SoruId;
                    x++;
                    if (x==test.CevapSayisi)
                    {
                        x = 0;
                        q++;
                    }
                    FCevap.Insert(cevaplar[k]);
                }
            }
            return cevapId;
        }

        public static int Update(Test test, List<Soru> sorular, List<Cevap> cevaplar)
        {
            int sonuc = 0;
            FTest.Update(test);
            for (var i = 0; i < sorular.Count; i++)
            {
                FSoru.Update(sorular[i]);
            }
            for (var k = 0; k < cevaplar.Count; k++)
            {
                sonuc=FCevap.Update(cevaplar[k]);
            }
            return sonuc;
        }

        public static int Delete(Test test)
        {
            int cevapId = 0;
            var testId = FTest.Delete(test.TestId);
            if (testId >= 0)
            {
                var soruId = FSoru.DeleteAll(test.TestId);
                cevapId = FCevap.DeleteAll(test.TestId);
            }
            return cevapId;
        }

        public static(Test Test,List<Soru> Sorular, List<Cevap> Cevaplar) Select(string testAdi)
        {
            var test = FTest.Select(testAdi);
            List<Soru> sorular = new List<Soru>(FSoru.SelectAll(test.TestId));
            List<Cevap> cevaplar = new List<Cevap>(FCevap.SelectByTestId(test.TestId));
            return (test, sorular, cevaplar);
        }
    }
}
