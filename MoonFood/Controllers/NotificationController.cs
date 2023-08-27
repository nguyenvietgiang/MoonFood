using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Messaging;
using static Azure.Core.HttpHeader;
using Google.Cloud.Firestore.V1;
using MoonFood.Common.CommonModels;
using Microsoft.Extensions.Options;
using System.Text;

namespace YourWebApi.Controllers
{
    [Route("api/v1/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
    //    private readonly FirestoreDb _firestoreDb;
    //    private const string CollectionName = "notifications";
        private readonly OneSignalConfig _oneSignalConfig;
        private readonly IHttpClientFactory _httpClientFactory;


        public NotificationsController(/*IWebHostEnvironment env, */ IOptions<OneSignalConfig> oneSignalConfig,
            IHttpClientFactory httpClientFactory)
        {
            ////var projectId = "moondb-e3b12";
            ////var pathToConfigJson = env.ContentRootPath + "/Common/FirebaseSdk/moondb-e3b12-firebase-adminsdk-w0a8n-339fa17c8b.json";
            ////GoogleCredential credentials;
            ////using (var stream = new FileStream(pathToConfigJson, FileMode.Open, FileAccess.Read))
            ////{
            ////    credentials = GoogleCredential.FromStream(stream);
            ////}
            ////_firestoreDb = FirestoreDb.Create(projectId);

             _oneSignalConfig = oneSignalConfig.Value;
            _httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// get notification 
        /// </summary>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        //{
        //    var snapshot = await _firestoreDb.Collection(CollectionName).GetSnapshotAsync();
        //    var notifications = new List<Notification>();
        //    foreach (var document in snapshot.Documents)
        //    {
        //        var notification = document.ConvertTo<Notification>();
        //        notifications.Add(notification);
        //    }
        //    return Ok(notifications);
        //}

        [HttpPost]
        public async Task<IActionResult> SendNotification()
        {
            string restApiKey = _oneSignalConfig.RestApiKey;
            string appId = _oneSignalConfig.AppId;

            try
            {
                using (var client = _httpClientFactory.CreateClient())
                {
                    client.BaseAddress = new Uri("https://onesignal.com/api/v1/");

                    var content = new StringContent(
                        "{\"app_id\": \"" + appId + "\", \"contents\": {\"en\": \"Hello, this is a push notification!\"}}",
                        Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + restApiKey);

                    var response = await client.PostAsync("notifications", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok("Notification sent successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to send notification. Status code: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }
    }
}

