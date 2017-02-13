using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day1Hash
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get hashed key from user input
            var myInput = Console.ReadLine();
            string hashKey = Encrypt256(myInput);

            Console.WriteLine(hashKey);

            int start = 0;
            int lineNum = 0;
            bool invalid = true;

            //If first three numbers are out of bounds,
            //get next three numbers
            while (invalid) 
            {
                lineNum = int.Parse(hashKey.Substring(start, 3));
                invalid = lineNum > File.ReadLines(@"..\..\TheSonnets.txt").Count() ? true : false;
                start++;
            }

            Console.WriteLine(lineNum);
            //Capitalize first letter of each word
            string line = File.ReadLines(@"..\..\TheSonnets.txt").Skip(lineNum).Take(1).First();
            //http://stackoverflow.com/questions/1943273/convert-all-first-letter-to-upper-case-rest-lower-for-each-word
            line = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(line.ToLower());
            Console.WriteLine(line);

            //Rearrange words in line (alphabetize) and strip " "
            List<string> sList = new List<string>(
                line.Split(new string[] { " " }, StringSplitOptions.None));
            sList = sList.OrderByDescending(w => w).ToList();
            string results = string.Join("", sList.ToArray());

            //Output results
            Console.WriteLine(results);
        }

        /*****************************************
         * Encrypt256()
         * Hashing code written by Joel in class
         *****************************************/
        static string Encrypt256(string input)
        {
            string output;
            byte[] byteData = Encoding.ASCII.GetBytes(input);
            Stream inputStream = new MemoryStream(byteData);

            using (SHA256 shaM = new SHA256Managed())
            {
                var result = shaM.ComputeHash(inputStream);
                output = BitConverter.ToString(result);
            }

            //Strip special characters
            output = output.Replace("-", "");
            //Strip letters
            output = Regex.Replace(output, "[A-Za-z ]", "");

            return output;
        }
    }
}
