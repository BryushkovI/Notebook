using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthAppLib.Model
{
    public class User : IdentityUser
    {
        [NotMapped]
        public IList<string> Roles { get; set; }
    }
}
