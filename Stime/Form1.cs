using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stime;
using System.Data.SQLite;

namespace Stime
{
    public partial class Form1 : Form
    {
        private string inputFilePath = "";
        private string inputImagePath = "";
        private Boolean isKMP = true; // switch mode
        
        private List<string> binaryStringList = new List<string>();
        private List<string> binaryToAsciiResult = new List<string>();

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        // Fungsi Input Image
        private void chooseImage_Click(object sender, EventArgs e) {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp"; // Format file
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    // Get the path of specified file
                    inputFilePath = openFileDialog.FileName;
                    this.inputImagePath = inputFilePath;

                    // Display image in PictureBox
                    InputImage.Image = Image.FromFile(inputFilePath);
                }
            }
        }

        // BM Mode Click
        private void BM_Click(object sender, EventArgs e) {
            this.isKMP = false;
            MessageBox.Show("Mode telah diubah ke Boyer-Moore", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // KMP Mode Click
        private void KMP_Click(object sender, EventArgs e) {
            this.isKMP = true;
            MessageBox.Show("Mode telah diubah ke Knuth-Morris-Pratt", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Search Button
        private void searchButton_Click(object sender, EventArgs e) {
            try {
                // Memeriksa apakah gambar sudah dipilih
                if (string.IsNullOrEmpty(inputImagePath))
                {
                    MessageBox.Show("Silakan pilih gambar terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Convert ke Binary 
                binaryStringList = ImageToBinaryParts(inputImagePath, 30);
                
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../outputBinary.txt");
                using (StreamWriter writer = new StreamWriter(filePath)) {
                    foreach (string binString in binaryStringList) {
                        writer.WriteLine(binString);
                    }
                }

                // 2. Convert ke ASCII
                binaryToAsciiResult = BinaryToAscii(binaryStringList);

                string filePathAscii = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../outputAscii.txt");
                using (StreamWriter writer = new StreamWriter(filePathAscii))
                {
                    foreach (string asciiString in binaryToAsciiResult)
                    {
                        writer.WriteLine(asciiString);
                    }
                }

                // 3. Memulai Pencarian
                string modePencarian = isKMP ? "Knuth-Morris-Pratt" : "Boyer-Moore";
                MessageBox.Show($"Mode pencarian yang dipilih: {modePencarian}", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                string dbPath = "../../database/mydatabase.db";
                string connectionString = $"Data Source={dbPath};";
                string query = "SELECT * FROM sidik_jari LIMIT 10";

                List<string> hasilQuery = new List<string>();

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SQLiteCommand command = new SQLiteCommand(query, connection);
                        SQLiteDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string berkasCitra = reader["berkas_citra"].ToString();
                            hasilQuery.Add(berkasCitra); // Memasukkan data dari database ke hasilQuery
                            Console.WriteLine(string.Format("berkas_citra: {0}", berkasCitra));
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("An error occurred: {0}", ex.Message));
                        MessageBox.Show("Error Terjadi", ex.Message);
                    }
                }

                // convert hasil query ke binary
                List<List<string>> hasilBinary = new List<List<string>>();
                foreach (string hasil in hasilQuery)
                {
                    hasilBinary.Add(ImageToBinaryParts(hasil, 30));
                }

                // convert setiap hasil binary ke ascii
                List<List<string>> hasilAscii = new List<List<string>>();
                foreach (List<string> hasil in hasilBinary)
                {
                    hasilAscii.Add(BinaryToAscii(hasil));
                }


                bool found = false;

                if (found)
                {
                    MessageBox.Show("Sidik jari ditemukan di database.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sidik jari tidak ditemukan di database.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex) {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        // Fungsi convert gambar ke binary (dengan pembagian area)
        public List<string> ImageToBinaryParts(string imagePath, int partSize) { // fungsi untuk membagi image ke beberapa bagian lalu di convert ke binary
            List<string> binaryStrings = new List<string>();

            try {
                using (Bitmap image = new Bitmap(imagePath)) {
                    int width = image.Width;
                    int height = image.Height;

                    int partsHorizontal = (int)Math.Ceiling((double)width / partSize);
                    int partsVertical = (int)Math.Ceiling((double)height / partSize);

                    for (int y = 0; y < partsVertical; y++) {
                        for (int x = 0; x < partsHorizontal; x++) {
                            int startX = x * partSize;
                            int startY = y * partSize;
                            int endX = Math.Min((x + 1) * partSize, width);
                            int endY = Math.Min((y + 1) * partSize, height);

                            Rectangle rect = new Rectangle(startX, startY, endX - startX, endY - startY);

                            using (Bitmap partBitmap = image.Clone(rect, PixelFormat.Format24bppRgb)) {
                                string binaryString = ImageToBinary(partBitmap);
                                binaryStrings.Add(binaryString);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show("An error occurred while processing the image: " + ex.Message);
            }

            return binaryStrings;
        }

        // Fungsi convert gambar ke binary
        static string ImageToBinary(Bitmap image) {
            using (MemoryStream stream = new MemoryStream()) {
                image.Save(stream, ImageFormat.Png);

                byte[] binaryData = stream.ToArray();

                string binaryString = "";
                foreach (byte b in binaryData) {
                    binaryString += Convert.ToString(b, 2).PadLeft(8, '0');
                }
                return binaryString;
            }
        }

        // Fungsi convert list binary ke ascii
        public List<string> BinaryToAscii(List<string> binaryStrings)
        {
            List<string> asciiStrings = new List<string>();

            foreach (string binaryString in binaryStrings)
            {
                StringBuilder sb = new StringBuilder();

                int l = binaryString.Length;
                for (int i = 0; i < l; i += 8)
                {
                    string byteString = binaryString.Substring(i, 8);
                    byte b = Convert.ToByte(byteString, 2);
                    sb.Append((char)b);
                }

                asciiStrings.Add(sb.ToString());
            }

            return asciiStrings;
        }
    }
    // Fungsi algortima Booyer More
    public class BoyerMoore
    {
        private int[] badCharTable;
        private int[] goodSuffixTable;
        private string pattern;
        private int patternLength;

        public BoyerMoore(string str1, string str2)
        {
            this.pattern = str1;
            this.patternLength = str1.Length;
            badCharTable = BuildBadCharTable(str1);
            goodSuffixTable = BuildGoodSuffixTable(str1);
        }

        // Membangun bad character table
        private int[] BuildBadCharTable(string pattern)
        {
            const int ASCII_SIZE = 256;
            int[] table = new int[ASCII_SIZE];

            // Inisialisasi semua kejadian sebagai -1
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = -1;
            }

            // Menetapkan nilai karakter sebagai kejadian terakhirnya dalam pola
            for (int i = 0; i < pattern.Length; i++)
            {
                table[(int)pattern[i]] = i;
            }

            return table;
        }

        // Membangun good sufix table
        private int[] BuildGoodSuffixTable(string pattern)
        {
            int patternLength = pattern.Length;
            int[] table = new int[patternLength];
            int[] suffixes = new int[patternLength];

            // Inisialisasi semua nilai ke nol
            for (int i = 0; i < patternLength; i++)
            {
                suffixes[i] = 0;
            }

            // Prapemrosesan pola untuk menemukan suffix
            suffixes[patternLength - 1] = patternLength;
            int g = patternLength - 1;
            int f = 0;

            for (int i = patternLength - 2; i >= 0; i--)
            {
                if (i > g && suffixes[i + patternLength - 1 - f] < i - g)
                {
                    suffixes[i] = suffixes[i + patternLength - 1 - f];
                }
                else
                {
                    if (i < g)
                    {
                        g = i;
                    }
                    f = i;
                    while (g >= 0 && pattern[g] == pattern[g + patternLength - 1 - f])
                    {
                        g--;
                    }
                    suffixes[i] = f - g;
                }
            }

            // Prapemrosesan untuk membangun good sufix table
            for (int i = 0; i < patternLength; i++)
            {
                table[i] = patternLength;
            }

            int j = 0;
            for (int i = patternLength - 1; i >= 0; i--)
            {
                if (suffixes[i] == i + 1)
                {
                    for (; j < patternLength - 1 - i; j++)
                    {
                        if (table[j] == patternLength)
                        {
                            table[j] = patternLength - 1 - i;
                        }
                    }
                }
            }

            for (int i = 0; i <= patternLength - 2; i++)
            {
                table[patternLength - 1 - suffixes[i]] = patternLength - 1 - i;
            }

            return table;
        }

        // Metode pencarian untuk memeriksa apakah pola ditemukan dalam teks
        public bool Search(string text)
        {
            int textLength = text.Length;
            int skip;

            for (int i = 0; i <= textLength - patternLength; i += skip)
            {
                skip = 0;
                for (int j = patternLength - 1; j >= 0; j--)
                {
                    if (pattern[j] != text[i + j])
                    {
                        skip = Math.Max(1, j - badCharTable[text[i + j]]);
                        skip = Math.Max(skip, goodSuffixTable[j]);
                        break;
                    }
                }
                if (skip == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }

    // Fungsi algortima Knuth-Morris-Pratt
    public class KMP
    {
        static int[] Border(string inputString) //Fungsi untuk menghitung border (basically border function or b(k))
        {

            int l = inputString.Length;
            int[] Borders = new int[l];
            Borders[0] = 0;
            for (int k = 1; k < l; k++)
            {
                string substring_end = inputString.Substring(1, k);
                string substring_front = inputString.Substring(0, k + 1);
                bool valid = true;
                int front_ln = substring_front.Length;
                int end_ln = substring_end.Length;
                //Console.WriteLine("Borders ["+k+"] : ");
                for (int i = 0; i < front_ln - 1; i++)
                {

                    for (int j = i; j >= 0; j--)
                    {
                        //Console.WriteLine("At index["+i+"] : "+substring_front[i] + " and " +substring_end[end_ln-j-1]);
                        if (substring_front[i - j] != substring_end[end_ln - j - 1])
                        {
                            //Console.WriteLine("!=");
                            valid = false;
                            break;
                        }
                    }
                    if (valid == true)
                    {
                        Borders[k] = i + 1;
                        //Console.WriteLine("i : "+(i+1));
                        //Console.WriteLine("Borders["+k+"] : "+Borders[k]);
                    }
                    valid = true;
                }
                if (valid == false && Borders[k] == 0)
                {
                    Borders[k] = Borders[k - 1];
                }
            }

            return Borders;
        }

        public bool KMPfunc(string str1, string str2)
        {
            int[] Borders = Border(str2);
            // for (int i = 0; i < Borders.Length; i++)
            // { //ngecek hasil border function
            //     Console.WriteLine("Largest border [" + i + "] : " + Borders[i]);
            // }

            int l1 = str1.Length;
            int l2 = str2.Length;
            int idx1 = 0;
            int idx2 = 0;
            while (idx1 < l1 - 1 && idx2 < l2 - 1)
            {
                if (str1[idx1] != str2[idx2])
                {
                    idx2 = Borders[idx2];
                }
                else
                {
                    idx2++;
                }
                idx1++;

            }
            if (idx2 == l2 - 1)
            {
                Console.WriteLine("Target found in index : " + (idx1 - idx2) + "-" + idx1);
                return true;
            }
            else
            {
                Console.WriteLine("Target Not Found");
                return false;
            }
        }
    }
}
