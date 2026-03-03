using System;

namespace Contacts
{
    public static class InputHelpers
    {
        public static string ReadOnlyLetters(string prompt, int minLen, int maxLen)
        {
            
            return Letters.validations.ReadOnlyLetters.GetLetters(prompt, minLen, maxLen);
        }

        public static string ReadRequired(string prompt)
        {
            
            Console.Write(prompt);
            return Console.ReadLine() ?? string.Empty;
        }

        public static string ReadPhone(string prompt)
        {
            return Phone.validations.ReadPhone.GetPhone(prompt);
        }

        public static string ReadEmail(string prompt)
        {
            return Readgmail.validations.GmailValidator.ReadEmail(prompt);
        }

        public static int ReadAge(string prompt)
        {
            return age.validations.ReadAge.GetAge(prompt);
        }

        public static bool ReadBestFriend()
        {
            return IsBest.validations.BestFriend.Get();
        }

        public static bool Confirm(string prompt)
        {
            Console.Write(prompt + " (y/n): ");
            string? resp = Console.ReadLine();
            return resp?.StartsWith("y", StringComparison.OrdinalIgnoreCase) == true;
        }

        public static int ReadNumber(string prompt)
        {
            return NumberPhone.validations.ReadNumber.GetNumber(prompt);
        }
    }
}