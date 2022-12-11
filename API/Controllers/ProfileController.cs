using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// User details endpoint
    /// </summary>
    [Authorize]
    public class ProfileController: BaseApiController
    { 
        /// <summary>
        /// Edit a user profile, they must be logged in
        /// </summary>
        /// <param name="profileEdit"></param>
        /// <returns>New User DTO</returns>
        [HttpPost]
        public async Task<IActionResult> ProfileEdit(ProfileEdit profileEdit)
        {
          
          return HandleResult(await Mediator.Send(new Application.UserProfile.Edit.Command {DisplayName = profileEdit.DisplayName} ));
        }
    }
}