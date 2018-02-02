using System.Threading.Tasks;

namespace TasksWithSecurity.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
