using Newtonsoft.Json;
using System.Collections.Generic;

namespace ButterCMS.Tests.Models
{
    public class HotProduct
    {
        [JsonProperty("productname")]
        public string ProductName { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("previewimage")]
        public string PreviewImage { get; set; }
        [JsonProperty("url")]
        public string URL { get; set; }
        [JsonProperty("priceinusd")]
        public decimal PriceInUSD { get; set; }
    }

    public class HotProductsResponse
    {
        [JsonProperty("hotproducts")]
        public List<HotProduct> HotProducts { get; set; }
    }
}
