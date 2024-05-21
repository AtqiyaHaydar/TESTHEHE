using System;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;

class Program
{
    // cara ngerun :
    // csc src\BinaryToASCII.cs
    // ./BinaryToASCII.exe
    static void Main()
    {
        
        string bin = "01001000011000010110110001101111";
        string ascii = BinaryToAscii(bin);
        Console.WriteLine("ascii : "+ascii);

        // Referensi Buat ngecek hasil https://www.rapidtables.com/convert/number/ascii-to-binary.html pake yang ACII/UTF-8
    }

    static string BinaryToAscii(string bin)
    {
        StringBuilder sb = new StringBuilder();
        int l = bin.Length;
        int n = l/8;
        string[] binChunks = new string[n];
        // if (l % 8 != 0)
        // {
        //     bin = bin.PadLeft((n + 1) * 8, '0');
        //     n++;
        // }
        for (int i = 0;i<n;i++)
        {
            binChunks[i] = bin.Substring(i*8,8);
        }
        byte[] binaryData = new byte[n];

        for (int i = 0; i < n; i++)
        {
            binaryData[i] = Convert.ToByte(binChunks[i], 2);
        }
        string utf8Text = Encoding.UTF8.GetString(binaryData);
        return utf8Text;
    }

}