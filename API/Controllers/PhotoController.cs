using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Images;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{ 
    /// <summary>
    /// Cloudinary endpoints
    /// </summary>
    [Authorize]
    public class PhotoController: BaseApiController
    {
      /// <summary>
      /// Add new media to cloudinary, only works for a logged in user
      /// </summary>
      /// <param name="cancellationToken"></param>
      /// <param name="command">media file and path</param>
      /// <returns>Image url and public id</returns>
      [HttpPost]
      public async Task<IActionResult> Add(CancellationToken cancellationToken, [FromForm] Add.Command command)
      {
        return HandleResult(await Mediator.Send(command, cancellationToken));
      }

      /// <summary>
      /// Delete media from cloudinary, only works for a logged in user
      /// </summary>
      /// <param name="cancellationToken"></param>
      /// <returns>deletion result</returns>
      [HttpDelete]
      public async Task<IActionResult> Delete(CancellationToken cancellationToken)
      {
        return HandleResult(await Mediator.Send(new Delete.Command()));
      }

      /// <summary>
      /// Add media to a game, only works if the user is an admin user
      /// </summary>
      /// <param name="cancellationToken"></param>
      /// <param name="command">media file, path</param>
      /// <returns>image url and public id</returns>
      [Authorize(Policy = "IsAdmin")]
      [HttpPost]
      [Route("add-games-photo/")]
      public async Task<IActionResult> AddGamePhoto(CancellationToken cancellationToken, [FromForm] Application.Games.Add.Command command)
      {
        return HandleResult(await Mediator.Send(command, cancellationToken));
      }
    }
}