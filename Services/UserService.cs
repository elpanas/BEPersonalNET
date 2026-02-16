using BEPersonal.DBs;
using BEPersonal.DTOs.In;
using Microsoft.EntityFrameworkCore;

namespace BEPersonal.Services
{
    public class UserService : IUserService
    {
        private readonly PersonalDBContext _context;

        public UserService(PersonalDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Login(UserDTOIn userDTO)
        {
            // Here you would typically check the email and password against a database
            // For demonstration purposes, we'll just return a new UserDTOIn if the email and password are not empty
            if (!string.IsNullOrEmpty(userDTO.Email) && !string.IsNullOrEmpty(userDTO.Password))
            {
                // Cerco l'utente per email
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);

                if (user == null) return false;

                return BCrypt.Net.BCrypt.Verify(userDTO.Password, user.Password);
            }
            return true;

        }
    }
}
