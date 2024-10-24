using System;
using System.Collections.Generic;

namespace ButterCMS.Models
{
    public class Post
    {
        public string Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Published { get; set; }
        public Author Author { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public string FeaturedImage { get; set; }
        public string FeaturedImageAlt { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Summary { get; set; }
        public string SeoTitle { get; set; }
        public string MetaDescription { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime Scheduled { get; set; }
    }
}

