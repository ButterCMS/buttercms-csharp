using ButterCMS.Models;

namespace ButterCMS.Tests
{
    public static class AuthorsMocks
    {
        public static Author Author = new Author()
        {
            FirstName = "John",
            LastName = "Doe",
            Slug = "john-doe",
        };

        public static Author AuthorWithPosts = new Author()
        {
            FirstName = "John",
            LastName = "Doe",
            Slug = "john-doe",
            RecentPosts = new[] { new Post() { Slug = "post-1" }, new Post() { Slug = "post-2" } }
        };
        
        public static AuthorsResponse AuthorsResponse = new AuthorsResponse()
        {
            Data = new[] { Author },
        };

        public static AuthorsResponse AuthorsResponseWithPosts = new AuthorsResponse()
        {
            Data = new[] { AuthorWithPosts },
        };
        
        public static AuthorResponse AuthorResponse = new AuthorResponse()
        {
            Data = Author,
        };

        public static AuthorResponse AuthorResponseWithPosts = new AuthorResponse()
        {
            Data = AuthorWithPosts,
        };

        public static void MockSuccessfullAuthorsResponse(this ButterCMSClientWithMockedHttp butterClient)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/authors/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", AuthorsResponse);
        }

        public static void MockSuccessfullAuthorsResponseWithPosts(this ButterCMSClientWithMockedHttp butterClient)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/authors/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}&include=recent_posts", AuthorsResponseWithPosts);
        }

        public static void MockSuccessfullAuthorResponse(this ButterCMSClientWithMockedHttp butterClient, string slug)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/authors/{slug}/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", AuthorResponse);
        }

        public static void MockSuccessfullAuthorResponseWithPosts(this ButterCMSClientWithMockedHttp butterClient, string slug)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/authors/{slug}/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}&include=recent_posts", AuthorResponseWithPosts);
        }

        public static void MockSuccessfullNullAuthorResponse(this ButterCMSClientWithMockedHttp butterClient, string slug) 
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/authors/{slug}/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", new AuthorResponse());
        }
    }
}
