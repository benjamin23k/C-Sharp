using System;
using System.Net.Mail;

namespace Readgmail.validations
{
    public static class GmailValidator
    {
        public static string ReadEmail(string message)
        {
            while (true)
            {
                try
                {
                    Console.Write(message);
                    string input = Console.ReadLine()!;
                    _ = new MailAddress(input);
                    return input;
                }
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Use a valid email format (e.g. example@gmail.com)");
                    Console.ResetColor();
                }
            }
        }
    }
}