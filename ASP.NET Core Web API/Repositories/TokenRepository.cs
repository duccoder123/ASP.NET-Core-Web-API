using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASP.NET_Core_Web_API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;
        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            
        //Create claims
        var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach(var role in roles) 
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            // biến key nhận giá trị đối tượng khóa đối xứng (khóa có thể mã hóa và giải mã )
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtKey"]));
            
            // SingingCredentials đại diện cho thông tin cần thiết để tạo chữ ký số. Phương pháp mã hóa
            // để xác thực tính toàn vẹn của dữ liệu và tính xác thực

            // (key, algorithm) : 2 đối số
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // JwtSecurtiyToken được sử dụng để tạo và giải mã JWT, là phương pháp xác thực và trao đổi thông tin an toàn
            // hiệu quả
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims, // thông tin về users
                expires: DateTime.Now.AddMinutes(15), // tgian hết hạn của jwt
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token); // giải mã 
        }
    }
}
