using System;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;

namespace utils
{   
    public class Comparer
    {
        public void compareKMP(string filepath1, string filepath2) //Fungsi untuk menghitung border (basically border function or b(k))
        {

            List<string> binaryString1 = ImageToBin.ImageToBinaryParts(filepath1, 30);
            List<string> binaryString2 = ImageToBin.ImageToBinaryParts(filepath2, 30);
            string ascii1 = BinAsciiConv.BinaryToAscii(binaryString1);
            string ascii2 = BinAsciiConv.BinaryToAscii(binaryString2);
            KMP.KMP(ascii1, ascii2);

        }
    }
    
}