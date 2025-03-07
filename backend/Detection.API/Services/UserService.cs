using Detection.API.Models;

namespace Detection.API.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Email = "test@example.com", Password = "password123", Fingerprint = "unique-fingerprint-id" }
        };

        public User Authenticate(string email, string password)
        {
            return _users.SingleOrDefault(x => x.Email == email && x.Password == password);
        }
    }
}
