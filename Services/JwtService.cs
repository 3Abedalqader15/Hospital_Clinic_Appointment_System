using Hospital_Clinic_Appointment_System.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hospital_Clinic_Appointment_System.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration configuration;
        public JwtService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(User user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("phone", user.Phone_Number ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // unique identifier for the token
                new Claim(JwtRegisteredClaimNames.Iat, // issued at time in seconds since epoch
                    new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey( 
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)  // Ensure the key is not null and is properly configured
                );

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256 // Use HMAC SHA256 for signing the token
                );

            var token = new JwtSecurityToken( // Create the JWT token with the specified parameters

                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(
                    double.Parse(configuration["Jwt:ExpiryInHours"]!)
                ),
                signingCredentials: creds
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public int? ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var TokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);

            try
            {
                TokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // No tolerance for expiry
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.TryParse(
                    jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                    out int id
                    
                    )? id
                    : (int?)null;
                return userId;


            }
            catch
            {
                return null; // Token invalid
            }



        }

    }
}
