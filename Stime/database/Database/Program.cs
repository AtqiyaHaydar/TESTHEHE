using System;
using System.Data;
using MySql.Data.MySqlClient;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "server=localhost;user=root;database=tubes3stima;port=3456;password=2211";
        string query = "SELECT * FROM biodata LIMIT 10";
        string query2 = "SELECT * FROM biodata WHERE NIK = '1234567890'";
        string query3 = "SELECT * FROM biodata WHERE Nama = 'John Doe'";
        QueryDatabase(connectionString, query3);
    }

    static void QueryDatabase(string connectionString, string query)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection to database established successfully.");

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