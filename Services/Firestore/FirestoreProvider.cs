using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore.V1;
using Google.Cloud.Firestore;
using Services.Interfaces;

namespace Services.Firestore
{
  /// <summary>
  /// https://dev.to/kedzior_io/simple-net-core-and-cloud-firestore-setup-1pf9
  /// 
  /// Service to 
  /// </summary>
  public class FirestoreProvider
  {
    private readonly FirestoreDb _firestoreDb = null;

    public FirestoreProvider(FirestoreDb firestoreDb)
    {
      _firestoreDb = firestoreDb;
    }
    
    /// <summary>
    /// Add or update entity data to Firestore collection
    /// </summary>
    /// <typeparam name="T">This is a collection object</typeparam>
    /// <param name="entity">Entity object</param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task AddOrUpdate<T>(T entity, CancellationToken ct) where T : IFirebaseEntity
    {
        var document = _firestoreDb.Collection(typeof(T).Name).Document(entity.Id);
        await document.SetAsync(entity, cancellationToken: ct);
    }

    /// <summary>
    /// Get back a document from firestore
    /// </summary>
    /// <typeparam name="T">This is collection in firestore</typeparam>
    /// <param name="id">Id of the object in sql db and </param>
    /// <param name="ct"></param>
    /// <returns>document converted to T</returns>
    public async Task<T> Get<T>(string id, CancellationToken ct) where T : IFirebaseEntity
    {
        var document = _firestoreDb.Collection(typeof(T).Name).Document(id);
        var snapshot = await document.GetSnapshotAsync(ct);
        return snapshot.ConvertTo<T>();
    }

    /// <summary>
    /// Get all documents within a collection
    /// </summary>
    /// <typeparam name="T">Collection Name</typeparam>
    /// <param name="ct"></param>
    /// <returns>An array of document objects</returns>
    public async Task<IReadOnlyCollection<T>> GetAll<T>(CancellationToken ct) where T : IFirebaseEntity
    {
        var collection = _firestoreDb.Collection(typeof(T).Name);
        var snapshot = await collection.GetSnapshotAsync(ct);
        return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
    }
  }
}