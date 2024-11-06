using System.Threading.Tasks;
using Case.Dtos;

namespace Case.Services
{
    public interface IUserService
    {
        Task<bool> Register(UserRegistrationDto dto);
        Task<string> Login(UserLoginDto dto);
    }
}

