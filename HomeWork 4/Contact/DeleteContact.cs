using System;
using System.Linq;
using Contacts.Domain.Entities;
using Contacts.Show;
using Contacts;

namespace Contacts.Delete
{
    public static class DeleteContact
    {
        public static void Delete()
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

            int id = InputHelpers.ReadNumber("ID to delete (0 to return): ");
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

            if (!InputHelpers.Confirm($"Delete {contact.Name} {contact.Lastname}?"))
                return;

            Contact.All.Remove(contact);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Contact deleted.");
            Console.ResetColor();
        }
    }
}