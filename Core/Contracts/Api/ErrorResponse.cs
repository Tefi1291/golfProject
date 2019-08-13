using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Api
{
    [JsonObject]
    public class ErrorResponse
    {
        [JsonProperty(PropertyName = "code")]
        public ErrorCodes ErrorCode { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

    }

    public enum ErrorCodes
    {
        WrongPassword,
        WrongUsername,
        BadRequest
    }
}
