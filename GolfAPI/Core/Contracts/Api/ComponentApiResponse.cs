using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.Core.Contracts.Api
{
    public class ComponentApiResponse
    {
        [JsonProperty(PropertyName = "id")]
        public int ComponentId { get; set; }
        [JsonProperty(PropertyName = "componentCode")]
        public string ComponentCode { get; set; }
        [JsonProperty(PropertyName = "componentQuantity")]
        public int Quantity { get; set; }
    }
}
