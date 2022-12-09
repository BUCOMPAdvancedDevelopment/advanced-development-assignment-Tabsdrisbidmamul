using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Services.Interfaces;

namespace API.Models
{
    [FirestoreData]
    public class Review: IFirebaseEntity
    {
        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty]     
        public List<Dictionary<string, string>> Reviews { get; set; }

        public Review()
        {

        }

        public Review(string gameId, List<Dictionary<string, string>> reviews)
        {
          Id = new Guid(gameId).ToString("N");
          Reviews = reviews;
        }
    }
}