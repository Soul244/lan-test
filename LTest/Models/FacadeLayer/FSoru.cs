using Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace LTest.Models.FacadeLayer
{
    class FSoru
    {
        public static int Insert(Soru item)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                //SQLiteCommand com = new SQLiteCommand("INSERT INTO Sorular(TestId, Soru) VALUES(@TestId, @Soru)", Baglanti);
                Baglanti.Open();
                SQLiteTransaction transaction = null;
                transaction = Baglanti.BeginTransaction();

                using (SQLiteCommand com = new SQLiteCommand("INSERT INTO Sorular(TestId, Soru) VALUES(@TestId, @Soru)", Baglanti))
                {
                    com.Parameters.AddWithValue("@TestId", item.TestId);
                    com.Parameters.AddWithValue("@Soru", item.SoruText);
                    com.ExecuteScalar();
                };

                int result = Convert.ToInt32(Baglanti.LastInsertRowId);
                transaction.Commit();

                Baglanti.Close();
                return result;
            }
        }


    //                    using (SQLiteCommand com = new SQLiteCommand("INSERT INTO Sorular(TestId, Soru) VALUES(@TestId, @Soru) Select last_insert_rowid()", Baglanti))
    //            {
    //                Baglanti.Open();
    //                com.Parameters.AddWithValue("@TestId", item.TestId);
    //                com.Parameters.AddWithValue("@Soru", item.SoruText);

    //                int result = Convert.ToInt32(com.ExecuteScalar());

    //Baglanti.Close();
    //                return result;
    //            }

        public static int Update(Soru item)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("UPDATE Sorular SET Soru=@Soru WHERE SoruId=@SoruId ", Baglanti))
                {
                    Baglanti.Open();

                    com.Parameters.AddWithValue("SoruId", item.SoruId);
                    com.Parameters.AddWithValue("Soru", item.SoruText);

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
                using (SQLiteCommand com = new SQLiteCommand("DELETE FROM Sorular WHERE TestId=@TestId", Baglanti))
                {
                    Baglanti.Open();

                    com.Parameters.AddWithValue("TestId", testId);

                    var result = com.ExecuteNonQuery();
                    Baglanti.Close();
                    return result;
                }
            }

        }

        public static List<Soru> SelectAll(int testId)
        {
            using (SQLiteConnection Baglanti = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                using (SQLiteCommand com = new SQLiteCommand("SELECT * FROM Sorular WHERE TestId=@TestId", Baglanti))
                {
                    Baglanti.Open();
                    List<Soru> itemList = null;
                    com.Parameters.AddWithValue("@TestId", testId);
                    using (SQLiteDataReader rdr = com.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            itemList = new List<Soru>();
                            while (rdr.Read())
                            {
                                var item = new Soru
                                {
                                    TestId = Convert.ToInt16(rdr["TestId"]),
                                    SoruId = Convert.ToInt16(rdr["SoruId"]),
                                    SoruText = rdr["Soru"].ToString()
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
