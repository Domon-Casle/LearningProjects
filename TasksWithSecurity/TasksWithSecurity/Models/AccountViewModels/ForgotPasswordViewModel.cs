using System.ComponentModel.DataAnnotations;

namespace TasksWithSecurity.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
