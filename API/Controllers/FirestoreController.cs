using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Firestore;
using API.Models;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FirestoreController: ControllerBase
    {
        private readonly FirestoreProvider _firestoreProvider;

        public FirestoreController(FirestoreProvider firestoreProvider)
        {
          _firestoreProvider = firestoreProvider;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewDTO reviewDTO, CancellationToken cancellationToken)
        {
          // Check if an array already exists, if so we can append to the already existing list and push that array up to firestore
          var guid = new Guid(reviewDTO.GameId).ToString("N");
          var document = await _firestoreProvider.Get<Review>(guid, cancellationToken);

          var username = User.FindFirstValue(ClaimTypes.Name);

          Dictionary<string, string> tempMap = new Dictionary<string, string>
          {
            {"username", username},
            {"rating", reviewDTO.Rating},
            {"review", reviewDTO.Review}
          };

          var tempList = new List<Dictionary<string, string>>
          {
            tempMap
          };

          if(document != null)
          {
            foreach(Dictionary<string, string> entry in document.Reviews)
            {
              string _username;
              if(entry.TryGetValue("username", out _username)) 
              {
                if(_username.Equals(username)) 
                {
                  return BadRequest("You have already made a review");
                }
              }
            }

            document.Reviews.Add(tempMap);
            tempList = new List<Dictionary<string, string>>(document.Reviews);
          } 

          var review = new Review(reviewDTO.GameId, tempList);

          await _firestoreProvider.AddOrUpdate(review, cancellationToken);

          return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview(ReviewDTO reviewDTO, CancellationToken cancellationToken)
        {
          var guid = new Guid(reviewDTO.GameId).ToString("N");
          var document = await _firestoreProvider.Get<Review>(guid, cancellationToken);

          var username = User.FindFirstValue(ClaimTypes.Name);

          if(document == null) return BadRequest("Review could not be updated");

          bool willUpdate = false;
          foreach(Dictionary<string, string> entry in document.Reviews)
          {
            string _username;
            if(entry.TryGetValue("username", out _username) && entry.TryGetValue("review", out _) && entry.TryGetValue("rating", out _)) 
            {
              if(_username.Equals(username)) 
              {
                entry["review"] = reviewDTO.Review;
                entry["rating"] = reviewDTO.Rating;
                willUpdate = true;
              }
            }
          }

          if(!willUpdate)
          {
            return BadRequest("Cannot update review");
          }

          await _firestoreProvider.AddOrUpdate<Review>(document, cancellationToken);
          return Ok();

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReviews(string username)
        {
          throw new NotImplementedException();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetReviews(CancellationToken cancellationToken)
        {
          var allReviews = await _firestoreProvider.GetAll<Review>(cancellationToken);

          return Ok(allReviews);
        }
    }
}