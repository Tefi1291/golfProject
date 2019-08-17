using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GolfAPI.Core.Contracts.Api
{

    public class OrderApi
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "number")]
        public string OrderNumber { get; set; }
        [JsonProperty(PropertyName = "created")]
        public string DateCreated { get; set; }
        [JsonProperty(PropertyName = "required")]
        public string DateRequired { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public UserApi User { get; set; }
        [JsonProperty(PropertyName = "components", NullValueHandling = NullValueHandling.Ignore)]
        public ComponentApi[] Components { get; set; }
      
    }

}
