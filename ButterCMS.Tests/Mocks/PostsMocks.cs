using System;
using ButterCMS.Models;

namespace ButterCMS.Tests
{
    public static class PostsMocks
    {
        public static Post Post = new Post()
        {
            Slug = "post",
            Title = "Post",
            Body = "<p>Post body</p>",
            Author = new Author() { Slug = "author" },
            Categories = new[] { new Category() { Slug = "category" } },
            Tags = new[] { new Tag() { Slug = "tag" } },
        };
        
        public static PostsResponse PostsResponse = new PostsResponse()
        {
            Data = new[] 
            { 
                Post, 
                new Post() { Slug = "post-2" }
            },
            Meta = new PostsMeta()
            {
                Count = 2,
                NextPage = 2,
                PreviousPage = null,
            }
        };

        public static string AppendParamsToListPostsUrl(int? page = null, int? pageSize = null, bool? excludeBody = null, string authorSlug = null, string categorySlug = null, string tagSlug = null)
        {
            var url = $"https://api.buttercms.com/v2/posts/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}";

            if (page.HasValue && page.Value > 1)
            {
                url += $"&page={page.Value}";
            }

            if (pageSize.HasValue)
            {
                url += $"&page_size={pageSize.Value}";
            }

            if (excludeBody.HasValue)
            {
                url += $"&exclude_body={excludeBody.Value}";
            }

            if (!string.IsNullOrEmpty(authorSlug))
            {
                url += $"&author_slug={authorSlug}";
            }

            if (!string.IsNullOrEmpty(categorySlug))
            {
                url += $"&category_slug={categorySlug}";
            }

            if (!string.IsNullOrEmpty(tagSlug))
            {
                url += $"&tag_slug={tagSlug}";
            }

            Console.WriteLine(url);

            return url;
        }

        public static void MockSuccessfullPostsResponse(this ButterCMSClientWithMockedHttp butterClient, int? page = null, int? pageSize = null, bool? excludeBody = null, string authorSlug = null, string categorySlug = null, string tagSlug = null)
        {
            butterClient.MockSuccessfullJSONResponse(AppendParamsToListPostsUrl(page, pageSize, excludeBody, authorSlug, categorySlug, tagSlug), PostsResponse);
        }

        public static void MockSuccessfullEmptyPostsResponse(this ButterCMSClientWithMockedHttp butterClient, int? page = null, string authorSlug = null)
        {
            butterClient.MockSuccessfullJSONResponse(AppendParamsToListPostsUrl(page, authorSlug: authorSlug), new PostsResponse()
            {
                Data = new Post[] { },
                Meta = new PostsMeta()
                {
                    Count = 0,
                    NextPage = null,
                    PreviousPage = null,
                }
            });
        }
    }
}
