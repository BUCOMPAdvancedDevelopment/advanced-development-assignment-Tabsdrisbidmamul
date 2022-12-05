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
        public async Task<IActionResult> GetReviews(ReviewDTO reviewDTO, CancellationToken cancellationToken)
        {
          Dictionary<string, object> tempMap = new Dictionary<string, object>
          {
            {"username", reviewDTO.Review.Username},
            {"review", reviewDTO.Review.Review}
          };

          var review = new Review(reviewDTO.GameId, tempMap);

          await _firestoreProvider.AddOrUpdate(review, cancellationToken);

          return Ok();
        }

        
    }
}