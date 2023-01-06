using System.Text.Json.Serialization;

namespace AntdMangement.RequestModels.Login
{
    public class LoginToken
    {
        [JsonPropertyName("expires_in")]
        public long Expires_in;
        [JsonPropertyName("access_token")]
        public string Access_token;
        [JsonPropertyName("refresh_token")]
        public string Refresh_token;
    }
}
