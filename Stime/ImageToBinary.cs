using System;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
using Stime;

namespace Stime
{
    public class ImageToBinary
    {
        // cara ngerun :
        // csc src\ImageToBinary.cs
        // ./ImageToBinary.exe

        static void Main(imagePath)
        {
            
            //filepath dari image yang ingin di convert
            List<string> binaryString = ImageToBinaryParts(imagePath, 30);
            string filePath = "outputBinary.txt"; // file txt buat output binary karena kalo di terminal gak cukup

            // Open the file for writing using StreamWriter
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the long string to the file
                foreach (string binString in binaryString)
                {
                    writer.WriteLine(binString);
                }

            }

            Console.WriteLine("Long string has been written to the file.");

            //Referensi buat ngecek hasil https://capitalizemytitle.com/binary-to-image-converter/
            // cara ngecek tinggal copy binary yang ada di output txt file (1 baris saja setiap ngecek) lalu taro di input website di atas
            // Biasanya bagian tengah image ada di baris ke n/2 dari output txt (n : total baris) 
        }

        public List<string> ImageToBinaryParts(string imagePath, int partSize) // fungsi untuk membagi image ke beberapa bagian lalu di convert ke binary
        {
            List<string> binaryStrings = new List<string>();

            using (Bitmap image = new Bitmap(imagePath))
            {
                int width = image.Width;
                int height = image.Height;

                int partsHorizontal = (int)Math.Ceiling((double)width / partSize);
                int partsVertical = (int)Math.Ceiling((double)height / partSize);

                for (int y = 0; y < partsVertical; y++)
                {
                    for (int x = 0; x < partsHorizontal; x++)
                    {
                        int startX = x * partSize;
                        int startY = y * partSize;
                        int endX = Math.Min((x + 1) * partSize, width);
                        int endY = Math.Min((y + 1) * partSize, height);

                        Rectangle rect = new Rectangle(startX, startY, endX - startX, endY - startY);
                        using (Bitmap partBitmap = image.Clone(rect, PixelFormat.Format24bppRgb))
                        {
                            string binaryString = ImageToBinary(partBitmap);
                            binaryStrings.Add(binaryString);
                        }
                    }
                }
            }

            return binaryStrings;
        }
        static string ImageToBinary(Bitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                // Save the image to a memory stream in PNG format
                image.Save(stream, ImageFormat.Png);

                // Read the binary data from the memory stream
                byte[] binaryData = stream.ToArray();

                // Convert binary data to a string of binary digits
                string binaryString = "";
                foreach (byte b in binaryData)
                {
                    binaryString += Convert.ToString(b, 2).PadLeft(8, '0');
                }
                return binaryString;
            }
        }
    }
    
}