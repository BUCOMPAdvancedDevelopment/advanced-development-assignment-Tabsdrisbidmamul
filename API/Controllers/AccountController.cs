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

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDTO>> GetCurrentUser()
    {
      var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

      if(user == null) return BadRequest("Token is invalid");

      return await GenerateUserDTO(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
    {
      var user = await _userManager.FindByEmailAsync(loginDto.Email);

      if(user == null) return Unauthorized("Your email or password is incorrect");

      var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

      if(!result.Succeeded) return Unauthorized("Your email or password is incorrect");

      return await GenerateUserDTO(user);
    }

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