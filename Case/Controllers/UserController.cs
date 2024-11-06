using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Case.Dtos;
using Case.Services;

namespace Case.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Register(dto);
            return result ? Ok("Kayıt başarılı") : BadRequest("Kayıt başarısız");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = await _userService.Login(dto);
            return token != null ? Ok(new { Token = token }) : Unauthorized("Geçersiz kullanıcı bilgileri");
        }
    }
}
