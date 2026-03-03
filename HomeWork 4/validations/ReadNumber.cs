using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberPhone.validations
{
    public static class ReadNumber
    {
        public static int GetNumber(string message)
        {
            while (true)
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Invalid number. Example: 849927382");
                Console.ResetColor();
            }
        }
    }
}