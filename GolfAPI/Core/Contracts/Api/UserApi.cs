using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.Core.Contracts.Api
{
    public class UserApi
    {
        [JsonProperty(PropertyName ="id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "firstname")]
        public string Firstname { get; set; }
        [JsonProperty(PropertyName = "lastname")]
        public string Lastname { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "guid")]
        public string Guid { get; set; }
    }
}
