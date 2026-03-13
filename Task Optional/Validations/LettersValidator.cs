using System;

namespace Validations
{
    public static class LettersValidator
    {
        public static string ReadOnlyLetters(string message, int min, int max)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("The field cannot be empty");
                    continue;
                }

                if (input.Length < min || input.Length > max)
                {
                    Console.WriteLine($"Between {min} And {max}");
                    continue;
                }

                if (input.All(c => char.IsLetter(c) || c == ' '))
                    return input;

                Console.WriteLine("Only Letters Are Allowed");
            }
        }
    }
}

