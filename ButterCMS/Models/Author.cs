using System.Collections.Generic;

namespace ButterCMS.Models
{
    public class Author
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Slug { get; set; }
        public string Bio { get; set; }
        public string Title { get; set; }
        public string LinkedinUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string PinterestUrl { get; set; }
        public string TwitterHandle { get; set; }
        public string ProfileImage { get; set; }
        public IEnumerable<Post> RecentPosts { get; set; }
    }
}
