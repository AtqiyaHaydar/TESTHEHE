# Tubes3_Pejuang4Stima
Pemanfaatan Pattern Matching dalam Membangun Sistem Deteksi Individu Berbasis Biometrik Melalui Citra Sidik Jari

## **Table of Contents**
* [General Information](#general-information)
* [Requirements](#requirements)
* [How to Run and Compile](#how-to-run-and-compile)
* [Screenshot](#screenshot)
* [Authors](#authors)

## **General Information**
Desktop application to perform individual identification based on biometric fingerprints. The methods used for fingerprint detection are Boyer-Moore and Knuth-Morris-Pratt algorithms. Additionally, the system will be linked to an individual's identity through a database, with the hope of creating a system that can fully recognize someone's identity solely based on their fingerprints. This application use C# programming languange and SQLite for the database.

>The Knuth-Morris-Pratt (KMP) algorithm is a string searching algorithm that efficiently finds occurrences of a pattern within a longer text. It works by precomputing a partial match table that helps avoid unnecessary comparisons during the search process. This table, also known as the "failure function" or "prefix function," allows the algorithm to skip characters in the text that are known not to match the pattern. As a result, the KMP algorithm achieves linear time complexity in the worst case, making it particularly efficient for large texts or patterns.

>The Boyer-Moore algorithm is a string searching algorithm that efficiently finds occurrences of a pattern within a longer text. It works by scanning the text from left to right while comparing characters of the pattern with corresponding characters in the text. The key feature of the Boyer-Moore algorithm is its ability to skip comparisons based on information gathered during preprocessing.
The algorithm preprocesses the pattern to create two lookup tables: the "bad character" table and the "good suffix" table. These tables allow the algorithm to determine the maximum amount it can shift the pattern relative to the text based on mismatched characters encountered during the search process. By using these tables, the Boyer-Moore algorithm can skip comparisons and shift the pattern efficiently, resulting in faster search times.

>Regular expressions (regex) are patterns used for efficient string matching. They consist of basic characters, literals, escape characters for special meanings like '.', '', and character classes like '[ ]' for matching a set of characters or ranges. Pre-defined characters like '\d' for any digit, '\w' for any word character, and anchors like '^' for the start of a string and '$' for the end are also part of regex. Quantifiers such as '', '+', '?', and '{n,m}' are used to specify the number of occurrences. Groups and alternations allow grouping parts of the expression and matching one of several patterns. Lookaheads and lookbehinds, including positive and negative variations, enable matching based on preceding or following patterns without including them in the match. These elements together form powerful tools for flexible and precise string matching.

## **Requirements**
To use this program, you will need to install **Visual Studio** (https://visualstudio.microsoft.com/) on the device you are using.

You have to Download SQLite.
```bash
Install-Package System.Data.SQLite
```

## **How to Run and Compile (Windows)**
### **Setup**
1. Clone this repository <br>
```sh 
$ git clone git@github.com:AtqiyaHaydar/Tubes3_Pejuang4Stima.git
```
2. Open this repository in terminal

### **Run**
1. Open Solution Explorer

2. Click Start Button

## **Screenshot**
<img src="Stime/Properties/home.jpg"> 



## **Authors**

| **NIM**  |       **Name**        | **Class**  |       
| :------: | :-------------------: | :------:   | 
| 13522128 |    Mohammad Andhika Fadillah   | K03
| 13522162 |  Pradipta Rafa Mahesa | K03
| 13522163 |      Atqiya Haydar Luqman    | K03