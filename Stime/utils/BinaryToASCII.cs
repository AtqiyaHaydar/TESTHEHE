using System;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;

namespace utils
{
    public class BinAsciiConv
    {
        // cara ngerun :
        // csc src\BinaryToASCII.cs
        // ./BinaryToASCII.exe
        static void Main()
        {

            string bin = "01001000011000010110110001101111";
            string ascii = BinaryToAscii(bin);
            Console.WriteLine("ascii : " + ascii);

            // Referensi Buat ngecek hasil https://www.rapidtables.com/convert/number/ascii-to-binary.html pake yang ACII/UTF-8
        }

        public string BinaryToAscii(string bin)
        {
            StringBuilder sb = new StringBuilder();
            int l = bin.Length;
            int n = l / 8;
            string[] binChunks = new string[n];

            for (int i = 0; i < n; i++)
            {
                binChunks[i] = bin.Substring(i * 8, 8);
            }
            byte[] binaryData = new byte[n];

            for (int i = 0; i < n; i++)
            {
                binaryData[i] = Convert.ToByte(binChunks[i], 2);
            }

            // Convert binary data to string using ISO-8859-1 encoding
            string iso88591Text = Encoding.GetEncoding("ISO-8859-1").GetString(binaryData);
            return iso88591Text;
        }

        public string AsciiToBinary(string ascii)
        {
            byte[] asciiBytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(ascii);
            StringBuilder binaryStringBuilder = new StringBuilder();

            foreach (byte asciiByte in asciiBytes)
            {
                // Convert each ASCII byte to binary representation and append to the string
                binaryStringBuilder.Append(Convert.ToString(asciiByte, 2).PadLeft(8, '0'));
            }

            return binaryStringBuilder.ToString();
        }
    }
    

}