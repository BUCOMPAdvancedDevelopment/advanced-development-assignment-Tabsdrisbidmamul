using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ICloudinaryPhoto
    {
      /// <summary>
      /// Upload image with a path specified, allows for more structure in cloundiary 
      /// </summary>
      /// <param name="file">blob of file</param>
      /// <param name="path">path to folder</param>
      /// <returns>public Id and public url for deletion and transformation purposes in the frontend</returns>
      Task<Image> AddPhoto(IFormFile file, string path);
      /// <summary>
      /// Destroy the image from the logo/** folder
      /// </summary>
      /// <param name="publicId">Will be attached to all dtos that have images with them</param>
      /// <returns>deletion results</returns>
      Task<string> DeletePhoto(string publicId);
    }
}