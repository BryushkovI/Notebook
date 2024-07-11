using System.ComponentModel.DataAnnotations;

namespace Notebook.AuthApp
{
    public class UserRegistration
    {
        [Required, MaxLength(20)]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
