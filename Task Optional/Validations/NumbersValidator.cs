using System;

namespace Validations
{
    public static class NumbersValidator
    {
        public static int ReadOnlyNumbers(string message, int min, int max)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (!int.TryParse(input, out int value))
                {
                    Console.WriteLine("Please enter a valid number");
                    continue;
                }

                if (value < min || value > max)
                {
                    Console.WriteLine($"Number must be between {min} and {max}");
                    continue;
                }

                return value;
            }
        }

        
    }
}

