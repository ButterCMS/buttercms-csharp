using System.Net;
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

        public static PostResponse PostResponse = new PostResponse()
        {
            Data = Post,
            Meta = new PostMeta()
            {
                NextPost = new PostLight() { Slug = "next-post"},
                PreviousPost = new PostLight() { Slug = "previous-post" },
            }
        };

        public static string AppendParamsToListPostsUrl(int? page = null, int? pageSize = null, bool? excludeBody = null, string authorSlug = null, string categorySlug = null, string tagSlug = null, string query = null)
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

            return url;
        }

        public static string AppendParamsToSearchPostsUrl(int? page = null, int? pageSize = null, string query = null)
        {
            var url = $"https://api.buttercms.com/v2/search/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}";

            if (!string.IsNullOrEmpty(query))
            {
                url += $"&query={WebUtility.UrlEncode(query)}";
            }

            if (page.HasValue && page.Value > 1)
            {
                url += $"&page={page.Value}";
            }

            if (pageSize.HasValue)
            {
                url += $"&page_size={pageSize.Value}";
            }

            return url;
        }

        public static void MockSuccessfullPostsResponse(this ButterCMSClientWithMockedHttp butterClient, int? page = null, int? pageSize = null, bool? excludeBody = null, string authorSlug = null, string categorySlug = null, string tagSlug = null, string query = null)
        {
            butterClient.MockSuccessfullJSONResponse(AppendParamsToListPostsUrl(page, pageSize, excludeBody, authorSlug, categorySlug, tagSlug, query), PostsResponse);
        }

        public static void MockSuccessfullEmptyPostsResponse(this ButterCMSClientWithMockedHttp butterClient, int? page = null, string authorSlug = null, int? pageSize = null, string query = null)
        {
            butterClient.MockSuccessfullJSONResponse(AppendParamsToListPostsUrl(page, authorSlug: authorSlug, pageSize: pageSize, query: query), new PostsResponse()
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

        public static void MockSuccessfullSearchPostsResponse(this ButterCMSClientWithMockedHttp butterClient, int? page = null, int? pageSize = null, string query = null)
        {
            butterClient.MockSuccessfullJSONResponse(AppendParamsToSearchPostsUrl(page, pageSize, query), PostsResponse);
        }

        public static void MockSuccessfullEmptySearchPostsResponse(this ButterCMSClientWithMockedHttp butterClient, int? page = null, int? pageSize = null, string query = null)
        {
            butterClient.MockSuccessfullJSONResponse(AppendParamsToSearchPostsUrl(page, pageSize: pageSize, query: query), new PostsResponse()
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

        public static void MockSuccessfullPostResponse(this ButterCMSClientWithMockedHttp butterClient, string slug)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/posts/{slug}/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", PostResponse);
        }
        
        public static void MockSuccessfullNullPostResponse(this ButterCMSClientWithMockedHttp butterClient, string slug) 
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/posts/{slug}/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", new PostResponse()
            {
                Data = null,
                Meta = null,
            });
        }
    }
}
