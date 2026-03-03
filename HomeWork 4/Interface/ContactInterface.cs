using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Contacts.Domain.Entities;

namespace Contacts.Interfaces
{
    public interface IContactRepository
    {
        void Add(Contact contact);
        List<Contact> GetAll();
        Contact? GetById(int id);
        void Delete(int id);
        void Update(int id, Contact contact);
    }
}