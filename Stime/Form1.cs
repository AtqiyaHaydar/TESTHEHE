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

namespace Stime
{
    public partial class Form1 : Form
    {
        private string inputFilePath = "";
        private string inputImagePath = "";
        
        private List<string> binaryStringList = new List<string>();
        private List<string> binaryToAsciiResult = new List<string>();

        Boolean isInBMMode = true; // true jika mode BM, false jika mode KMP

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        // Fungsi Input Image
        private void chooseImage_Click(object sender, EventArgs e) {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
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

        }

        // KMP Mode Click
        private void KMP_Click(object sender, EventArgs e) {

        }

        // Search Button
        private void searchButton_Click(object sender, EventArgs e) {
            try {
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
}
