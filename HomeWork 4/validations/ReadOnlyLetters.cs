using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letters.validations
{
    public static class ReadOnlyLetters
    {
        public static string GetLetters(string message, int min, int max)
        {
            while (true)
            {
                min = 2;
                max = 14;
                Console.Write(message);
                string input = Console.ReadLine()!.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine(" Input cannot be empty.");
                    continue;
                }

                if (input.Length < min || input.Length > max)
                {
                    Console.WriteLine($"Between {min} and {max} characters");
                    continue;  
                }

                if (input.All(c => char.IsLetter(c) || c == ' '))
                    return input;

                Console.WriteLine(" Only letters and spaces allowed.");
            }
        }
    }

}
