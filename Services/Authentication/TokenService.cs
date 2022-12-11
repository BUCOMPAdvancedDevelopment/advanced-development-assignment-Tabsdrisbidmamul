using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;

namespace Services.Authentication
{
  /// <summary>
  /// Authentication service, JWT tokens are created to authenticate that the user is who they say they are to the application
  /// </summary>
  public class TokenService : ITokenService
  {
    private readonly IConfiguration _config;
    
    public TokenService(IConfiguration config)
    {
      _config = config;
    }

    public RefreshToken CreateRefreshToken()
    {
      var randomNumber = new byte[32];
      using var rng = RandomNumberGenerator.Create();
      rng.GetBytes(randomNumber);
      return new RefreshToken
      {
        Token = Convert.ToBase64String(randomNumber)
      };
    }

    public string CreateToken(User user)
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role)
      };

      var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

      string jwtKey;

      if(env == "Development") {
        jwtKey = _config.GetValue<string>("JwtKey");
      } else {
        jwtKey = Environment.GetEnvironmentVariable("JwtKey");
      }

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddMinutes(1),
        SigningCredentials = credentials
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }

  
}