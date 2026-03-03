using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsBest.validations
{
    public static class BestFriend
    {
        public static bool Get(string message = "¿Best Friend? (1=Yes, 2=No): ")
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine()!.Trim();

                if (input == "1" 
                || input.Equals("yes", StringComparison.OrdinalIgnoreCase) 
                || input.Equals("y", StringComparison.OrdinalIgnoreCase) 
                || input.Equals("Yes", StringComparison.OrdinalIgnoreCase) 
                || input.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                return true;
                }

                if (input == "2" 
                || input.Equals("No", StringComparison.OrdinalIgnoreCase) 
                || input.Equals("N", StringComparison.OrdinalIgnoreCase) 
                || input.Equals("n", StringComparison.OrdinalIgnoreCase)
                || input.Equals("no", StringComparison.OrdinalIgnoreCase)
                || input.Equals("NO", StringComparison.OrdinalIgnoreCase))  
                
                {
                return false;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" invalid input. Please enter '1' for Yes or '2' for No.");
                Console.ResetColor();
            }
        }
    }
}









