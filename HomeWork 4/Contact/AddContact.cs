using System;
using System.Linq;
using Contacts.Domain.Entities;
using Letters.validations;
using Read.validations;
using age.validations;
using Phone.validations;
using Readgmail.validations;
using NumberPhone.validations;
using IsBest.validations;

namespace Contacts.Add
{
    public static class AddContact
    {
        public static void Add()
        {
            string name = InputHelpers.ReadOnlyLetters("Name: ", 2, 50);
            string lastname = InputHelpers.ReadOnlyLetters("Lastname: ", 2, 50);
            string address = InputHelpers.ReadRequired("Address: ");

            string phone;
            do
            {
                phone = InputHelpers.ReadPhone("Phone: ");
                if (Contact.All.Any(c => c.Phone == phone))
                    Console.WriteLine(" That phone number already exists.");
            } while (Contact.All.Any(c => c.Phone == phone));

            string email = InputHelpers.ReadEmail("Email: ");
            int age = InputHelpers.ReadAge("Age: ");
            bool best = InputHelpers.ReadBestFriend();

            Console.WriteLine("\n----- RESUMEN -----");
            Console.WriteLine($"{name} {lastname}");
            Console.WriteLine($"Address: {address}");
            Console.WriteLine($"Phone: {phone}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Best Friend: {(best ? "Yes" : "No")}");

            if (!InputHelpers.Confirm("¿Save contact?"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Cancelled.");
                Console.ResetColor();
                return;
            }

            int id = Contact.NextId;
            var contact = new Contact(id, name, lastname, address, phone, email, age, best);
            Contact.All.Add(contact);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nContact created.");
            Console.ResetColor();
        }
    }
}