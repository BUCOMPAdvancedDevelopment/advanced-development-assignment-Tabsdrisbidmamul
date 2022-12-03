using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class ProfileController: BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> ProfileEdit(ProfileEdit profileEdit)
        {
          
          return HandleResult(await Mediator.Send(new Application.UserProfile.Edit.Command {DisplayName = profileEdit.DisplayName} ));
        }
    }
}