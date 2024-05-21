using System;
using System.Text.RegularExpressions;

class Program
{

    // cara ngerun :
    // csc src\regex.cs
    // ./regex.exe
    static void Main()
    {
        // Input string
        string input = "Bintang Dwi Marthen"; // String dalam bentuk normal

        //String dalam bentuk alay pake https://alaygenerator.blogspot.com/ buat nge generate, harusnya semuanya muncul di all matches found
        string data = "13ntn6 DW1 m4rtH3n BNTAn6 Dw mrThn bnT4N6 Dw mrthN BNtNg dW mrth3n BNt4N6 dW mrthN bnTn6 dW Marthn bntng dwi mrthn bntng dW mRTh3n";
        
        // Pattern to match
        string pattern = GetPattern(input); // Generate Pattern
        Console.WriteLine("pattern : "+pattern);
        
        // Use Regex.Match to find the first match in the input string
        Match match = Regex.Match(data, pattern);

        // Check if a match is found
        if (match.Success)
        {
            // Print the matched value
            Console.WriteLine("First Match found: " + match.Value);
        }
        else
        {
            Console.WriteLine("No match found.");
        }

        // Use Regex.Matches to find all matches in the input string
        MatchCollection matches = Regex.Matches(data, pattern);

        // Print all matches found
        Console.WriteLine("All matches found:");
        int id = 1;
        foreach (Match m in matches)
        {
            Console.WriteLine(id+"). "+m.Value);
            id++;
        }


    }
    static string GetPattern(string awal)
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
                    switch(c)
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
                            pattern+= "])";
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
                    switch(c){
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
                            pattern+= "])";
                            break;
                    }
                    //pattern += "?";
                    break; // Exit the uppercase loop
                    
                }
            }
            if(!matchFound){
                pattern += c_awal;
            }
        }

        pattern += @"\b"; // End of word boundary

        return pattern;
    }

}
