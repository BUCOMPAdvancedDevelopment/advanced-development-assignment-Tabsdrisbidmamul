using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models;
using Domain;
using Domain.Extensions;
using Domain.Types;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace API.Controllers
{
  /// <summary>
  /// Authenticate and Authorisation endpoints
  /// </summary>
  [AllowAnonymous]
  [ApiController]
  [Route("api/[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
    {
      _signInManager = signInManager;
      _userManager = userManager;
      _tokenService = tokenService;
    }

    /// <summary>
    /// Get the current user from their JWT token Claims 
    /// </summary>
    /// <returns>User DTO with new JWT token</returns>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDTO>> GetCurrentUser()
    {
      var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

      if(user == null) return BadRequest("Token is invalid");

      return await GenerateUserDTO(user);
    }

    /// <summary>
    /// Log the user in, check if the user is in the Identity Table
    /// </summary>
    /// <param name="loginDto">email and password</param>
    /// <returns>User DTO with new JWT token</returns>
    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
    {
      var user = await _userManager.FindByEmailAsync(loginDto.Email);

      if(user == null) return Unauthorized("Your email or password is incorrect");

      var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

      if(!result.Succeeded) return Unauthorized("Your email or password is incorrect");

      return await GenerateUserDTO(user);
    }

    /// <summary>
    /// Signup a user to the application
    /// </summary>
    /// <param name="signupDTO">email, password, username and display name</param>
    /// <returns>User DTO with new JWT token</returns>
    [HttpPost("signup")]
    public async Task<ActionResult<UserDTO>> Signup(SignupDTO signupDTO)
    {
      if(await _userManager.Users.AnyAsync(x => x.Email == signupDTO.Email)) 
      {
        return BadRequest("Email taken");
      }

      if(await _userManager.Users.AnyAsync(x => x.UserName == signupDTO.Username)) 
      {
        return BadRequest("Username taken");
      }

      var user = new User
      {
        DisplayName = signupDTO.DisplayName,
        Email = signupDTO.Email,
        UserName = signupDTO.Username,
        Role = Roles.User.GetStringValue()
      };

      var result = await _userManager.CreateAsync(user, signupDTO.Password);

      if(!result.Succeeded) return BadRequest("Problem registering user");

      return await GenerateUserDTO(user);

    }

    /// <summary>
    /// Change the password for the user in the identity table
    /// </summary>
    /// <param name="changePasswordDTO">old and new password</param>
    /// <returns>User DTO with new JWT token</returns>
    [HttpPost("change-password")]
    public async Task<ActionResult<UserDTO>> ChangePassword(ChangePasswordDTO changePasswordDTO)
    {
      if(string.IsNullOrWhiteSpace(changePasswordDTO.OldPassword) && string.IsNullOrEmpty(changePasswordDTO.NewPassword))
      {
        return BadRequest();
      }

      var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

      if(user == null) return Unauthorized();

      var canSignIn = await _signInManager.CheckPasswordSignInAsync(user, changePasswordDTO.OldPassword, false);

      if(!canSignIn.Succeeded) return Unauthorized("Password is incorrect");

      var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);

      if(!result.Succeeded) return BadRequest("Could not change password");

      return await GenerateUserDTO(user);
    }

    /// <summary>
    /// Get username endpoint - used in reviews
    /// </summary>
    /// <param name="username"></param>
    /// <returns>User image and display name</returns>
    [AllowAnonymous]
    [HttpGet("{username}")]
    public async Task<IActionResult> GetUser(string username)
    {
      var user = await _userManager.Users
        .Include(u => u.Image)
        .FirstOrDefaultAsync(u => u.UserName == username);
      
      if(user == null) return BadRequest("Bad username");

      return Ok(
        new
        {
          Image = user.Image,
          DisplayName = user.DisplayName
        }
      );
    }

    /// <summary>
    /// Generate a new refresh token just before their current JWT token expires - the refresh token is a randomly generated string hash, and is checked against the stored refresh token in the user record before granting the user a new JWT token
    /// </summary>
    /// <returns>User DTO with new JWt token</returns>
    [Authorize]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<UserDTO>> RefreshToken()
    {
      var refreshToken = Request.Cookies["refreshToken"];

      var user = await _userManager.Users.Include(r => r.RefreshTokens)
        .FirstOrDefaultAsync(u => u.UserName == User.FindFirstValue(ClaimTypes.Name));
      
      if(user == null) return Unauthorized();

      var oldToken = user.RefreshTokens.SingleOrDefault(u => u.Token == refreshToken);

      if(oldToken != null && !oldToken.IsActive) return Unauthorized();

      return await GenerateUserDTO(user);
    }

    /// <summary>
    /// Create a new User DTO for the frontend, which contains the JWT token
    /// </summary>
    /// <param name="user">The found user in the Identity table</param>
    /// <returns>User DTO with new JWT token</returns>
    private async Task<UserDTO> GenerateUserDTO(User user)
    {
      var _user =
        await _userManager.Users.Include(u => u.Image)
          .FirstOrDefaultAsync(x => x.Email == user.Email);

      await SetRefreshToken(user);

      return new UserDTO
      {
        DisplayName = _user.DisplayName,
        Image = _user.Image,
        Token = _tokenService.CreateToken(_user),
        Username = _user.UserName,
        Role = _user.Role
      };
    }

    /// <summary>
    /// Create and set a new refresh token to the user in the identity table record, and append the token to the HTTP only cookies - so frontend JS cannot tamper with it
    /// </summary>
    /// <param name="user">Found user in the Identity table</param>
    /// <returns></returns>
    private async Task SetRefreshToken(User user)
    {
      var refreshToken = _tokenService.CreateRefreshToken();

      user.RefreshTokens.Add(refreshToken);
      await _userManager.UpdateAsync(user);

      var cookieOpts = new CookieOptions
      {
        HttpOnly = true,
        Expires = DateTime.UtcNow.AddDays(7)
      };

      Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOpts);
    }
  }
}