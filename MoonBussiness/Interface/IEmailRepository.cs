

namespace MoonBussiness.Interface
{
    public interface IEmailRepository
    {
        Task SendEmailAsync(string email, string content);

    }
}
