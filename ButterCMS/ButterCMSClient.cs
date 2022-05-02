using ButterCMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace ButterCMS
{
    public class ButterCMSClient
    {
        private static readonly string generalExMsg = "There is a problem with the ButterCMS service";

        private string authToken;
        private HttpClient httpClient;
        private TimeSpan defaultTimeout = new TimeSpan(0, 0, 10);
        private int maxRequestTries;

        private const string apiBaseAddress = "https://api.buttercms.com/";

        private const string listPostsEndpoint = "v2/posts/";
        private const string retrievePostEndpoint = "v2/posts/{0}/";
        private const string searchPostsEndpoint = "v2/search/";
        private const string listAuthorsEndpoint = "v2/authors/";
        private const string retrieveAuthorEndpoint = "v2/authors/{0}/";
        private const string listCategoriesEndpoint = "v2/categories/";
        private const string listTagsEndpoint = "v2/tags/";
        private const string retrieveCategoryEndpoint = "v2/categories/{0}/";
        private const string retrieveTagEndpoint = "v2/tags/{0}/";
        private const string rssFeedEndpoint = "v2/feeds/rss/";
        private const string atomEndpoint = "v2/feeds/atom/";
        private const string siteMapEndpoint = "v2/feeds/sitemap/";
        private const string contentEndpoint = "v2/content/";
        private const string listPagesEndpoint = "v2/pages/{0}/";
        private const string retrievePageEndpoint = "v2/pages/{0}/{1}/";
        private const int defaultPageSize = 10;
        private string authTokenParam
        {
            get
            {
                return string.Format("auth_token={0}", System.Net.WebUtility.UrlEncode(authToken));
            }
        }
        private JsonSerializerSettings serializerSettings;

        public ButterCMSClient(string authToken, TimeSpan? timeOut = null, int maxRequestTries = 3, HttpMessageHandler httpMessageHandler = null)
        {
#if (NET45 || NET451 || NET452 || NET46 || NET461 || NET462)
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#endif

            httpClient = new HttpClient(httpMessageHandler ?? new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            })
            {
                Timeout = timeOut ?? defaultTimeout,
                BaseAddress = new Uri(apiBaseAddress)
            };
            httpClient.DefaultRequestHeaders.Add("X-Butter-Client", ".NET/" + typeof(ButterCMSClient).GetTypeInfo().Assembly.GetName().Version.ToString());
            this.maxRequestTries = maxRequestTries;
            this.authToken = authToken;

            serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new SnakeCaseContractResolver();
        }

        public PostsResponse ListPosts(int page = 1, int pageSize = defaultPageSize, bool excludeBody = false, string authorSlug = null, string categorySlug = null, string tagSlug = null)
        {
            var queryString = ParseListPostsParams(page, pageSize, excludeBody, authorSlug, categorySlug, tagSlug);
            var postsResponse = JsonConvert.DeserializeObject<PostsResponse>(Execute(queryString), serializerSettings);
            return postsResponse;
        }

        public async Task<PostsResponse> ListPostsAsync(int page = 1, int pageSize = defaultPageSize, bool excludeBody = false, string authorSlug = null, string categorySlug = null, string tagSlug = null)
        {
            var queryString = ParseListPostsParams(page, pageSize, excludeBody, authorSlug, categorySlug, tagSlug);
            var postsResponse = JsonConvert.DeserializeObject<PostsResponse>(await ExecuteAsync(queryString), serializerSettings);
            return postsResponse;
        }

        private string ParseListPostsParams(int page = 1, int pageSize = defaultPageSize, bool excludeBody = false, string authorSlug = null, string categorySlug = null, string tagSlug = null)
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

            if (!string.IsNullOrEmpty(tagSlug))
            {
                queryString.Append(string.Format("&tag_slug={0}", tagSlug));
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

        public IEnumerable<Tag> ListTags(bool includeRecentPosts = false)
        {
            var queryString = new StringBuilder();
            queryString.Append(listTagsEndpoint);
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (includeRecentPosts)
            {
                queryString.Append("&include=recent_posts");
            }
            var response = JsonConvert.DeserializeObject<TagsResponse>(Execute(queryString.ToString()), serializerSettings);
            return response.Data ?? null;
        }

        public async Task<IEnumerable<Tag>> ListTagsAsync(bool includeRecentPosts = false)
        {
            var queryString = new StringBuilder();
            queryString.Append(listTagsEndpoint);
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (includeRecentPosts)
            {
                queryString.Append("&include=recent_posts");
            }
            var response = JsonConvert.DeserializeObject<TagsResponse>(await ExecuteAsync(queryString.ToString()), serializerSettings);
            return response.Data ?? null;
        }

        public Tag RetrieveTag(string tagSlug, bool includeRecentPosts = false)
        {
            var queryString = new StringBuilder();
            queryString.Append(string.Format(retrieveTagEndpoint, tagSlug));
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (includeRecentPosts)
            {
                queryString.Append("&include=recent_posts");
            }
            var response = JsonConvert.DeserializeObject<TagResponse>(Execute(queryString.ToString()), serializerSettings);
            if (response != null && response.Data != null)
            {
                return response.Data;
            }
            return null;
        }

        public async Task<Tag> RetrieveTagAsync(string tagSlug, bool includeRecentPosts = false)
        {
            var queryString = new StringBuilder();
            queryString.Append(string.Format(retrieveTagEndpoint, tagSlug));
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (includeRecentPosts)
            {
                queryString.Append("&include=recent_posts");
            }
            var response = JsonConvert.DeserializeObject<TagResponse>(await ExecuteAsync(queryString.ToString()), serializerSettings);
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

        public string RetrieveContentFieldsJSON(string[] keys, Dictionary<string, string> parameterDictionary = null)
        {
            var keysQueryString = string.Join(",", keys);
            var queryString = new StringBuilder();
            queryString.Append(string.Format("{0}?{1}&keys={2}", contentEndpoint, authTokenParam, keysQueryString));
            if (parameterDictionary != null && parameterDictionary.Count > 0)
            {
                foreach (var parameterPair in parameterDictionary)
                {
                    queryString.Append("&");
                    queryString.Append(parameterPair.Key);
                    queryString.Append("=");
                    queryString.Append(parameterPair.Value);
                }
            }
            var contentFields = Execute(queryString.ToString());
            return contentFields;
        }

        public async Task<string> RetrieveContentFieldsJSONAsync(string[] keys, Dictionary<string, string> parameterDictionary = null)
        {
            var keysQueryString = string.Join(",", keys);
            var queryString = new StringBuilder();
            queryString.Append(string.Format("{0}?{1}&keys={2}", contentEndpoint, authTokenParam, keysQueryString));
            if (parameterDictionary != null && parameterDictionary.Count > 0)
            {
                foreach (var parameterPair in parameterDictionary)
                {
                    queryString.Append("&");
                    queryString.Append(parameterPair.Key);
                    queryString.Append("=");
                    queryString.Append(parameterPair.Value);
                }
            }
            var contentFields = await ExecuteAsync(queryString.ToString());
            return contentFields;
        }

        public T RetrieveContentFields<T>(string[] keys, Dictionary<string, string> parameterDictionary = null) where T : class
        {
            var keysQueryString = string.Join(",", keys);
            var queryString = new StringBuilder();
            queryString.Append(string.Format("{0}?{1}&keys={2}", contentEndpoint, authTokenParam, keysQueryString));
            if (parameterDictionary != null && parameterDictionary.Count > 0)
            {
                foreach (var parameterPair in parameterDictionary)
                {
                    queryString.Append("&");
                    queryString.Append(parameterPair.Key);
                    queryString.Append("=");
                    queryString.Append(parameterPair.Value);
                }
            }
            var contentFieldsJSON = Execute(queryString.ToString());
            try
            {
                var contentFields = JsonConvert.DeserializeObject<ContentResponse<T>>(contentFieldsJSON);
                return contentFields.Data;
            }
            catch
            {
                throw new ContentFieldObjectMismatchException(string.Format("The Butter library was unable to deserialize the response object to the given object. Response from server: {0}", contentFieldsJSON));
            }
        }

        public async Task<T> RetrieveContentFieldsAsync<T>(string[] keys, Dictionary<string, string> parameterDictionary = null) where T : class
        {
            var keysQueryString = string.Join(",", keys);
            var queryString = new StringBuilder();
            queryString.Append(string.Format("{0}?{1}&keys={2}", contentEndpoint, authTokenParam, keysQueryString));
            if (parameterDictionary != null && parameterDictionary.Count > 0)
            {
                foreach (var parameterPair in parameterDictionary)
                {
                    queryString.Append("&");
                    queryString.Append(parameterPair.Key);
                    queryString.Append("=");
                    queryString.Append(parameterPair.Value);
                }
            }
            var contentFieldsJSON = await ExecuteAsync(queryString.ToString());
            try
            {
                var contentFields = JsonConvert.DeserializeObject<ContentResponse<T>>(contentFieldsJSON);
                return contentFields.Data;
            }
            catch
            {
                throw new ContentFieldObjectMismatchException(string.Format("The Butter library was unable to deserialize the response object to the given object. Response from server: {0}", contentFieldsJSON));
            }
        }

        public PagesResponse<T> ListPages<T>(string pageType, Dictionary<string, string> parameterDictionary = null) where T : class
        {
            var queryString = new StringBuilder();
            queryString.Append(string.Format(listPagesEndpoint, pageType));
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (parameterDictionary != null && parameterDictionary.Count > 0)
            {
                foreach (var parameterPair in parameterDictionary)
                {
                    queryString.Append("&");
                    queryString.Append(parameterPair.Key);
                    queryString.Append("=");
                    queryString.Append(parameterPair.Value);
                }
            }
            var listPagesJSON = Execute(queryString.ToString());
            try
            {
                var response = JsonConvert.DeserializeObject<PagesResponse<T>>(listPagesJSON);
                return response;
            }
            catch
            {
                throw new PagesObjectMismatchException(string.Format("The Butter library was unable to deserialize the response object to the given object. Response from server: {0}", listPagesJSON));
            }
            
        }
        public async Task<PagesResponse<T>> ListPagesAsync<T>(string pageType, Dictionary<string, string> parameterDictionary = null) where T : class
        {
            var queryString = new StringBuilder();
            queryString.Append(string.Format(listPagesEndpoint, pageType));
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (parameterDictionary != null && parameterDictionary.Count > 0)
            {
                foreach (var parameterPair in parameterDictionary)
                {
                    queryString.Append("&");
                    queryString.Append(parameterPair.Key);
                    queryString.Append("=");
                    queryString.Append(parameterPair.Value);
                }
            }
            var listPagesJSON = await ExecuteAsync(queryString.ToString());
            try
            {
                var response = JsonConvert.DeserializeObject<PagesResponse<T>>(listPagesJSON);
                return response;
            }
            catch
            {
                throw new PagesObjectMismatchException(string.Format("The Butter library was unable to deserialize the response object to the given object. Response from server: {0}", listPagesJSON));
            }
        }

        public PageResponse<T> RetrievePage<T>(string pageType, string pageSlug, Dictionary<string, string> parameterDictionary = null) where T : class
        {
            var queryString = new StringBuilder();
            queryString.Append(string.Format(retrievePageEndpoint, pageType, pageSlug));
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (parameterDictionary != null && parameterDictionary.Count > 0)
            {
                foreach (var parameterPair in parameterDictionary)
                {
                    queryString.Append("&");
                    queryString.Append(parameterPair.Key);
                    queryString.Append("=");
                    queryString.Append(parameterPair.Value);
                }
            }
            var retrievePageJSON = Execute(queryString.ToString());
            try
            {
                var response = JsonConvert.DeserializeObject<PageResponse<T>>(retrievePageJSON);
                return response;
            }
            catch
            {
                throw new PagesObjectMismatchException(string.Format("The Butter library was unable to deserialize the response object to the given object. Response from server: {0}", retrievePageJSON));
            }
        }

        public async Task<PageResponse<T>> RetrievePageAsync<T>(string pageType, string pageSlug, Dictionary<string, string> parameterDictionary = null) where T : class
        {
            var queryString = new StringBuilder();
            queryString.Append(string.Format(retrievePageEndpoint, pageType, pageSlug));
            queryString.Append("?");
            queryString.Append(authTokenParam);
            if (parameterDictionary != null && parameterDictionary.Count > 0)
            {
                foreach (var parameterPair in parameterDictionary)
                {
                    queryString.Append("&");
                    queryString.Append(parameterPair.Key);
                    queryString.Append("=");
                    queryString.Append(parameterPair.Value);
                }
            }
            var retrievePageJSON = await ExecuteAsync(queryString.ToString());
            try
            {
                var response = JsonConvert.DeserializeObject<PageResponse<T>>(retrievePageJSON);
                return response;
            }
            catch
            {
                throw new PagesObjectMismatchException(string.Format("The Butter library was unable to deserialize the response object to the given object. Response from server: {0}", retrievePageJSON));
            }
        }

        private string Execute(string queryString)
        {
            var remainingTries = maxRequestTries;
            var exceptions = new List<Exception>();

            do
            {
                --remainingTries;
                try
                {
                    return ExecuteSingle(queryString);
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }
            while (remainingTries > 0);

            throw aggregateExceptions(exceptions);
        }

        private string ExecuteSingle(string queryString)
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
                    throw new Exception(generalExMsg);
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
            var remainingTries = maxRequestTries;
            var exceptions = new List<Exception>();

            do
            {
                --remainingTries;
                try
                {
                    return await ExecuteSingleAsync(queryString);
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }
            while (remainingTries > 0);

            throw aggregateExceptions(exceptions);
        }

        private async Task<string> ExecuteSingleAsync(string queryString)
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
                    throw new Exception(generalExMsg);
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

        private Exception aggregateExceptions(List<Exception> exceptions)
        {
            // If we somehow managed to fail all the requests without getting any exceptions,
            // return a general exception. This shouldn't be possible.
            if (!exceptions.Any())
                return new Exception(generalExMsg);
            
            var uniqueExceptions = exceptions.Distinct(new ExceptionEqualityComparer());

            // If all the requests failed with the same exception (should be the case most of the time), 
            // just return one exception to represent them all.
            if (uniqueExceptions.Count() == 1)
                return uniqueExceptions.First();

            // If all the requests failed but for different reasons, return an AggregateException 
            // with all the root-cause exceptions.
            return new AggregateException(generalExMsg, uniqueExceptions);
        }

        /// <summary>
        /// Used to aggregate exceptions that occur on request retries. 
        /// </summary>
        /// <remarks>
        /// In most cases, the same exception will occur multiple times, 
        /// but we don't want to return multiple copies of it. This class is used 
        /// to find exceptions that are duplicates by type and message so we can
        /// only return one of them.
        /// </remarks>
        private class ExceptionEqualityComparer : IEqualityComparer<Exception>
        {
            public bool Equals(Exception e1, Exception e2)
            {
                if (e2 == null && e1 == null)
                    return true;
                else if (e1 == null | e2 == null)
                    return false;
                else if (e1.GetType().Name.Equals(e2.GetType().Name) && e1.Message.Equals(e2.Message))
                    return true;
                else
                    return false;
            }

            public int GetHashCode(Exception e)
            {
                return (e.GetType().Name + e.Message).GetHashCode();
            }
        }

    }

}
