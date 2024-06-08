using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using System.Collections.Generic;
using System.IO;
using Faker;
using Bogus;

class Program
{
    static void Main(string[] args)
    {

        // string query = "SELECT * FROM sidik_jari LIMIT 10";
        string dbPath = "database.db";

        // Connection string for SQLite
        string connectionString = $"Data Source={dbPath};";

        string createTableQuery = @"
            CREATE TABLE sidik_jari (
            berkas_citra TEXT,
            nama TEXT
            );
        ";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        // Buat data palsu
        List<string> fakeSidikJari = GenerateFakeSidikJariData(6000);
        List<string> filepath = GetFilePaths("./assets");

        // Masukkan data palsu ke dalam database
        InsertDataIntoDatabase(connectionString, filepath, fakeSidikJari);

        // Query database untuk melihat data yang dimasukkan
        QueryDatabase(connectionString, "SELECT * FROM sidik_jari LIMIT 10");

    }
    
    static List<string> GenerateFakeSidikJariData(int numberOfRecords)
    {
        var faker = new Bogus.Faker("id_ID");
        var fakeDataList = new List<string>();

        for (int i = 0; i < numberOfRecords; i++)
        {
            var nama = faker.Name.FullName();
            // Pastikan nama tidak lebih panjang dari 10 karakter
            if (nama.Length > 10)
            {
                nama = nama.Substring(0, 10); // Potong nama jika lebih dari 10 karakter
            }
            fakeDataList.Add(nama);
        }

        return fakeDataList;
    }

   
     static void QueryDatabase(string connectionString, string query)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(string.Format("berkas_citra: {0}, nama: {1}",  reader["berkas_citra"], reader["nama"]));
                    }
                }
            }
        }
    }

   
    // Function to get all file paths in a folder
        static List<string> GetFilePaths(string folderPath)
        {
            List<string> filePaths = new List<string>();

            // Get all files in the folder
            string[] files = Directory.GetFiles(folderPath);

            // Add each file path to the list
            foreach (string file in files)
            {
                filePaths.Add(file);
                // Console.WriteLine(file);
            }

            return filePaths;
        }

        // Function to insert file paths into the database using looping
        static void InsertDataIntoDatabase(string connectionString, List<string> filePaths, List<string> fakeSidikJari)
    {
        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            try
            {
                // Open the connection
                conn.Open();
                Console.WriteLine("Connection to database established successfully.");

                for (int i = 0; i < filePaths.Count && i < fakeSidikJari.Count; i++)
                {
                    string filePath = filePaths[i];
                    string nama = fakeSidikJari[i];

                    // Prepare the SQL query
                    string query = "INSERT INTO sidik_jari (berkas_citra, nama) VALUES (@berkas_citra, @nama)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Adding parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@berkas_citra", filePath);
                        cmd.Parameters.AddWithValue("@nama", nama);

                        // Execute the query
                        int result = cmd.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (result > 0)
                        {
                            Console.WriteLine($"File '{Path.GetFileName(filePath)}' with name '{nama}' has been inserted successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to insert file '{Path.GetFileName(filePath)}' with name '{nama}'.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close the connection
                conn.Close();
                Console.WriteLine("Connection to database closed.");
            }
        }
    }
}