using System;

class Levenshtein
{
    public static int ComputeLevenshteinDistance(string s1, string s2)
    {
        int lenS1 = s1.Length;
        int lenS2 = s2.Length;

        // Buat matriks (lenS1+1) x (lenS2+1)
        int[,] d = new int[lenS1 + 1, lenS2 + 1];

        // Inisialisasi baris dan kolom pertama
        for (int i = 0; i <= lenS1; i++)
        {
            d[i, 0] = i;
        }
        for (int j = 0; j <= lenS2; j++)
        {
            d[0, j] = j;
        }

        // Isi matriks dengan menghitung jarak
        for (int i = 1; i <= lenS1; i++)
        {
            for (int j = 1; j <= lenS2; j++)
            {
                int cost = s1[i - 1] == s2[j - 1] ? 0 : 1;

                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1,    // Penghapusan
                             d[i, j - 1] + 1),   // Penyisipan
                    d[i - 1, j - 1] + cost      // Penggantian
                );
            }
        }

        // Nilai di d[lenS1, lenS2] adalah jarak Levenshtein
        return d[lenS1, lenS2];
    }

    public static double ComputeSimilarityPercentage(string s1, string s2)
    {
        int distance = ComputeLevenshteinDistance(s1, s2);
        int maxLen = Math.Max(s1.Length, s2.Length);

        // Menghitung persentase kemiripan
        if (maxLen == 0) return 100.0; // Jika kedua string kosong
        double similarity = (1.0 - (double)distance / maxLen) * 100;
        return similarity;
    }

    static void Main()
    {
        string s1 = "eaten";
        string s2 = "eating";

        int distance = ComputeLevenshteinDistance(s1, s2);
        double similarity = ComputeSimilarityPercentage(s1, s2);

        Console.WriteLine("Levenshtein Distance: " + distance);
        Console.WriteLine("Similarity: " + similarity + "%");
    }
}
