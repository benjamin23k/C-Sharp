using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace age.validations
{
    public static class ReadAge
    {
        public static int GetAge(string message)
        {
            while (true)
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out int age) && age >= 1 && age <= 119)
                    return age;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Invalid age.");
                Console.ResetColor();
            }
        }
    }
}