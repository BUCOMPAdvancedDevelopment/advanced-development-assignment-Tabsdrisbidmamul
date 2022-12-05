using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore.V1;
using Google.Cloud.Firestore;
using Services.Interfaces;

namespace Services.Firestore
{
  public class FirestoreProvider
  {
    private readonly FirestoreDb _firestoreDb = null;

    public FirestoreProvider(FirestoreDb firestoreDb)
    {
      _firestoreDb = firestoreDb;
    }

    public async Task AddOrUpdate<T>(T entity, CancellationToken ct) where T : IFirebaseEntity
    {
        var document = _firestoreDb.Collection(typeof(T).Name).Document(entity.Id);
        await document.SetAsync(entity, cancellationToken: ct);
    }

    public async Task<T> Get<T>(string id, CancellationToken ct) where T : IFirebaseEntity
    {
        var document = _firestoreDb.Collection(typeof(T).Name).Document(id);
        var snapshot = await document.GetSnapshotAsync(ct);
        return snapshot.ConvertTo<T>();
    }
  }
}