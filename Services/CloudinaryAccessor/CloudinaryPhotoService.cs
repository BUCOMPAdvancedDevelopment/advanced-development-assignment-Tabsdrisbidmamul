using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Services.CloudinaryAccessor
{
  public class CloudinaryPhotoService : ICloudinaryPhoto
  {
    private readonly Cloudinary _cloudinary;
    private readonly ILogger<CloudinaryPhotoService> _logger;

    public CloudinaryPhotoService(IOptions<CloudinarySettings> config, ILogger<CloudinaryPhotoService> logger)
    {
      _logger = logger;
      Account account = new(
        config.Value.CloudName,
        config.Value.ApiKey,
        config.Value.ApiSecret
      );

      _cloudinary = new Cloudinary(account);
    }

    public async Task<CloudinaryDTO> AddPhoto(IFormFile file)
    {
      if(file.Length == 0) return null;

      await using var stream = file.OpenReadStream();

      var uploadParams = new ImageUploadParams
      {
        File = new FileDescription(file.FileName, stream),
        Transformation = new Transformation().FetchFormat("auto").Quality("90")
      };

      var uploadResult = await _cloudinary.UploadAsync(uploadParams);

      if(uploadResult.Error != null) 
      {
        _logger.LogInformation($"ERROR: {this.GetType()} Could not upload photo to Cloudinary {uploadResult.Error.Message},\n File dump: Filename {file.FileName}");
        throw new Exception(uploadResult.Error.Message);
      }

      return new CloudinaryDTO
      {
        PublicId = uploadResult.PublicId,
        Url = uploadResult.SecureUrl.ToString()
      };

    }

    public async Task<string> DeletePhoto(string publicId)
    {
      var deleteParams = new DeletionParams(publicId);

      var deleteResult = await _cloudinary.DestroyAsync(deleteParams);

      return deleteResult.Result == "ok" ? deleteResult.Result : null;
    }
  }
}