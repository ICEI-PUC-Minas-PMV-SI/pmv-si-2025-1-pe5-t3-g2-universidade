using ApiNovaHorizonte.Configuration;
using ApiNovaHorizonte.Interfaces;
using ApiNovaHorizonte.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiNovaHorizonte.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public UserService(IUserRepository userRepository, IOptions<JwtSettings> jwtOptions)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<int> AddAsync(User user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            return await _userRepository.RemoveAsync(id);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<User?> LoginAsync(string email, string senha)
        {
            return await _userRepository.GetByEmailAndSenhaAsync(email, senha);
        }

        public async Task<string?> LoginJwtAsync(string email, string senha)
        {
            var user = await _userRepository.GetByEmailAndSenhaAsync(email, senha);
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Role, user.Cargo.ToString()) // exemplo: Funcionario
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
