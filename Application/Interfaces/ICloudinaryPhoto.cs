using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface ICloudinaryPhoto
    {
      Task<CloudinaryDTO> AddPhoto(IFormFile file);
      Task<string> DeletePhoto(string publicId);
    }
}