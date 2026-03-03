using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contacts.Interfaces;
using Contacts.Domain.Entities;

   namespace Contacts.Infrastructure
{
    public class InMemoryContactRepository : IContactRepository
    {
        private readonly List<Contact> contacts = new();

        public void Add(Contact contact)
        {
            contacts.Add(contact);
        }

        public List<Contact> GetAll()
        {
            return contacts;
        }

        public Contact? GetById(int id)
        {
            return contacts.FirstOrDefault(c => c.Id == id);
        }

        public void Delete(int id)
        {
            var contact = GetById(id);
            if (contact != null)
                contacts.Remove(contact);
        }

        public void Update(int id, Contact updatedContact)
        {
            var existing = GetById(id);
            if (existing != null)
            {
                existing.Name = updatedContact.Name;
                existing.Lastname = updatedContact.Lastname;
                existing.Address = updatedContact.Address;
                existing.Phone = updatedContact.Phone;
                existing.Gmail = updatedContact.Gmail;
                existing.Age = updatedContact.Age;
                existing.IsBestFriend = updatedContact.IsBestFriend;
            }
        }
    }
}
