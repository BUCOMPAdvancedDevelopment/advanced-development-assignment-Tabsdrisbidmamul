using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Services.Interfaces
{
    public interface ITokenService
    {
      /// <summary>
      /// Issues a new JWT token for authentication to authroised endpoints
      /// </summary>
      /// <param name="user">A user from the User table</param>
      /// <returns>hashed JWT token</returns>
      string CreateToken(User user);

      /// <summary>
      /// Issues a new Refresh token for authentication to authroised endpoint - we do this to combat JWT long expiration times
      /// </summary>
      /// <returns>A random 32 byte string each creation</returns>
      RefreshToken CreateRefreshToken();
    }
}