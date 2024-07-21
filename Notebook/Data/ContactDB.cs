using Microsoft.EntityFrameworkCore;
using Notebook.Data.Interfaces;
using Notebook.Model;

namespace Notebook.Data
{
    public class ContactDB : IContactData
    {
        readonly NotebookContext _context;
        public ContactDB(NotebookContext context)
        {
            _context = context;
        }

        public bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }

        public async Task CreateContactAsync(Contact contact)
        {
            _context.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int? id)
        {
            var contact = await GetContactAsync(id);
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<Contact> GetContactAsync(int? id)
        {
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
            return await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await _context.Contact.ToListAsync();
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            _context.Update(contact);
            await _context.SaveChangesAsync();
        }
    }
}
