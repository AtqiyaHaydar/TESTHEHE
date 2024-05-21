using System;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        
        string P = "ABABABABCABABABAB"; //String tempat pencarian
        string Q = "ABABABAB"; //String yang mau dicari
        KMP(P,Q); // Hasil dibandingin dengan https://cmps-people.ok.ubc.ca/ylucet/DS/KnuthMorrisPratt.html

        // cara ngerun :
        // csc src\KMP.cs
        // ./KMP.exe
    }

    static int[] Border(string inputString) //Fungsi untuk menghitung border (basically border function or b(k))
    {   
        
        int l = inputString.Length;
        int[] Borders = new int[l];
        Borders[0] = 0;
        for (int k = 1; k < l;k++){
            string substring_end = inputString.Substring(1,k);
            string substring_front = inputString.Substring(0,k+1);
            bool valid = true;
            int front_ln = substring_front.Length;
            int end_ln = substring_end.Length;
            //Console.WriteLine("Borders ["+k+"] : ");
            for (int i = 0; i < front_ln-1; i++)
            {   
                
                for (int j = i; j >= 0; j--)
                {
                    //Console.WriteLine("At index["+i+"] : "+substring_front[i] + " and " +substring_end[end_ln-j-1]);
                    if (substring_front[i-j] != substring_end[end_ln-j-1])
                    {
                        //Console.WriteLine("!=");
                        valid = false;
                        break;
                    }
                }
                if (valid == true)
                {
                    Borders[k] = i+1;
                    //Console.WriteLine("i : "+(i+1));
                    //Console.WriteLine("Borders["+k+"] : "+Borders[k]);
                }
                valid = true;
            }
            if (valid == false && Borders[k] ==  0){
                Borders[k] = Borders[k-1];
            }
        }
        
        return Borders;
    }

    static void KMP(string str1,string str2)
    {
        int[] Borders = Border(str2);
        for(int i = 0; i < Borders.Length; i++){ //ngecek hasil border function
            Console.WriteLine("Largest border ["+i+"] : "+Borders[i]);
        }
        
        int l1 = str1.Length;
        int l2 = str2.Length;
        int idx1 = 0;
        int idx2 = 0;
        while(idx1 < l1-1 && idx2 < l2-1)
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
        if(idx2 == l2-1)
        {
            Console.WriteLine("Target found in index : "+(idx1-idx2)+"-"+idx1);
        }
        else
        {
            Console.WriteLine("Target Not Found" );
        }
    }
    

}

