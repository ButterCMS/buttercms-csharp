using Newtonsoft.Json;

namespace ButterCMS.Models
{
    public class Page<T>
    {
        public string Slug { get; set; }
        [JsonProperty("page_type")]
        public string PageType { get; set; }
        public T Fields { get; set; }
    }
}
