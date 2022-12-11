using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Interfaces;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Domain.Models;

namespace Services.CloudinaryAccessor
{
  /// <summary>
  /// Cloudinary service to store all media in no-sql db
  /// </summary>
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

    /// <summary>
    /// Helper method to add a photo to the no-sql db
    /// </summary>
    /// <param name="file">Image file</param>
    /// <param name="imageParams">Transformations and folder paths params</param>
    /// <returns>The image with the public id and url</returns>
    /// <exception cref="Exception">Upload error</exception>
    private async Task<Image> AddPhoto(IFormFile file, ImageUploadParams imageParams)
    {
      if(file.Length == 0) return null;

      await using var stream = file.OpenReadStream();

      imageParams.File = new FileDescription(file.FileName, stream);

      var uploadResult = await _cloudinary.UploadAsync(imageParams);

      if(uploadResult.Error != null) 
      {
        _logger.LogInformation($"ERROR: {this.GetType()} Could not upload photo to Cloudinary {uploadResult.Error.Message},\n File dump: Filename {file.FileName}");
        throw new Exception(uploadResult.Error.Message);
      }

      return new Image
      {
        PublicId = uploadResult.PublicId,
        Url = uploadResult.SecureUrl.ToString()
      };

    }

    /// <summary>
    /// Add media to Cloudinary
    /// </summary>
    /// <param name="file">Media file</param>
    /// <param name="path">Where to store the media - default location is logo</param>
    /// <returns></returns>
    public async Task<Image> AddPhoto(IFormFile file, string path = "logo")
    {
      var uploadParams = new ImageUploadParams
      {
        Transformation = new Transformation().FetchFormat("auto").Quality("90"),
        Folder = path
      };

      return await AddPhoto(file, uploadParams);
    }

    /// <summary>
    /// Remove media from Cloudinary db
    /// </summary>
    /// <param name="publicId">Public id that is available in all game objects</param>
    /// <returns>Deletion results</returns>
    public async Task<string> DeletePhoto(string publicId)
    {
      var deleteParams = new DeletionParams(publicId);

      var deleteResult = await _cloudinary.DestroyAsync(deleteParams);

      return deleteResult.Result == "ok" ? deleteResult.Result : null;
    }
  }
}