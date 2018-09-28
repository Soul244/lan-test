using Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTest.Models.FacadeLayer
{
    class FServerKullanici
    {
        public static int Insert(ServerKullanici item)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("INSERT INTO ServerKullanicilar(KullaniciAdi,Sifre,Mail) VALUES(@KullaniciAdi,@Sifre, @Mail)", Baglanti))
                {
                    Baglanti.Open();
                    com.Parameters.AddWithValue("KullaniciAdi", item.KullaniciAdi);
                    com.Parameters.AddWithValue("Sifre", item.Sifre);
                    com.Parameters.AddWithValue("Mail", item.Mail);
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
                using (SQLiteCommand com = new SQLiteCommand("DELETE FROM ServerKullanicilar WHERE KullaniciId=@KullaniciId"))
                {
                    Baglanti.Open();

                    com.Parameters.AddWithValue("KullaniciId", kullaniciId);

                    var result = com.ExecuteNonQuery();
                    Baglanti.Close();
                    return result;
                }
            }
        }

        public static int Update(ServerKullanici item)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("UPDATE ServerKullanicilar SET Sifre=@Sifre WHERE KullaniciId=@KullaniciId ", Baglanti))
                {
                    Baglanti.Open();

                    com.Parameters.AddWithValue("KullaniciId", item.KullaniciId);
                    com.Parameters.AddWithValue("Sifre", item.Sifre);

                    var result = com.ExecuteNonQuery();
                    Baglanti.Close();
                    return result;
                }
            }

        }

        public static ServerKullanici Select(ServerKullanici serverKullanici)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("SELECT * FROM ServerKullanicilar WHERE KullaniciAdi=@KullaniciAdi AND Sifre=@Sifre ", Baglanti))
                {
                    Baglanti.Open();
                    ServerKullanici item = null;
                    com.Parameters.AddWithValue("KullaniciAdi", serverKullanici.KullaniciAdi);
                    com.Parameters.AddWithValue("Sifre", serverKullanici.Sifre);
                    using (SQLiteDataReader rdr = com.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                item = new ServerKullanici
                                {
                                    KullaniciId = Convert.ToInt16(rdr["KullaniciId"]),
                                    KullaniciAdi = (string)rdr["KullaniciAdi"],
                                    Sifre = (string)rdr["Sifre"],
                                    Mail = (string)rdr["Mail"]
                                };
                            }
                        }
                    }
                    Baglanti.Close();
                    return item;
                }
            }
        }

        //public static List<Kullanici> SelectAll()
        //{
        //    List<Kullanici> itemList = null;
        //    SQLiteCommand com = new SQLiteCommand("SELECT * FROM Kullanicilar", DatabaseManager.Baglanti);
        //    SQLiteDataReader rdr = com.ExecuteReader();
        //    if (rdr.HasRows)
        //    {
        //        itemList = new List<Kullanici>();
        //        while (rdr.Read())
        //        {
        //            Kullanici item = new Kullanici
        //            {
        //                KullaniciAdi = (string)rdr["KullaniciAdi"],
        //            };
        //            itemList.Add(item);
        //        }
        //    }
        //    return itemList;
        //}
    }
}
