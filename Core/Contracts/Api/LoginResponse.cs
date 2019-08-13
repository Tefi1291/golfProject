using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Api
{
    [JsonObject]
    public class LoginResponse
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "giud")]
        public string Giud { get; set; }
    }
}
