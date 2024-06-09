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

        // // string query = "SELECT * FROM sidik_jari LIMIT 10";
        // string dbPath = "database.db";

        // // Connection string for SQLite
        // string connectionString = $"Data Source={dbPath};";

        // string createTableQuery = @"
        //     CREATE TABLE sidik_jari (
        //     berkas_citra TEXT,
        //     nama TEXT
        //     );
        // ";

        // using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        // {
        //     connection.Open();

        //     using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
        //     {
        //         command.ExecuteNonQuery();
        //     }

        //     connection.Close();
        // }

        // Buat data palsu
        // List<string> fakeSidikJari = GenerateFakeSidikJariData(6000);
        // List<string> filepath = GetFilePaths("./assets");
        List<string> fakeNIK = GenerateFakeNIKData(10);
        List<string> fakeTempatLahir = GenerateFakeTempatLahirData(10);
        List<string> fakeTanggalLahir = GenerateFakeTanggalLahirData(10);
        List<string> fakeGender = GenerateFakeGenderData(10);
        List<string> fakeBloodType = GenerateFakeBloodTypeData(10);
        List<string> fakeAlamat = GenerateFakeAlamatData(10);
        List<string> fakeAgama = GenerateFakeAgamaData(10);
        List<string> fakeStatusPerkawinan = GenerateFakeStatusPerkawinanData(10);
        List<string> fakePekerjaan = GenerateFakePekerjaanData(10);
        List<string> fakeKewarganegaraan = GenerateFakeKewarganegaraanData(10);

        // // Masukkan data palsu ke dalam database
        // InsertDataIntoDatabase(connectionString, filepath, fakeSidikJari);

        // // Query database untuk melihat data yang dimasukkan
        // QueryDatabase(connectionString, "SELECT * FROM sidik_jari LIMIT 10");

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

    static List<string> GenerateFakeNIKData(int numberOfRecords)
    {
        var faker = new Bogus.Faker("id_ID");
        var fakeDataList = new List<string>();

        for (int i = 0; i < numberOfRecords; i++)
        {
            // Generate random NIK with format: YYMMDDXXXXC
            // YYMMDD: Tanggal lahir (tahun bulan hari)
            // XXXX: Nomor urut
            // C: Digit verifikasi
            var tanggalLahir = faker.Date.Past(18, DateTime.Now.AddYears(-12)); // Generate random date within the last 12 years
            var nomorUrut = faker.Random.Number(1000, 9999).ToString(); // Generate random 4-digit number
            var digitVerifikasi = faker.Random.Number(0, 9).ToString(); // Generate random single-digit number

            var nik = $"{tanggalLahir:yyMMdd}{nomorUrut}{digitVerifikasi}"; // Format NIK

            fakeDataList.Add(nik);
        }

        return fakeDataList;
    }

    static List<string> GenerateFakeTempatLahirData(int numberOfRecords)
    {
        var faker = new Bogus.Faker("id_ID");
        var fakeDataList = new List<string>();

        for (int i = 0; i < numberOfRecords; i++)
        {
            var tempatLahir = faker.Address.City(); // Generate random city name
            fakeDataList.Add(tempatLahir);
        }

        return fakeDataList;
    }

    static List<string> GenerateFakeTanggalLahirData(int numberOfRecords)
    {
        var faker = new Bogus.Faker("id_ID");
        var fakeDataList = new List<string>();

        for (int i = 0; i < numberOfRecords; i++)
        {
            // Generate random date of birth within a reasonable range
            var tanggalLahirWithTime = faker.Date.Between(DateTime.Now.AddYears(-80), DateTime.Now.AddYears(-18)); // Generate date of birth between 18 and 80 years ago
            var tanggalLahir = tanggalLahirWithTime.ToString("yyyy-MM-dd"); // Format date to yyyy-MM-dd
            fakeDataList.Add(tanggalLahir);
        }

        return fakeDataList;
    }

    static List<string> GenerateFakeGenderData(int numberOfRecords)
    {
        var faker = new Bogus.Faker("id_ID");
        var fakeGenderList = new List<string>();

        for (int i = 0; i < numberOfRecords; i++)
        {
            // Generate random gender ("Laki-Laki" or "Perempuan")
            var gender = faker.Random.Bool() ? "Laki-Laki" : "Perempuan";
            fakeGenderList.Add(gender);
        }

        return fakeGenderList;
    }

    static List<string> GenerateFakeBloodTypeData(int numberOfRecords)
    {
        var faker = new Bogus.Faker("id_ID");
        var fakeBloodTypeList = new List<string>();
        var bloodTypes = new List<string> { "A", "AB", "B", "O" };

        for (int i = 0; i < numberOfRecords; i++)
        {
            // Select a random blood type from the list
            var bloodType = faker.Random.ListItem(bloodTypes);
            fakeBloodTypeList.Add(bloodType);
        }

        return fakeBloodTypeList;
    }

    static List<string> GenerateFakeAlamatData(int numberOfRecords)
    {
        var faker = new Bogus.Faker("id_ID");
        var fakeDataList = new List<string>();

        for (int i = 0; i < numberOfRecords; i++)
        {
            // Generate specific parts of an Indonesian address
            var jalan = faker.Address.StreetName(); // Nama jalan
            var nomorRumah = faker.Address.BuildingNumber(); // Nomor rumah
            var kota = faker.Address.City(); // Kota
            var provinsi = faker.Address.State(); // Provinsi
            var kodePos = faker.Address.ZipCode("#####"); // Kode pos dengan format lima digit

            // Combine the parts into a full Indonesian address format
            var alamat = $"{jalan} No. {nomorRumah}, {kota}, {provinsi}, {kodePos}";
            fakeDataList.Add(alamat);
        }

        return fakeDataList;
    }

    static List<string> GenerateFakeAgamaData(int numberOfRecords)
    {
        var faker = new Bogus.Faker("id_ID");
        var fakeDataList = new List<string>();

        // List of religions in Indonesia
        var agamaOptions = new[] { "Islam", "Kristen", "Katolik", "Hindu", "Buddha", "Khonghucu" };

        for (int i = 0; i < numberOfRecords; i++)
        {
            // Generate a random religion from the list
            var agama = faker.PickRandom(agamaOptions);
            fakeDataList.Add(agama);
        }

        return fakeDataList;
    }

    static List<string> GenerateFakeStatusPerkawinanData(int numberOfRecords)
    {
        var faker = new Bogus.Faker("id_ID");
        var fakeDataList = new List<string>();

        // List of marital statuses
        var statusPerkawinanOptions = new[] { "Belum Menikah", "Menikah", "Cerai" };

        for (int i = 0; i < numberOfRecords; i++)
        {
            // Generate a random marital status from the list
            var statusPerkawinan = faker.PickRandom(statusPerkawinanOptions);
            fakeDataList.Add(statusPerkawinan);
        }

        return fakeDataList;
    }

    static List<string> GenerateFakePekerjaanData(int numberOfRecords)
    {
        var faker = new Bogus.Faker("id_ID");
        var fakeDataList = new List<string>();

        // List of job statuses
        var pekerjaanOptions = new[]
        {
            "Belum Bekerja",
            "Mengurus Rumah Tangga",
            "Pelajar/Mahasiswa",
            "Pensiunan",
            "Pegawai Negeri Sipil",
            "Tentara Nasional Indonesia",
            "Kepolisian RI",
            "Pegawai Swasta"
        };

        for (int i = 0; i < numberOfRecords; i++)
        {
            // Generate a random job status from the list
            var pekerjaan = faker.PickRandom(pekerjaanOptions);
            fakeDataList.Add(pekerjaan);
        }

        return fakeDataList;
    }

    static List<string> GenerateFakeKewarganegaraanData(int numberOfRecords)
    {
        var faker = new Bogus.Faker("id_ID");
        var fakeDataList = new List<string>();

        // List of citizenship statuses
        var kewarganegaraanOptions = new[] { "WNI", "WNA" };

        for (int i = 0; i < numberOfRecords; i++)
        {
            // Generate a random citizenship status from the list
            var kewarganegaraan = faker.PickRandom(kewarganegaraanOptions);
            fakeDataList.Add(kewarganegaraan);
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