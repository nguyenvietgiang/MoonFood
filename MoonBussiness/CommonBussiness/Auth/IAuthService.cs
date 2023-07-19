using MoonModels;


namespace MoonBussiness.CommonBussiness.Auth
{
    public interface IAuthService
    {
        string GenerateJwtToken(Account account);
        string HashPassword(string password);
    }
}
