using ButterCMS.Models;

namespace ButterCMS.Tests
{
    public static class TagsMocks
    {
        public static Tag Tag = new Tag()
        {
            Slug = "tag",
            Name = "Tag",
        };

        public static Tag TagWithPosts = new Tag()
        {
            Slug = "tag",
            Name = "Tag",
            RecentPosts = new[] { PostsMocks.Post }
        };

        public static TagsResponse TagsResponse = new TagsResponse()
        {
            Data = new[] { Tag },
        };

        public static TagsResponse TagsResponseWithPosts = new TagsResponse()
        {
            Data = new[] { TagWithPosts },
        };

        public static void MockSuccessfullTagsResponse(this ButterCMSClientWithMockedHttp butterClient)
        {
            var tags = new[] { Tag };
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/tags/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", TagsResponse);
        }

        public static void MockSuccessfullTagsWithPostsResponse(this ButterCMSClientWithMockedHttp butterClient)
        {
            var tags = new[] { TagWithPosts };
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/tags/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}&include=recent_posts", TagsResponseWithPosts);
        }
    }
}
