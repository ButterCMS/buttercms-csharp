using System.Collections.Generic;

namespace ButterCMS.Models
{
    public class PostsResponse
    {
        public PostsMeta Meta { get; set; }
        public IEnumerable<Post> Data { get; set; }
    }
}
