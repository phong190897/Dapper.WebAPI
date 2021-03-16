using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Core.CustomEntities
{
    public class Metadata
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        [JsonProperty("page_size")]
        public int PageSize { get; set; }
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }
        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
        [JsonProperty("has_next_page")]
        public bool HasNextPage { get; set; }
        [JsonProperty("has_previous_page")]
        public bool HasPreviousPage { get; set; }
        [JsonProperty("next_page_url")]
        public string NextPageUrl { get; set; }
        [JsonProperty("previous_page_url")]
        public string PreviousPageUrl { get; set; }
    }
}
