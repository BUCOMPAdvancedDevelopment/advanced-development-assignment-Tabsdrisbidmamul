using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models;
using Domain;
using Domain.Types;
using Extensions;
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

      return GenerateUserDTO(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
    {
      var user = await _userManager.FindByEmailAsync(loginDto.Email);

      if(user == null) return Unauthorized();

      var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

      if(!result.Succeeded) return Unauthorized();

      return GenerateUserDTO(user);
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

      return GenerateUserDTO(user);

    }

    private UserDTO GenerateUserDTO(User user)
    {
      return new UserDTO
      {
        DisplayName = user.DisplayName,
        Image = null,
        Token = _tokenService.CreateToken(user),
        Username = user.UserName,
        Role = user.Role
      };
    }
  }
}