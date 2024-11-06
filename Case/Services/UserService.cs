using System.Threading.Tasks;
using Case.Dtos;
using BCrypt.Net;
using Case.Models;
using Case.Repositories;
using Case.Services;
using Case.Data;

namespace Case.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Register(UserRegistrationDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password)
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<string> Login(UserLoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null || !VerifyPassword(user.PasswordHash, dto.Password))
            {
                return null;
            }

            return GenerateJwtToken(user);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string hashedPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private string GenerateJwtToken(User user)
        {
            // JWT token oluşturma işlemi
            return "example.jwt.token";
        }
    }
}
