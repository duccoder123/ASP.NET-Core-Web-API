using ASP.NET_Core_Web_API.Models.DTO;
using ASP.NET_Core_Web_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //UserManager cung cấp các phương thức để quản lý người dùng
        //CreateAsync : tạo một người dùng mới
        // FindByEmailAsync(string email): tìm user theo email
        // FindByIdAsync(string id) : tìm user theo id
        // FindByNameAsync(string name): tìm user theo name
        // UpdateAsync(IdentityUser user): cập nhật thông tin của user
        // DeleteAsync(IdentityUser user) : xóa một user
        // AddRoleAsync(IdentityUser user, IdentityRole role) : thêm một vai trò cho người dùng
        // RemoveRoleAsync(IdentityUser user, IdentityRole role) : xóa một vai trò cho người dùng
        // IsInRoleAsync(IdentityUser user, IdentityRole role): ktra user có thuộc vai trò nào đó hay ko
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.UserName,
                PasswordHash = registerRequestDTO.Password
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRoleAsync(identityUser, registerRequestDTO.Roles.ToString());
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was regitered! Please login");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.UserName);
            if(user != null) 
            {
               var checkPassword = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if(checkPassword)
                {
                    // get roles for this user
                var roles =    await _userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        //create token
                        var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }
                   
                }

            }
            return BadRequest("Username or password is incorrect");
        }
    }
}
