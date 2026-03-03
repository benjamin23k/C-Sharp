using System;
using System.Linq;
using Contacts.Domain.Entities;
using Contacts;

namespace Contacts.Show
{
    public static class ShowAllContact
    {
        public static void ShowAll()
        {
            Console.Clear();

            if (Contact.All.Count == 0)
            {
                Console.WriteLine("No contacts available.");
                Console.WriteLine("\nPress 0 to return...");
                InputHelpers.ReadNumber("");
                return;
            }

            Console.WriteLine("===== LIST OF CONTACTS =====\n");
            foreach (var c in Contact.All)
            {
                Console.WriteLine($"  {c.Id}: {c.Name} {c.Lastname}");
            }
            Console.WriteLine();
        }
    }
}
