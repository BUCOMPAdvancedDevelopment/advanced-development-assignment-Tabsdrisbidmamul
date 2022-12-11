using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// Taken from https://github.com/TryCatchLearn/Reactivities-v6/blob/main/API/Controllers/BaseApiController.cs

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BaseApiController : ControllerBase
  {
    private IMediator _mediator;
    protected IMediator Mediator => 
      _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    /// <summary>
    /// Helper method to return a HTTP status code from the result wrapped object
    /// </summary>
    /// <typeparam name="T">Result</typeparam>
    /// <param name="result">Result object</param>
    /// <returns>HTTP status code</returns>
    protected ActionResult HandleResult<T>(Result<T> result)
    {
      if(result == null) return NotFound();
      
      if(result.IsSuccess && result.Value != null) 
      {
        return Ok(result.Value);
      }

      if(result.IsSuccess && result.Value == null) 
      {
        return NotFound();
      }

      return BadRequest(result.Error);
    }
  }
}