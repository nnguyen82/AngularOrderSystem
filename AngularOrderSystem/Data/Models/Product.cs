using System;
using Newtonsoft.Json;

namespace AngularOrderSystem.Data.Models
{
    public class Product
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Descriptions { get; set; }

        public double Price { get; set; }
    }
}
