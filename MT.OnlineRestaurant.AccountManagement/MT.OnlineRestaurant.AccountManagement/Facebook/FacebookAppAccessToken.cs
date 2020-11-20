using Newtonsoft.Json;

namespace MT.OnlineRestaurant.AccountManagement.Facebook
{
    public class FacebookAppAccessToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
