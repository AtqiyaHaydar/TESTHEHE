using System;
using MySql.Data.MySqlClient;

namespace database
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;user=root;database=tubes3stima;port=3456;password=";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection to database established successfully.");

                    // Example: Querying the database 
                    string query = "SELECT * FROM biodata LIMIT 10";
                    // string query2 = "INSERT INTO biodata (NIK, nama, alamat) VALUES ('12345678905000', 'gerry', 'Jl. Jend. Sudirman No 10')";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine(string.Format("NIK: {0}, Nama: {1}", reader["NIK"], reader["Nama"]));
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("An error occurred: {0}", ex.Message));
                }
            }
        }
    }
}