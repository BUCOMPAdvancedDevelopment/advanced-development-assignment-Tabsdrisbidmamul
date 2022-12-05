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
        public Dictionary<string, object> ReviewMap { get; set; }

        public Review()
        {

        }

        public Review(string gameId, Dictionary<string, object> reviews)
        {
          Id = new Guid(gameId).ToString("N");
          ReviewMap = reviews;
        }
    }
}