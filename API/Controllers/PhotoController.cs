using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Images;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class PhotoController: BaseApiController
    {
      [HttpPost]
      public async Task<IActionResult> Add(CancellationToken cancellationToken, [FromForm] Add.Command command)
      {
        return HandleResult(await Mediator.Send(command, cancellationToken));
      }
    }
}