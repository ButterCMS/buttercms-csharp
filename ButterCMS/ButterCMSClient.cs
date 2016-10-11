using ButterCMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ButterCMS
{
    public class ButterCMSClient
    {
        private string authToken;
        private HttpClient httpClient;
        private TimeSpan defaultTimeout = new TimeSpan(0, 0, 10);

        private const string apiBaseAddress = "https://api.buttercms.com/";

        private const string listPostsEndpoint = "v2/posts";
        private const string retrievePostEndpoint = "v2/posts/{0}";
        private const string searchPostsEndpoint = "v2/search";
        private const string listAuthorsEndpoint = "v2/authors";
        private const string retrieveAuthorEndpoint = "v2/authors/{0}";
        private const string listCategoriesEndpoint = "v2/categories";
        private const string retrieveCategoryEndpoint = "v2/categories/{0}";
        private const string rssFeedEndpoint = "v2/feeds/rss";
        private const string atomEndpoint = "v2/feeds/atom";
        private const string siteMapEndpoint = "v2/feeds/sitemap";
        private const string contentEndpoint = "v2/content";
        private const int defaultPageSize = 10;
        private string authTokenParam
        {
            get
            {
                return string.Format("auth_token={0}", System.Net.WebUtility.UrlEncode(authToken));
            }
        }
        private JsonSerializerSettings serializerSettings;

        public ButterCMSClient(string authToken, TimeSpan? timeOut = null)
        {
            httpClient = new HttpClient
            {
                Timeout = timeOut ?? defaultTimeout,
                BaseAddress = new Uri(apiBaseAddress)
            };
            this.authToken = authToken;

            serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new SnakeCaseContractResolver();
        }

        public PostsResponse ListPosts(int page = 1, int pageSize = defaultPageSize, bool excludeBody = false, string authorSlug = null, string categorySlug = null)
        {
            var queryString = ParseListPostsParams(page, pageSize, excludeBody, authorSlug, categorySlug);
            var postsResponse = JsonConvert.DeserializeObject<PostsResponse>(Execute(queryString), serializerSettings);
            return postsResponse;
        }

        public async Task<PostsResponse> ListPostsAsync(int page = 1, int pageSize = defaultPageSize, bool excludeBody = false, string authorSlug = null, string categorySlug = null)
        {
            var queryString = ParseListPostsParams(page, pageSize, excludeBody, authorSlug, categorySlug);
            var postsResponse = JsonConvert.DeserializeObject<PostsResponse>(await ExecuteAsync(queryString), serializerSettings);
            return postsResponse;
        }

        private string ParseListPostsParams(int page = 1, int pageSize = defaultPageSize, bool excludeBody = false, string authorSlug = null, string categorySlug = null)
        {
            var queryString = new StringBuilder();
            queryString.Append(listPostsEndpoint);
            queryString.Append("?");
            queryString.Append(authTokenParam);

            if (page > 1)
            {
                queryString.Append(string.Format("&page={0}", page));
            }

            if (pageSize != defaultPageSize)
            {
                queryString.Append(string.Format("&page_size={0}", pageSize));
            }

            if (excludeBody)
            {
                queryString.Append(string.Format("&exclude_body={0}", excludeBody));
            }

            if (!string.IsNullOrEmpty(authorSlug))
            {
                queryString.Append(string.Format("&author_slug={0}", authorSlug));
            }

            if (!string.IsNullOrEmpty(categorySlug))
            {
                queryString.Append(string.Format("&category_slug={0}", categorySlug));
            }
            return queryString.ToString();
        }
        
        public PostResponse RetrievePost(string postSlug)
        {
            var queryString = new StringBuilder();
            queryString.Append(string.Format(retrievePostEndpoint, postSlug));
            queryString.Append("?");
            queryString.Append(authTokenParam);
            var postResponse = JsonConvert.DeserializeObject<PostResponse>(Execute(queryString.ToString()), serializerSettings);
            return postResponse;
        }

        public async Task<PostResponse> RetrievePostAsync(string postSlug)
        {
            var queryString = new StringBuilder();
            queryString.Append(string.Format(retrievePostEndpoint, postSlug));
            queryString.Append("?");
            queryString.Append(authTokenParam);
            var postResponse = JsonConvert.DeserializeObject<PostResponse>(await ExecuteAsync(queryString.ToString()), serializerSettings);
            return postResponse;
        }

        public PostsResponse SearchPosts(string query, int page = 1, int pageSize = defaultPageSize)
        {
            var queryString = ParseSearchPostsParams(query, page, pageSize);
            var postsResponse = JsonConvert.DeserializeObject<PostsResponse>(Execute(queryString), serializerSettings);
            return postsResponse.Data.Any() ? postsResponse : null;
        }

        public async Task<PostsResponse> SearchPostsAsync(string query, int page = 1, int pageSize = defaultPageSize)
        {
            var queryString = ParseSearchPostsParams(query, page, pageSize);
            var postsResponse = JsonConvert.DeserializeObject<PostsResponse>(await ExecuteAsync(queryString), serializerSettings);
            return postsResponse.Data.Any() ? postsResponse : null;
        }

        private string ParseSearchPostsParams(string query, int page = 1, int pageSize = defaultPageSize)
        {
            var queryString = new StringBuilder();
            queryString.Append(searchPostsEndpoint);
            queryString.Append("?");
            queryString.Append(authTokenParam);

            if (!string.IsNullOrEmpty(query))
            {
                queryString.Append(string.Format("&query={0}", System.Net.WebUtility.UrlEncode((query))));
            }

            if (page > 1)
            {
                queryString.Append(string.Format("&page={0}", page));
            }

            if (pageSize != defaultPageSize)
            {
                queryString.Append(string.Format("&page_size={0}", pageSize));
            }

            return queryString.ToString();
        }

        public IEnumerable<Author> ListAuthors(bool includeRecentPosts = false)
        {
            var queryString = new StringBuilder();
            queryString.Append(listAuthorsEndpoint);
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (includeRecentPosts)
            {
                queryString.Append("&include=recent_posts");
            }
            var response = JsonConvert.DeserializeObject<AuthorsResponse>(Execute(queryString.ToString()), serializerSettings);
            return response.Data ?? null;
        }

        public async Task<IEnumerable<Author>> ListAuthorsAsync(bool includeRecentPosts = false)
        {
            var queryString = new StringBuilder();
            queryString.Append(listAuthorsEndpoint);
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (includeRecentPosts)
            {
                queryString.Append("&include=recent_posts");
            }
            var response = JsonConvert.DeserializeObject<AuthorsResponse>(await ExecuteAsync(queryString.ToString()), serializerSettings);
            return response.Data ?? null;
        }
        
        public Author RetrieveAuthor(string authorSlug, bool includeRecentPosts = false)
        {
            var queryString = new StringBuilder();
            queryString.Append(string.Format(retrieveAuthorEndpoint, authorSlug));
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (includeRecentPosts)
            {
                queryString.Append("&include=recent_posts");
            }
            var response = JsonConvert.DeserializeObject<AuthorResponse>(Execute(queryString.ToString()), serializerSettings);
            if(response != null && response.Data != null)
            {
                return response.Data;
            }
            return null;
        }

        public async Task<Author> RetrieveAuthorAsync(string authorSlug, bool includeRecentPosts = false)
        {
            var queryString = new StringBuilder();
            queryString.Append(string.Format(retrieveAuthorEndpoint, authorSlug));
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (includeRecentPosts)
            {
                queryString.Append("&include=recent_posts");
            }
            var response = JsonConvert.DeserializeObject<AuthorResponse>(await ExecuteAsync(queryString.ToString()), serializerSettings);
            if (response != null && response.Data != null)
            {
                return response.Data;
            }
            return null;
        }

        public IEnumerable<Category> ListCategories(bool includeRecentPosts = false)
        {
            var queryString = new StringBuilder();
            queryString.Append(listCategoriesEndpoint);
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (includeRecentPosts)
            {
                queryString.Append("&include=recent_posts");
            }
            var response = JsonConvert.DeserializeObject<CategoriesResponse>(Execute(queryString.ToString()), serializerSettings);
            return response.Data ?? null;
        }

        public async Task<IEnumerable<Category>> ListCategoriesAsync(bool includeRecentPosts = false)
        {
            var queryString = new StringBuilder();
            queryString.Append(listCategoriesEndpoint);
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (includeRecentPosts)
            {
                queryString.Append("&include=recent_posts");
            }
            var response = JsonConvert.DeserializeObject<CategoriesResponse>(await ExecuteAsync(queryString.ToString()), serializerSettings);
            return response.Data ?? null;
        }

        public Category RetrieveCategory(string categorySlug, bool includeRecentPosts = false)
        {
            var queryString = new StringBuilder();
            queryString.Append(string.Format(retrieveCategoryEndpoint, categorySlug));
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (includeRecentPosts)
            {
                queryString.Append("&include=recent_posts");
            }
            var response = JsonConvert.DeserializeObject<CategoryResponse>(Execute(queryString.ToString()), serializerSettings);
            if (response != null && response.Data != null)
            {
                return response.Data;
            }
            return null;
        }

        public async Task<Category> RetrieveCategoryAsync(string categorySlug, bool includeRecentPosts = false)
        {
            var queryString = new StringBuilder();
            queryString.Append(string.Format(retrieveCategoryEndpoint, categorySlug));
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (includeRecentPosts)
            {
                queryString.Append("&include=recent_posts");
            }
            var response = JsonConvert.DeserializeObject<CategoryResponse>(await ExecuteAsync(queryString.ToString()), serializerSettings);
            if (response != null && response.Data != null)
            {
                return response.Data;
            }
            return null;
        }

        public XmlDocument GetRSSFeed()
        {
            var request = rssFeedEndpoint + "?" + authTokenParam;
            var response = JsonConvert.DeserializeObject<XmlAPIResponse>(Execute(request), serializerSettings);
            if (response != null && response.Data != null)
            {
                var xml = new XmlDocument();
                xml.LoadXml(response.Data);
                return xml;
            }
            return null;
        }

        public async Task<XmlDocument> GetRSSFeedAsync()
        {
            var request = rssFeedEndpoint + "?" + authTokenParam;
            var response = JsonConvert.DeserializeObject<XmlAPIResponse>(await ExecuteAsync(request), serializerSettings);
            if (response != null && response.Data != null)
            {
                var xml = new XmlDocument();
                xml.LoadXml(response.Data);
                return xml;
            }
            return null;
        }

        public XmlDocument GetAtomFeed()
        {
            var request = atomEndpoint + "?" + authTokenParam;
            var response = JsonConvert.DeserializeObject<XmlAPIResponse>(Execute(request), serializerSettings);
            if (response != null && response.Data != null)
            {
                var xml = new XmlDocument();
                xml.LoadXml(response.Data);
                return xml;
            }
            return null;
        }

        public async Task<XmlDocument> GetAtomFeedAsync()
        {
            var request = atomEndpoint + "?" + authTokenParam;
            var response = JsonConvert.DeserializeObject<XmlAPIResponse>(await ExecuteAsync(request), serializerSettings);
            if (response != null && response.Data != null)
            {
                var xml = new XmlDocument();
                xml.LoadXml(response.Data);
                return xml;
            }
            return null;
        }

        public XmlDocument GetSitemap()
        {
            var request = siteMapEndpoint + "?" + authTokenParam;
            var response = JsonConvert.DeserializeObject<XmlAPIResponse>(Execute(request), serializerSettings);
            if (response != null && response.Data != null)
            {
                var xml = new XmlDocument();
                xml.LoadXml(response.Data);
                return xml;
            }
            return null;
        }

        public async Task<XmlDocument> GetSitemapAsync()
        {
            var queryString = siteMapEndpoint + "?" + authTokenParam;
            try
            {
                var response = JsonConvert.DeserializeObject<XmlAPIResponse>(await ExecuteAsync(queryString), serializerSettings);
                var xml = new XmlDocument();
                xml.LoadXml(response.Data);
                return xml;
            }
            catch (InvalidKeyException invalidKeyException)
            {
                throw invalidKeyException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RetrieveContentFieldsJSON(string[] keys)
        {
            var keysQueryString = string.Join(",", keys);
            var queryString = string.Format("{0}?{1}&keys={2}", contentEndpoint, authTokenParam, keysQueryString);
            var contentFields = Execute(queryString);
            return contentFields;
        }

        public async Task<string> RetrieveContentFieldsJSONAsync(string[] keys)
        {
            var keysQueryString = string.Join(",", keys);
            var queryString = string.Format("{0}?{1}&keys={2}", contentEndpoint, authTokenParam, keysQueryString);
            var contentFields = await ExecuteAsync(queryString);
            return contentFields;
        }
        
        private string Execute(string queryString)
        {
            try
            {
                var response = httpClient.GetAsync(queryString).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new InvalidKeyException("No valid API key provided.");
                }
                if (response.StatusCode >= System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception("There is a problem with the ButterCMS service");
                }
            }
            catch (TaskCanceledException taskException)
            {
                if (!taskException.CancellationToken.IsCancellationReques‌​ted)
                {
                    throw new Exception("Timeout expired trying to reach the ButterCMS service.");
                }
                throw taskException;
            }
            catch (HttpRequestException httpException)
            {
                throw httpException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return string.Empty;
        }

        private async Task<string> ExecuteAsync(string queryString)
        {
            try
            {
                var response = await httpClient.GetAsync(queryString);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new InvalidKeyException("No valid API key provided.");
                }
                if (response.StatusCode >= System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception("There is a problem with the ButterCMS service");
                }
            }
            catch (TaskCanceledException taskException)
            {
                if (!taskException.CancellationToken.IsCancellationReques‌​ted)
                {
                    throw new Exception("Timeout expired trying to reach the ButterCMS service.");
                }
                throw taskException;
            }
            catch (HttpRequestException httpException)
            {
                throw httpException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return string.Empty;
        }
    }
}
