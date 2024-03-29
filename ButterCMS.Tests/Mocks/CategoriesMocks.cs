﻿using ButterCMS.Models;

namespace ButterCMS.Tests
{
    public static class CategoriesMocks
    {
        public static Category Category = new Category()
        {
            Name = "Category",
            Slug = "category",
        };

        public static Category CategoryWithPosts = new Category()
        {
            Name = "Category",
            Slug = "category",
            RecentPosts = new[] { new Post() { Slug = "post-1" }, new Post() { Slug = "post-2" } }
        };

        public static CategoriesResponse CategoriesResponse = new CategoriesResponse()
        {
            Data = new[] { Category },
        };

        public static CategoriesResponse CategoriesResponseWithPosts = new CategoriesResponse()
        {
            Data = new[] { CategoryWithPosts },
        };

        public static CategoryResponse CategoryResponse = new CategoryResponse()
        {
            Data = Category,
        };

        public static CategoryResponse CategoryResponseWithPosts = new CategoryResponse()
        {
            Data = CategoryWithPosts,
        };

        public static void MockSuccessfullCategoriesResponse(this ButterCMSClientWithMockedHttp butterClient)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/categories/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", CategoriesResponse);
        }

        public static void MockSuccessfullCategoriesResponseWithPosts(this ButterCMSClientWithMockedHttp butterClient)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/categories/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}&include=recent_posts", CategoriesResponseWithPosts);
        }

        public static void MockSuccessfullCategoryResponse(this ButterCMSClientWithMockedHttp butterClient, string slug)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/categories/{slug}/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", CategoryResponse);
        }

        public static void MockSuccessfullCategoryResponseWithPosts(this ButterCMSClientWithMockedHttp butterClient, string slug)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/categories/{slug}/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}&include=recent_posts", CategoryResponseWithPosts);
        }

        public static void MockSuccessfullNullCategoryResponse(this ButterCMSClientWithMockedHttp butterClient, string slug)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/categories/{slug}/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", new CategoryResponse());
        }
    }
}
