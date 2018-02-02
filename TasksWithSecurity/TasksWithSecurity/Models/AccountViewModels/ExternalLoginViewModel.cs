using System.ComponentModel.DataAnnotations;

namespace TasksWithSecurity.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
