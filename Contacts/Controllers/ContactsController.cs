using Contacts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Contacts.Controllers
{
    public class ContactsController : ApiController
    {
        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<Contact> GetAll()
        {
            var db = new ContactsEntities();
            return db.Contacts.ToList();
        }

        [HttpPost]
        [ActionName("Add")]
        public IHttpActionResult Add(AddContactVm vm)
        {
            Contact newContact = new Contact();
            newContact.FirstName = vm.FirstName;
            newContact.LastName = vm.LastName;
            newContact.PhoneNumber = vm.PhoneNumber;
            newContact.Telepone = vm.Telepone;

            var db = new ContactsEntities();
            db.Contacts.Add(newContact);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newContact.ID }, newContact);
        }


        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            var db = new ContactsEntities();
            var contactToRemove = db.Contacts.FirstOrDefault(c => c.ID == id);

            if (contactToRemove == null)
            {
                return NotFound();
            }

            db.Contacts.Remove(contactToRemove);
            db.SaveChanges();

            return Ok();
        }
    }
}
