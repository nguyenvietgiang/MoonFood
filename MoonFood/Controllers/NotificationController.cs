using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Messaging;

namespace YourWebApi.Controllers
{
    [Route("api/v1/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;
        private const string CollectionName = "notifications";

        public NotificationsController()
        {
            var projectId = "moondb-e3b12"; 
            var pathToConfigJson = "MoonFood\\MoonBussiness\\CommonBussiness\\FirebaseSdk\\moondb-e3b12-firebase-adminsdk-w0a8n-339fa17c8b.json";
            GoogleCredential credentials;
            using (var stream = new FileStream(pathToConfigJson, FileMode.Open, FileAccess.Read))
            {
                credentials = GoogleCredential.FromStream(stream);
            }
            _firestoreDb = FirestoreDb.Create(projectId);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            var snapshot = await _firestoreDb.Collection(CollectionName).GetSnapshotAsync();
            var notifications = new List<Notification>();
            foreach (var document in snapshot.Documents)
            {
                var notification = document.ConvertTo<Notification>();
                notifications.Add(notification);
            }
            return Ok(notifications);
        }
    }
}

