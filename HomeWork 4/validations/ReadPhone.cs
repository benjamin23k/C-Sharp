using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phone.validations
{
    public static class ReadPhone
    {
        public static string GetPhone(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine()!.Trim();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (!input.All(char.IsDigit))
                {
                    Console.WriteLine(" Invalid phone number. Example: 8099162344");
                    continue;
                }

                if (input.Length < 10 || input.Length > 11)
                {
                    Console.WriteLine(" Invalid phone number. Example: 8099162344");
                    continue;
                }

                return input;
            }
        }
    }
}