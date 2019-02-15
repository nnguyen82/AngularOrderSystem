using System;
using Newtonsoft.Json;

namespace AngularOrderSystem.Data.Models
{
    public class Order
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateTime OrderDate { get; set; }

        public Product[] Products { get; set; }
    }
}
