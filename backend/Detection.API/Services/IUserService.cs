using Detection.API.Models;

namespace Detection.API.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
    }
}