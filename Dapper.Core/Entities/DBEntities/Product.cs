using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Core.Entities.DBEntities
{
    public class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("barcode")]
        public string Barcode { get; set; }
        [JsonProperty("rate")]
        public decimal Rate { get; set; }
        [JsonProperty("added_on")]
        public DateTime AddedOn { get; set; }
        [JsonProperty("modified_on")]
        public DateTime ModifiedOn { get; set; }
    }
}
