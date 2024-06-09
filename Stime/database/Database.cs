using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using System.Collections.Generic;
using System.IO;
using Faker;
using Bogus;

class Database
{
    static void Main(string[] args)
    {
        string dbPath = "database.db";
        string connectionString = $"Data Source={dbPath};";

        // Membuat tabel sidik_jari
        // string createTableQuery = @"
        //     CREATE TABLE sidik_jari (
        //     berkas_citra TEXT,
        //     nama TEXT
        //     );
        // ";

        // Membuat tabel biodata
        // string createTableBiodataQuery = @"
        //     CREATE TABLE biodata (
        //     nik varchar(16) PRIMARY KEY NOT NULL,
        //     nama varchar(100) DEFAULT NULL,
        //     tempat_lahir varchar(50) DEFAULT NULL,
        //     tanggal_lahir date DEFAULT NULL,
        //     jenis_kelamin TEXT DEFAULT NULL,
        //     golongan_darah varchar(5) DEFAULT NULL,
        //     alamat varchar(255) DEFAULT NULL,
        //     agama varchar(50) DEFAULT NULL,
        //     status_perkawinan TEXT DEFAULT NULL,
        //     pekerjaan varchar(100) DEFAULT NULL,
        //     kewarganegaraan varchar(50) DEFAULT NULL
        //     );
        // ";

        // using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        // {
        //     connection.Open();

        //     using (SQLiteCommand command = new SQLiteCommand(createTableBiodataQuery, connection))
        //     {
        //         command.ExecuteNonQuery();
        //     }

        //     connection.Close();
        // }

        // Membuat data Faker
        // List<string> fakeSidikJari = GenerateFakeNamaData(6000);
        // List<string> filepath = GetFilePathsData("./assets");
        // List<string> fakeNIK = GenerateFakeNIKData(6000);
        // List<string> fakeTempatLahir = GenerateFakeTempatLahirData(6000);
        // List<DateTime> fakeTanggalLahir = GenerateFakeTanggalLahirData(6000);
        // List<string> fakeGender = GenerateFakeGenderData(6000);
        // List<string> fakeBloodType = GenerateFakeBloodTypeData(6000);
        // List<string> fakeAlamat = GenerateFakeAlamatData(6000);
        // List<string> fakeAgama = GenerateFakeAgamaData(6000);
        // List<string> fakeStatusPerkawinan = GenerateFakeStatusPerkawinanData(6000);
        // List<string> fakePekerjaan = GenerateFakePekerjaanData(6000);
        // List<string> fakeKewarganegaraan = GenerateFakeKewarganegaraanData(6000);

        // List<string> namaFromSidikJari = GetNamaFromSidikJari(connectionString);

        // List<string> namaRegexed = new List<string>();
        // foreach (string nama in namaFromSidikJari)
        // {
        //     namaRegexed.Add(RegexMatcher.GenerateAlayString(nama));
        // }

        // Insert faker ke dalam tabel biodata
        // InsertBiodataIntoDatabase(connectionString, fakeNIK, namaRegexed, fakeTempatLahir, fakeTanggalLahir, fakeGender, fakeBloodType, fakeAlamat, fakeAgama, fakeStatusPerkawinan, fakePekerjaan, fakeKewarganegaraan);
        // Insert faker ke dalam tabel sidik_jari
        // InsertDataSidikJariIntoDatabase(connectionString, filepath, fakeSidikJari);

        // Query database untuk menampilkan data pada tabel biodata
        // ShowBiodataTable(connectionString);

        // Query database untuk menampilkan data pada tabel sidik_jari
        // ShowSidikJariTable(connectionString, "SELECT * FROM sidik_jari LIMIT 10");
    }
    
    static List<string> GetNamaFromSidikJari(string connectionString)
    {
        List<string> namaList = new List<string>();

        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT nama FROM sidik_jari";

            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Pastikan nama tidak null sebelum menambahkannya ke list
                        if (!reader.IsDBNull(0))
                        {
                            string nama = reader.GetString(0);
                            namaList.Add(nama);
                        }
                    }
                }
            }
        }

        return namaList;
    }
    static List<string> GenerateFakeNamaData(int numberOfRecords)
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

    // Function to get all file paths in a folder
    static List<string> GetFilePathsData(string folderPath)
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

    static List<DateTime> GenerateFakeTanggalLahirData(int numberOfRecords)
    {
        var faker = new Bogus.Faker("id_ID");
        var fakeDataList = new List<DateTime>();

        for (int i = 0; i < numberOfRecords; i++)
        {
            // Generate random date of birth within a reasonable range
            DateTime randomDate = faker.Date.Between(DateTime.Now.AddYears(-80), DateTime.Now.AddYears(-18)); // Generate random date within the last 80 years
            // var tanggalLahir = faker.Date.Between(DateTime.Now.AddYears(-80), DateTime.Now.AddYears(-18)).Date; // Only keep the date part
            fakeDataList.Add(randomDate);
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


    static void ShowSidikJariTable(string connectionString, string query)
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

    static void ShowBiodataTable(string connectionString)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM biodata";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("NIK: {0}, Nama: {1}, Tempat Lahir: {2}, Tanggal Lahir: {3}, Jenis Kelamin: {4}, Golongan Darah: {5}, Alamat: {6}, Agama: {7}, Status Perkawinan: {8}, Pekerjaan: {9}, Kewarganegaraan: {10}",
                            reader["nik"], 
                            reader["nama"], 
                            reader["tempat_lahir"], 
                            reader["tanggal_lahir"], 
                            reader["jenis_kelamin"], 
                            reader["golongan_darah"], 
                            reader["alamat"], 
                            reader["agama"], 
                            reader["status_perkawinan"], 
                            reader["pekerjaan"], 
                            reader["kewarganegaraan"]);
                    }
                }
            }
        }
    }

    // Function to insert file paths into the database using looping
    static void InsertDataSidikJariIntoDatabase(string connectionString, List<string> filePaths, List<string> fakeSidikJari)
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

    static void InsertBiodataIntoDatabase(string connectionString, List<string> fakeNIK, List<string> namaRegexed, List<string> fakeTempatLahir, List<DateTime> fakeTanggalLahir, List<string> fakeGender, List<string> fakeBloodType, List<string> fakeAlamat, List<string> fakeAgama, List<string> fakeStatusPerkawinan, List<string> fakePekerjaan, List<string> fakeKewarganegaraan)
    {
        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            try
            {
                // Open the connection
                conn.Open();
                Console.WriteLine("Connection to database established successfully.");

                // Find the minimum length of all lists
                int minCount = Math.Min(fakeNIK.Count, 
                                Math.Min(namaRegexed.Count, 
                                Math.Min(fakeTempatLahir.Count, 
                                Math.Min(fakeTanggalLahir.Count, 
                                Math.Min(fakeGender.Count, 
                                Math.Min(fakeBloodType.Count, 
                                Math.Min(fakeAlamat.Count, 
                                Math.Min(fakeAgama.Count, 
                                Math.Min(fakeStatusPerkawinan.Count, 
                                Math.Min(fakePekerjaan.Count, fakeKewarganegaraan.Count))))))))));

                for (int i = 0; i < minCount; i++)
                {
                    string NIK = fakeNIK[i];
                    string Regexed = namaRegexed[i];
                    string TempatLahir = fakeTempatLahir[i];
                    DateTime TanggalLahir = fakeTanggalLahir[i];
                    string Gender = fakeGender[i];
                    string BloodType = fakeBloodType[i];
                    string Alamat = fakeAlamat[i];
                    string Agama = fakeAgama[i];
                    string StatusPerkawinan = fakeStatusPerkawinan[i];
                    string Pekerjaan = fakePekerjaan[i];
                    string Kewarganegaraan = fakeKewarganegaraan[i];

                    // Prepare the SQL query
                    string query = "INSERT INTO biodata (nik, nama, tempat_lahir, tanggal_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan) VALUES (@NIK, @Regexed, @TempatLahir, @TanggalLahir, @Gender, @BloodType, @Alamat, @Agama, @StatusPerkawinan, @Pekerjaan, @Kewarganegaraan)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Adding parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@NIK", NIK);
                        cmd.Parameters.AddWithValue("@Regexed", Regexed);
                        cmd.Parameters.AddWithValue("@TempatLahir", TempatLahir);
                        cmd.Parameters.AddWithValue("@TanggalLahir", TanggalLahir.ToString("yyyy-MM-dd")); // Ensure date format
                        cmd.Parameters.AddWithValue("@Gender", Gender);
                        cmd.Parameters.AddWithValue("@BloodType", BloodType);
                        cmd.Parameters.AddWithValue("@Alamat", Alamat);
                        cmd.Parameters.AddWithValue("@Agama", Agama);
                        cmd.Parameters.AddWithValue("@StatusPerkawinan", StatusPerkawinan);
                        cmd.Parameters.AddWithValue("@Pekerjaan", Pekerjaan);
                        cmd.Parameters.AddWithValue("@Kewarganegaraan", Kewarganegaraan);

                        // Execute the query
                        int result = cmd.ExecuteNonQuery();

                        // Optional: Print success message
                        if (result > 0)
                        {
                            Console.WriteLine($"Record for '{Regexed}' inserted successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to insert record for '{Regexed}'.");
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

public class RegexMatcher
{
    // cara ngerun :
    // csc src\regex.cs
    // ./regex.exe
    public static string GetPattern(string awal)
    {
        string pattern = @"\b"; // Start of word boundary
        foreach (char c_awal in awal)
        {
            bool matchFound = false; // Flag to track if a match is found

            // Lowercase loop
            for (char c = 'a'; c <= 'z'; c++)
            {
                if (c_awal == c)
                {
                    matchFound = true; // Set matchFound to true
                    pattern += "([" + c + char.ToUpper(c); // Match lowercase or uppercase of the character
                    switch (c)
                    {
                        case 'a':
                            pattern += "4])?";
                            break;
                        case 'b':
                            pattern += "]|[1][3])";
                            break;
                        case 'd':
                            pattern += "]|[1][7])";
                            break;
                        case 'e':
                            pattern += "3])?";
                            break;
                        case 'g':
                            pattern += "6])";
                            break;
                        case 'i':
                            pattern += "1])?";
                            break;
                        case 'o':
                            pattern += "0])?";
                            break;
                        case 'r':
                            pattern += "]|[1][2])";
                            break;
                        case 's':
                            pattern += "5])";
                            break;
                        case 'u':
                            pattern += "])?";
                            break;
                        case 'z':
                            pattern += "2])";
                            break;
                        default:
                            pattern += "])";
                            break;
                    }
                    //pattern += "?";
                    break; // Exit the lowercase loop
                }
            }



            // Uppercase loop
            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (matchFound) // Check if a match is found in the lowercase loop
                {
                    break;
                }
                if (c_awal == c)
                {
                    matchFound = true;
                    pattern += "([" + c + char.ToLower(c); // Match uppercase or lowercase of the character
                    switch (c)
                    {
                        case 'A':
                            pattern += "4])";
                            break;
                        case 'B':
                            pattern += "]|[1][3])";
                            break;
                        case 'D':
                            pattern += "]|[1][7])";
                            break;
                        case 'E':
                            pattern += "3])";
                            break;
                        case 'G':
                            pattern += "6])";
                            break;
                        case 'O':
                            pattern += "0])?";
                            break;
                        case 'I':
                            pattern += "1])";
                            break;
                        case 'R':
                            pattern += "]|[1][2])";
                            break;
                        case 'S':
                            pattern += "5])";
                            break;
                        case 'U':
                            pattern += "])?";
                            break;
                        case 'Z':
                            pattern += "2])";
                            break;
                        default:
                            pattern += "])";
                            break;
                    }
                    //pattern += "?";
                    break; // Exit the uppercase loop

                }
            }
            if (!matchFound)
            {
                pattern += c_awal;
            }
        }

        pattern += @"\b"; // End of word boundary

        return pattern;
    }

    public static string GenerateAlayString(string input)
    {
        Random random = new Random();
        StringBuilder result = new StringBuilder();

        foreach (char c in input)
        {
            if (char.IsLetter(c))
            {
                // Mengonversi huruf menjadi kombinasi huruf besar dan kecil
                char convertedChar = random.Next(0, 2) == 0 ? char.ToLower(c) : char.ToUpper(c);
                result.Append(convertedChar);
            }
            else
            {
                result.Append(c);
            }
        }

        string tempResult = result.ToString();
        result.Clear();

        foreach (char c in tempResult)
        {
            // Mengonversi huruf menjadi angka
            switch (char.ToLower(c))
            {
                case 'a':
                    result.Append('4');
                    break;
                case 'e':
                    result.Append('3');
                    break;
                case 'i':
                    result.Append('1');
                    break;
                case 'o':
                    result.Append('0');
                    break;
                case 's':
                    result.Append('5');
                    break;
                case 'g':
                    result.Append('6');
                    break;
                case 'b':
                    result.Append('8');
                    break;
                default:
                    result.Append(c);
                    break;
            }
        }

        // Menyingkat kata-kata dengan menghilangkan huruf vokal
        string shortenedResult = ShortenWords(result.ToString());

        return shortenedResult;
    }

    static string ShortenWords(string input)
    {
        StringBuilder result = new StringBuilder();
        bool previousWasVowel = false;

        foreach (char c in input)
        {
            if (IsVowel(c))
            {
                if (!previousWasVowel)
                {
                    result.Append(c);
                    previousWasVowel = true;
                }
            }
            else
            {
                result.Append(c);
                previousWasVowel = false;
            }
        }

        return result.ToString();
    }

    static bool IsVowel(char c)
    {
        char lower = char.ToLower(c);
        return lower == 'a' || lower == 'e' || lower == 'i' || lower == 'o' || lower == 'u';
    }
}