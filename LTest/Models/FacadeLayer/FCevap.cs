using Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace LTest.Models.FacadeLayer
{
    class FCevap
    {

        public static int Insert(Cevap item)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("INSERT INTO Cevaplar(SoruId, TestId, Cevap, Dogru) VALUES(@SoruId,@TestId,@Cevap,@Dogru)", Baglanti))
                {
                    Baglanti.Open();
                    com.Parameters.AddWithValue("@SoruId", item.SoruId);
                    com.Parameters.AddWithValue("@TestId", item.TestId);
                    com.Parameters.AddWithValue("@Cevap", item.CevapText);
                    com.Parameters.AddWithValue("@Dogru", item.Dogru);
                    var result = com.ExecuteNonQuery();
                    Baglanti.Close();
                    return result;
                }
            }
        }

        public static int Update(Cevap item)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("UPDATE Cevaplar SET Cevap=@Cevap,Dogru=@Dogru WHERE CevapId=@CevapId ", Baglanti))
                {
                    Baglanti.Open();
                    com.Parameters.AddWithValue("@CevapId", item.CevapId);
                    com.Parameters.AddWithValue("@Cevap", item.CevapText);
                    com.Parameters.AddWithValue("@Dogru", item.Dogru);
                    var result = com.ExecuteNonQuery();
                    Baglanti.Close();
                    return result;
                }
            }
        }

        public static int DeleteAll(int testId)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("DELETE FROM Cevaplar WHERE TestId=@TestId", Baglanti))
                {
                    Baglanti.Open();
                    com.Parameters.AddWithValue("TestId", testId);
                    var result= com.ExecuteNonQuery();
                    Baglanti.Close();
                    return result;
                };
            }
        }

        public static List<Cevap> SelectBySoruId(int soruId)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                Baglanti.Open();
                List<Cevap> itemList = null;
                using (SQLiteCommand com = new SQLiteCommand("SELECT * FROM Cevaplar WHERE SoruId=@SoruId", Baglanti))
                {
                    com.Parameters.AddWithValue("@SoruId", soruId);
                    using (var rdr = com.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            itemList = new List<Cevap>();
                            while (rdr.Read())
                            {
                                Cevap item = new Cevap
                                {
                                    CevapId = Convert.ToInt16(rdr["CevapId"]),
                                    SoruId = Convert.ToInt16(rdr["SoruId"]),
                                    TestId = Convert.ToInt16(rdr["TestId"]),
                                    CevapText = rdr["Cevap"].ToString(),
                                    Dogru = Convert.ToInt16(rdr["Dogru"])
                                };
                                itemList.Add(item);
                            }
                        }
                    }
                }
                Baglanti.Close();
                return itemList;
            }
        }

        public static List<Cevap> SelectByTestId(int testId)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                Baglanti.Open();
                List<Cevap> itemList = null;
                using (var com = new SQLiteCommand("SELECT * FROM Cevaplar WHERE TestId=@TestId", Baglanti))
                {
                    com.Parameters.AddWithValue("@TestId", testId);
                    using (var rdr = com.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            itemList = new List<Cevap>();
                            while (rdr.Read())
                            {
                                Cevap item = new Cevap
                                {
                                    CevapId = Convert.ToInt16(rdr["CevapId"]),
                                    SoruId = Convert.ToInt16(rdr["SoruId"]),
                                    TestId = Convert.ToInt16(rdr["TestId"]),
                                    CevapText = rdr["Cevap"].ToString(),
                                    Dogru = Convert.ToInt16(rdr["Dogru"])
                                };
                                itemList.Add(item);
                            }
                        }
                    }
                }
                Baglanti.Close();
                return itemList;
            }
        }
    }
}
