using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day1Hash
{
    class Program
    {
        static void Main(string[] args)
        {
            //Step 1: Take User Input
            var uInput = Console.ReadLine();

            //Step 2: Convert to Stream
            byte[] byteData = Encoding.UTF8.GetBytes(uInput);
            Stream inputStream = new MemoryStream(); 

            //Step 3: Encrypt input and output results
            using (SHA256 shaHash = new SHA256Managed())
            {
                var result = shaHash.ComputeHash(inputStream);
                var output = BitConverter.ToString(result);

                Console.WriteLine(output.Replace("-","").Substring(0, 8));
            }                        
        }
    }
}
