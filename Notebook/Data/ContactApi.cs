using Newtonsoft.Json;
using Notebook.Data.Interfaces;
using NotebookContext.Model;
using System.Net.Mime;

namespace Notebook.Data
{
    public class ContactApi : IContactData
    {
        public HttpClient HttpClient { get; set; }

        public ContactApi()
        {
            HttpClient = new HttpClient();
        }

        public async Task CreateContactAsync(Contact contact)
        {
            string url = @$"https://localhost:7297/api/Contact";

            await HttpClient.PostAsync(requestUri: url,
                                       content: new StringContent(
                                            JsonConvert.SerializeObject(contact), 
                                            System.Text.Encoding.UTF8, 
                                            MediaTypeNames.Application.Json)
                                       );
        }

        public async Task DeleteContactAsync(int? id)
        {
            string url = @$"https://localhost:7297/api/Contact/{id}";

            var request = await HttpClient.DeleteAsync(url);
        }

        public async Task<Contact> GetContactAsync(int? id)
        {
            string url = @$"https://localhost:7297/api/Contact/{id}";

            var response = await HttpClient.GetAsync(url);

            return JsonConvert.DeserializeObject<Contact>(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            string url = @"https://localhost:7297/api/Contact";

            var response = await HttpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<IEnumerable<Contact>>(response);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            string url = @$"https://localhost:7297/api/Contact";
            await HttpClient.PutAsync(url,
                                      new StringContent(
                                              JsonConvert.SerializeObject(contact),
                                              System.Text.Encoding.UTF8,
                                              MediaTypeNames.Application.Json)
                                      );
        }

        public bool ContactExists(int id)
        {
            try
            {
                string url = @$"https://localhost:7297/api/Contact/{id}";

                var response = HttpClient.GetAsync(url).Result;

                return JsonConvert.DeserializeObject<Contact>(response.Content.ReadAsStringAsync().Result) != null;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
