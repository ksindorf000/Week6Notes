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
        public static int indexStart = 0;
        public static int lineNum = 0;

        static void Main(string[] args)
        {
            bool again = true;

            while (again)
            {
                //Get hashed key from user input
                var myInput = Console.ReadLine();
                string hashKey = Encryptor.Encrypt256(myInput);

                //Get lines
                List<string> lineList = new List<string>();
                for (int i = 0; i < 4; i++)
                {
                    lineNum = Encryptor.GetLineNum(hashKey);
                    lineList.Add(Encryptor.GetLine(lineNum));
                }

                //Get random words
                List<string> rWords = new List<string>();
                foreach (var line in lineList)
                {
                    rWords.Add(Encryptor.GetRandWord(line));
                }

                //Capitalize words and display
                Encryptor.Results(rWords);

                Console.WriteLine("Again? ");
                string yN = Console.ReadLine();
                again = yN == "Y" ? true : false;
            }
        }
    }
}


