using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SQLite;

class Program
{
    static void Main(string[] args)
    {

        string query = "SELECT * FROM sidik_jari LIMIT 10";
        string dbPath = "mydatabase.db";

        // Connection string for SQLite
        string connectionString = $"Data Source={dbPath};";

        QueryDatabase(connectionString, query);

        // // Create a new database file
        // SQLiteConnection.CreateFile(dbPath);

        // Console.WriteLine("Database file created successfully.");

        // // Create a connection to the database
        // using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        // {
        //     conn.Open();
        //     Console.WriteLine("Connection to database established successfully.");

        //     // Example: Create a table
        //     string createTableQuery = "CREATE TABLE sidik_jari (berkas_citra TEXT, nama VARCHAR(100))";
        //     using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, conn))
        //     {
        //         cmd.ExecuteNonQuery();
        //         Console.WriteLine("Table 'sidik_jari' created successfully.");
        //     }

        //     // Example: Insert data
        //     // Path to the assets folder
        //     string assetsFolderPath = "./assets"; // Ubah ini dengan path yang sesuai

        //     // List of file paths to insert
        //     List<string> filePaths = GetFilePaths(assetsFolderPath);

        //     // Call the function to insert data
        //     InsertFilePathsIntoDatabase(connectionString, filePaths);

        //     conn.Close();
        //     Console.WriteLine("Connection to database closed.");
        // }
    }
    

    static void QueryDatabase(string connectionString, string query)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection to database established successfully.");

                SQLiteCommand command = new SQLiteCommand(query, connection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Console.WriteLine(string.Format("NIK: {0}, Nama: {1}", reader["NIK"], reader["Nama"]));
                    Console.WriteLine(string.Format("berkas_citra: {0}", reader["berkas_citra"]));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("An error occurred: {0}", ex.Message));
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
        static void InsertFilePathsIntoDatabase(string connectionString, List<string> filePaths)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    conn.Open();
                    Console.WriteLine("Connection to database established successfully.");

                    foreach (var filePath in filePaths)
                    {
                        // Extract file name from the file path
                        string fileName = Path.GetFileName(filePath);

                        // Prepare the SQL query
                        string query = "INSERT INTO sidik_jari (berkas_citra) VALUES (@berkas_citra)";

                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            // Adding parameters to prevent SQL injection
                            cmd.Parameters.AddWithValue("@berkas_citra", filePath);
                            // cmd.Parameters.AddWithValue("@nama", fileName);

                            // Execute the query
                            int result = cmd.ExecuteNonQuery();

                            // Check if the insertion was successful
                            if (result > 0)
                            {
                                Console.WriteLine($"File '{fileName}' has been inserted successfully.");
                            }
                            else
                            {
                                Console.WriteLine($"Failed to insert file '{fileName}'.");
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