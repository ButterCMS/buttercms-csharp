using System;
using Newtonsoft.Json;

namespace ButterCMS.Models
{
    public class Page<T>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime Updated { get; set; }
        public DateTime? Published { get; set; }
        [JsonProperty("page_type")]
        public string PageType { get; set; }
        public T Fields { get; set; }
    }
}
