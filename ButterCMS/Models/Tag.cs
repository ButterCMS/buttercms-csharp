using System.Collections.Generic;

namespace ButterCMS.Models
{
    public class Tag
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public IEnumerable<Post> RecentPosts { get; set; }
    }
}
