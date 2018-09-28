using Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace LTest.Models.FacadeLayer
{
    class FTest
    {
        public static int Insert(Test item)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                Baglanti.Open();
                SQLiteTransaction transaction = null;
                transaction = Baglanti.BeginTransaction();

                using (SQLiteCommand com = new SQLiteCommand("INSERT INTO Testler(Sure, SoruSayisi, CevapSayisi, TestAdi, KullaniciId) VALUES(@Sure, @SoruSayisi, @CevapSayisi, @TestAdi, @KullaniciId); Select last_insert_rowid()", Baglanti))
                {

                    com.Parameters.AddWithValue("@Sure", item.Sure);
                    com.Parameters.AddWithValue("@SoruSayisi", item.SoruSayisi);
                    com.Parameters.AddWithValue("@CevapSayisi", item.CevapSayisi);
                    com.Parameters.AddWithValue("@TestAdi", item.TestAdi);
                    com.Parameters.AddWithValue("@KullaniciId", item.KullaniciId);
                    com.ExecuteScalar();

                    int result = Convert.ToInt32(Baglanti.LastInsertRowId);
                    transaction.Commit();

                    Baglanti.Close();
                    return result;
                }
            }
        }

        public static int Update(Test item)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("UPDATE Testler SET Sure=@Sure,SoruSayisi=@SoruSayisi,CevapSayisi=@CevapSayisi WHERE TestId=@TestId", Baglanti))
                {
                    Baglanti.Open();

                    com.Parameters.AddWithValue("TestId", item.TestId);
                    com.Parameters.AddWithValue("Sure", item.Sure);
                    com.Parameters.AddWithValue("SoruSayisi", item.SoruSayisi);
                    com.Parameters.AddWithValue("CevapSayisi", item.CevapSayisi);
                    com.Parameters.AddWithValue("TestAdi", item.TestAdi);

                    var result = com.ExecuteNonQuery();
                    Baglanti.Close();
                    return result;
                }
            }
        }

        public static int Delete(int testId)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("DELETE FROM Testler WHERE TestId=@TestId", Baglanti))
                {
                    Baglanti.Open();

                    com.Parameters.AddWithValue("TestId", testId);

                    var result = com.ExecuteNonQuery();
                    Baglanti.Close();
                    return result;
                }
            }

        }

        public static Test Select(string testAdi)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("SELECT * FROM Testler WHERE TestAdi=@TestAdi", Baglanti))
                {
                    Baglanti.Open();
                    Test item = null;
                    com.Parameters.AddWithValue("@TestAdi", testAdi);
                    using (SQLiteDataReader rdr = com.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                item = new Test
                                {
                                    TestId = Convert.ToInt16(rdr["TestId"]),
                                    Sure = Convert.ToInt16(rdr["Sure"]),
                                    SoruSayisi = Convert.ToInt16(rdr["SoruSayisi"]),
                                    CevapSayisi = Convert.ToInt16(rdr["CevapSayisi"]),
                                    TestAdi = rdr["TestAdi"].ToString()
                                };
                            }
                        }
                        Baglanti.Close();
                        return item;
                    }
                }
            }
        }

        public static List<Test> SelectAll(int KullaniciId)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("SELECT * FROM Testler WHERE KullaniciId=@KullaniciId", Baglanti))
                {
                    Baglanti.Open();
                    List<Test> itemList = null;
                    com.Parameters.AddWithValue("KullaniciId", KullaniciId);
                    using (SQLiteDataReader rdr = com.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            itemList = new List<Test>();
                            while (rdr.Read())
                            {
                                Test item = new Test
                                {
                                    TestId = Convert.ToInt16(rdr["TestId"]),
                                    Sure = Convert.ToInt16(rdr["Sure"]),
                                    SoruSayisi = Convert.ToInt16(rdr["SoruSayisi"]),
                                    CevapSayisi = Convert.ToInt16(rdr["CevapSayisi"]),
                                    TestAdi = rdr["TestAdi"].ToString(),
                                    KullaniciId = Convert.ToInt16(rdr["KullaniciId"])
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
}
