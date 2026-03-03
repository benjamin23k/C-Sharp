using System;
using Contacts.Domain.Entities;
using Contacts.Show;
using Contacts;

namespace Contacts.Search
{
    public static class SearchContact
    {
        public static void Search()
        {
            Console.Clear();
            if (Contact.All.Count == 0)
            {
                Console.WriteLine("No contacts available.");
                Console.WriteLine("\nPress 0 to return...");
                InputHelpers.ReadNumber("");
                return;
            }

            ShowAllContact.ShowAll();

            int id = InputHelpers.ReadNumber("ID to search (0 to return): ");
            if (id == 0)
                return;

            var contact = Contact.GetById(id);
            if (contact == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Contact does not exist.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("\n===== CONTACT =====");
            contact.Show();
            Console.WriteLine("\n-------------------");
            Console.WriteLine("Press 0 to return...");
            InputHelpers.ReadNumber("");
        }
    }
}