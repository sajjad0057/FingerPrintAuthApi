using Detection.API.Data;
using Detection.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Detection.API.Services
{
    public class UserService : IUserService
    {
        private readonly IAppDbContext _context;
        public UserService(IAppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public User? Authenticate(string email, string password)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        // Get a user by email
        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }

        // Register a new user
        public User RegisterUser(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }
    }
}
