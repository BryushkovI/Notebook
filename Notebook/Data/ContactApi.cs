using Notebook.Data.Interfaces;
using Notebook.Model;

namespace Notebook.Data
{
    public class ContactApi : IContactData
    {
        public HttpClient HttpClient { get; set; }

        public ContactApi()
        {
            HttpClient = new HttpClient();
        }

        public Task CreateContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContactAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<Contact> GetContactAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contact>> GetContactsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public bool ContactExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
