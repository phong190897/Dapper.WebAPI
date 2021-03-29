using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dapper.Core.RequestEntities
{
    public class LoginRequest
    {
        [Required]
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
