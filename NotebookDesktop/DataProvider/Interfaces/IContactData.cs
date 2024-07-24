using NotebookContext.Model;

namespace Notebook.Data.Interfaces
{
    public interface IContactData
    {
        public Task<IEnumerable<Contact>> GetContactsAsync();

        public Task<Contact> GetContactAsync(int? id);

        public Task CreateContactAsync(Contact contact);

        public Task UpdateContactAsync(Contact contact);

        public Task DeleteContactAsync(int? id);

        bool ContactExists(int id);
    }
}
