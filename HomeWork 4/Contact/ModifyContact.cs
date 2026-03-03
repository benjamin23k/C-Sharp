using System;
using Contacts.Domain.Entities;
using Contacts.Show;
using Contacts;

namespace Contacts.Modify
{
    public static class ModifyContact
    {
        public static void Modify()
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

            int id = InputHelpers.ReadNumber("ID to modify (0 to return): ");
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

            string newName = InputHelpers.ReadOnlyLetters("New name: ", 2, 50);
            string newLastname = InputHelpers.ReadOnlyLetters("New lastname: ", 2, 50);
            string newAddress = InputHelpers.ReadRequired("New address: ");
            string newPhone = InputHelpers.ReadPhone("New phone: ");
            string newEmail = InputHelpers.ReadEmail("New email: ");
            int newAge = InputHelpers.ReadAge("New age: ");
            bool newBest = InputHelpers.ReadBestFriend();

            Console.WriteLine("\n----- NEW DATA -----");
            Console.WriteLine($"{newName} {newLastname}");

            if (!InputHelpers.Confirm("Apply changes?"))
                return;

            contact.Name = newName;
            contact.Lastname = newLastname;
            contact.Address = newAddress;
            contact.Phone = newPhone;
            contact.Gmail = newEmail;
            contact.Age = newAge;
            contact.IsBestFriend = newBest;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Contact modified.");
            Console.ResetColor();




        }
    }
}