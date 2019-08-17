using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GolfAPI.Core.Contracts.Api
{
    [JsonObject]
    public class LoginResponse
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "guid")]
        public string Giud { get; set; }
    }
}
