using System;
using System.Collections.Generic;
using System.Linq;

namespace Contacts.Domain.Entities
{
    public class Contact
    {
        
        public static List<Contact> All { get; } = new();

        public static int NextId => All.Count == 0 ? 1 : All.Max(c => c.Id) + 1;
        public static Contact? GetById(int id) => All.FirstOrDefault(c => c.Id == id);

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Lastname { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Gmail { get; set; } = "";
        public int Age { get; set; }
        public bool IsBestFriend { get; set; }

        public Contact(int id, string name, string lastname, string address, string phone, string gmail, int age, bool isBestFriend)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
            Address = address;
            Phone = phone;
            Gmail = gmail;
            Age = age;
            IsBestFriend = isBestFriend;
        }

        public void Show()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name} {Lastname}");
            Console.WriteLine($"Address: {Address}");
            Console.WriteLine($"Phone: {Phone}");
            Console.WriteLine($"Gmail: {Gmail}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"BestFriend {(IsBestFriend ? "Yes" : "No")}");
        }
    }
}


    

















