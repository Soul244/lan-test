using Entity;
using System.Collections.Generic;
using System.Data.SQLite;

namespace LTest.Models.FacadeLayer
{
    class FClientKullanici
    {
        public static int Insert(ClientKullanici item, ClientKullanici.SoruOzellikleri soruOzellikleri)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("INSERT INTO ClientKullanicilar(KullaniciAdi,Puan,TestId) VALUES(@KullaniciAdi,@Puan, @TestId)", Baglanti))
                {
                    Baglanti.Open();
                    com.Parameters.AddWithValue("KullaniciAdi", item.KullaniciAdi);
                    com.Parameters.AddWithValue("KullaniciAdi", soruOzellikleri.Puan);
                    var result = com.ExecuteNonQuery();
                    Baglanti.Close();
                    return result;
                }
            }
        }

        public static int Delete(int kullaniciId)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("DELETE FROM Kullanicilar WHERE KullaniciId=@KullaniciId"))
                {
                    Baglanti.Open();
                    com.Parameters.AddWithValue("KullaniciId", kullaniciId);
                    var result = com.ExecuteNonQuery();
                    Baglanti.Close();
                    return result;
                }
            }

        }

        //public static int Update(Kullanici item)
        //{
        //    SQLiteCommand com = new SQLiteCommand("UPDATE Testler SET KullaniciAdi=@KullaniciAdi WHERE KullaniciId=@KullaniciId ", DatabaseManager.Baglanti);
        //    com.Parameters.AddWithValue("KullaniciId", item.KullaniciId);
        //    com.Parameters.AddWithValue("KullaniciAdi", item.KullaniciAdi);
        //    return com.ExecuteNonQuery();
        //}

        public static ClientKullanici Select(int kullaniciId)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("SELECT * FROM ClientKullanicilar WHERE KullaniciId=@KullaniciId ", Baglanti))
                {
                    Baglanti.Open();
                    ClientKullanici item = null;
                    com.Parameters.AddWithValue("KullaniciId", kullaniciId);
                    SQLiteDataReader rdr = com.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            item = new ClientKullanici
                            {
                                KullaniciAdi = (string)rdr["KullaniciAdi"],

                            };
                        }
                    }
                    Baglanti.Close();
                    return item;
                }
            }
        }

        public static List<ClientKullanici> SelectAll()
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("SELECT * FROM ClientKullanicilar", Baglanti))
                {
                    Baglanti.Open();
                    List<ClientKullanici> itemList = null;
                    SQLiteDataReader rdr = com.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        itemList = new List<ClientKullanici>();
                        while (rdr.Read())
                        {
                            ClientKullanici item = new ClientKullanici
                            {
                                KullaniciAdi = (string)rdr["KullaniciAdi"],
                            };
                            itemList.Add(item);
                        }
                    }
                    Baglanti.Close();
                    return itemList;
                }
            }
        }
    }
}
