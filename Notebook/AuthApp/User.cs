using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notebook.AuthApp
{
    public class User : IdentityUser
    {
        [NotMapped]
        public IList<string> Roles { get; set; }
    }
}
