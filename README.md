# buttercms-csharp

.NET Library for ButterCMS API. 

## Documentation

For a comprehensive list of examples, check out the [API documentation](https://buttercms.com/docs/api/).


## Installation

To install ButterCMS, run the following command in the [Package Manager Console](https://docs.nuget.org/ndocs/tools/package-manager-console)

```PowerShell
PM> Install-Package ButterCMS
```

## Usage
Getting started with the Butter API is easy! Simply instantiate the ButterCMSClient with your API key. You can optionally provide a custom [TimeSpan](https://msdn.microsoft.com/en-us/library/system.timespan%28v=vs.110%29.aspx) timeout value; the default is 10 seconds. 

```C#
using ButterCMS;
...
var butterClient = new ButterCMSClient("YOUR KEY");
```
If you will be making many Butter API calls, it is recommended to store and re-use the client object. 

Each Butter client method has a synchronous version and an asynchronous version. Asynchronous methods are appended with the word "Async".

## Jump to:

* [Posts](#posts)
* [Authors](#authors)
* [Categories](#categories)
* [Feeds](#feeds)
* [Content Fields](#content-fields)
* [Pages](#pages)
* [Class Definitions](#class-definitions)
* [Exceptions](#exceptions)

## Posts


### List Posts
Listing posts returns a [PostsResponse](#postsresponse-class) object. This object consists of a [metadata](#postsmeta-class) object and IEnumerable&lt;[Post](#post-class)&gt;

#### ListPosts() Parameters:
| Parameter|Default|Description|
| ---|---|---|
| page(optional) | 1 | Used to paginate through older posts. |
| pageSize(optional) | 10 |  Used to set the number of blog posts shown per page. |
| excludeBody(optional) | false | When true, does not return the full post body. Useful for keeping response size down when showing a list of blog posts. |
|authorSlug(optional) | |Filter posts by an author’s slug.|
|categorySlug(optional) | | Filter posts by a category’s slug.

#### Examples:
```C#
PostsResponse posts = butterClient.ListPosts();

PostsResponse filteredPosts = await butterClient.ListPostsAsync(page: 2, pageSize: 5, excludeBody: true, authorSlug: "alice", categorySlug: "dot-net");
```

### Retrieving a Single Post
Retrieving a single Post will return a PostResponse object. This object consists of a single [Post](#post-class) and Post [Metadata](#postmeta-class). Post Metadata offers hints about the Previous and Next posts.

#### RetrievePost() Parameters:
| Parameter|Description|
| ---|---|
| slug|The slug of the post to be retrieved.|

#### Examples:
```C#
PostResponse controversialPost = butterClient.RetrievePost("tabs-vs-spaces-throwdown");

PostResponse safePost = await butterClient.RetrievePostAsync("puppies-and-kittens");

```

### Searching Posts
Searching posts will return the same object as listing posts, [PostsResponse](#postsresponse-class)

#### SearchPosts() Parameters:
| Parameter|Default|Description|
| ---|---|---|
| query |  | Search query |
| page(optional) | 1 | Used to paginate through older posts. |
| pageSize(optional) | 10 |  Used to set the number of blog posts shown per page. |

#### Examples:
```C#
PostsResponse posts = butterClient.SearchPosts("spaceships");

PostsResponse caffeinePosts = await butterClient.SearchPostsAsync(query: "coffee", page: 3, pageSize: 5);

```

## Authors

### List Authors

Listing Authors returns IEnumerable&lt;[Author](#author-class)&gt;

#### ListAuthors() Parameters:
| Parameter|Description|
| ---|---|
| includeRecentPosts(optional)|If true, will return the author's recent posts in the response|

#### Examples:
```C#
IEnumerable<Author> authors = butterClient.ListAuthors();

IEnumerable<Author> authorsWithPosts = await butterClient.ListAuthorsAsync(includeRecentPosts: true);

```

### Retrieve a Single Author
Retrieving an author returns an [Author](#author-class) object

#### RetrieveAuthor() Parameters:
| Parameter|Description|
| ---|---|
|authorSlug|The slug of the author to be retrieved.|
| includeRecentPosts(optional)|If true, will return the author's recent posts in the response|

#### Examples:
```C#
Author sally = butterClient.RetrieveAuthor("sally");

Author tylerAndPosts = await butterClient.RetrieveAuthorAsync(authorSlug: "tyler", includeRecentPosts: true);

```
## Categories
### List Categories
Listing Categories returns IEnumerable&lt;[Category](#category-class)&gt;

#### ListCategories() Parameters
| Parameter|Description|
| ---|---|
| includeRecentPosts(optional)|If true, will return recent posts along with categories|

#### Examples:
```C#
IEnumerable<Category> categories = butterClient.ListCategories();

IEnumerable<Category> categoriesWithPosts = await butterClient.ListCategoriesAsync(includeRecentPosts: true);

```


### Retrieve a Single Category
Retrieving a single category returns [Category](#category-class)

#### RetrieveCategory() Parameters:
| Parameter|Description|
| ---|---|
|categorySlug|The slug of the category to be retrieved.|
| includeRecentPosts(optional)|If true, will return recent posts along with category|

#### Examples:
```C#
 Category dotnetCategory = butterClient.RetrieveCategory("dotnet");
 
 Category fsharpCategoryWithPosts = await butterClient.RetrieveCategoryAsync(categorySlug: "fsharp", includeRecentPosts: true);

```
## Feeds
Each of the feeds methods returns an [XmlDocument](https://msdn.microsoft.com/en-us/library/system.xml.xmldocument%28v=vs.110%29.aspx).


### RSS Feed
Retrieve a fully generated RSS feed for your blog.

#### Examples:
```C#
 XmlDocument rssFeed = butterClient.GetRSSFeed();
 
 XmlDocument rssFeed = await butterClient.GetRSSFeedAsync();

```

### Atom Feed
Retrieve a fully generated Atom feed for your blog.

#### Examples:
```C#
 XmlDocument atomFeed = butterClient.GetAtomFeed();
 
 XmlDocument atomFeed = await butterClient.GetAtomFeedAsync();

```

### Sitemap
Retrieve a fully generated sitemap for your blog.

#### Examples:
```C#
 XmlDocument siteMap = butterClient.GetSitemap();
 
 XmlDocument siteMap = await butterClient.GetSitemapAsync();

```

## Content Fields

**New in version 1.3.0**

By the power of .NET generics, Content Fields can now be deserialized by the library! The former method that would defer deserialization is still available to ease transition.

#### RetrieveContentFields() Parameters:
| Parameter|Description|
| ---|---|
|keys|String array of desired keys|
|parameterDictionary(optional)|Dictionary of additional parameters, such as "locale" or "test"|

#### RetrieveContentFields() Exceptions:
| Exception|Description|
| ---|---|
|[ContentFieldObjectMismatchException](#contentfieldobjectmismatchexception)|This exception will be thrown when the library can't fit the returned data into the passed object class. |

#### Examples:
```C#
var keys = new string[2] { "team_members[name=Elon]", "homepage_headline" };
var dict = new Dictionary<string, string>()
            {
                { "locale", "de" },
                { "test", "1" }
            };
var teamMembersAndHeadline = butterClient.RetrieveContentFields<TeamMembersHeadline>(keys, dict);

```

** Content Fields JSON documentation** :

As demonstrated in the [Content Fields documentation](https://buttercms.com/docs/api/#content-fields), any number of user-defined content fields can be retrieved from the API, these can get complicated in C# and you may choose to handle the response yourself. The RetrieveContentFieldsJSON() method will return the raw JSON response from the Butter API.

#### RetrieveContentFieldsJSON() Parameters:
| Parameter|Description|
| ---|---|
|keys|String array of desired keys|
|parameterDictionary(optional)|Dictionary of additional parameters, such as "locale" and "test"|

#### Examples:
```C#
var keys = new string[2] { "team_members[name=Elon]", "homepage_headline" };
var dict = new Dictionary&lt;string, string&gt;()
            {
                { "locale", "de" },
                { "test", "1" }
            };
var contentFields = await butterClient.RetrieveContentFieldsJSONAsync(keys, dict);

```

## Pages
### List Pages

Listing Pages returns a [PagesResponse](#pagesresponse-class) object. Full API documentation can be found at https://buttercms.com/docs/api/#list-pages-for-a-page-type.

#### ListPages() Parameters:
| Parameter|Description|
| ---|---|
|pageType| Desired page type|
|parameterDictionary| Dictionary of additional parameters|

#### ListPages() Exceptions:
| Exception|Description|
| ---|---|
|[PagesObjectMismatchException](#pagesobjectmismatchexception)|This exception will be thrown when the library can't fit the returned data into the passed object class. |

#### Examples:
```C#
PagesResponse<YourPage> productPages = butterClient.ListPages<YourPage>("products");

var paramterDict = new Dictionary<string, string>() 
{
    {"preview", "1"},
    {"field.category", "appetizers"},
    {"field.main_ingredient", "cheese"},
    {"order", "-date_published"},
    {"page", "7"},
    {"page_size", "10"}
};

PagesResponse<ExamplePage> recipePages = await butterClient.ListPagesAsync<ExamplePage>("recipes", parameterDict);

```
### Retrieve a Single Page
Retrieving a single page returns a [Page&lt;T&gt;](#page-class) object

#### RetrievePage() Parameters:
| Parameter|Description|
| ---|---|
|pageType| Desired page type|
|pageSlug| Slug of the desired page|
|parameterDictionary| Dictionary of additional parameters|

#### RetrievePage() Exceptions:
| Exception|Description|
| ---|---|
|[PagesObjectMismatchException](#pagesobjectmismatchexception)|This exception will be thrown when the library can't fit the returned data into the passed object class. |

#### Examples:
```C#
Page<ProductPage> saleOfTheDayPage = butterClient.RetrievePage<ProductPage>("products", "saleoftheday");

var paramterDict = new Dictionary<string, string>() 
{
    {"preview", "1"},
};
Page<ExamplePage> stuffedArtichokesPage = await butterClient.RetrievePageAsync<ExamplePage>("recipes", "stuffed-artichokes", paramterDict);

```

## Class Definitions

### PostsResponse Class:
| Property | Type|
|----|---|
|Meta| [PostsMeta](#postsmeta-class)|
|Data| IEnumerable&lt;[Post](#post-class)&gt;|

### PostsMeta Class:
| Property | Type|
|----|---|
|Count| int|
|NextPage| int?|
|PreviousPage| int?|

### Post Class:
| Property | Type|
|----|---|
|Url|string|
|Created|DateTime|
|Published|DateTime|
|Author|[Author](#author-class)|
|Categories|IEnumerable&lt;[Category](#category-class)&gt;
|FeaturedImage| string|
|Slug|string|
|Title|string|
|Body|string|
|Summary|string|
|SeoTitle|string|
|MetaDescription|string|
|Status|[PostStatusEnum](#poststatusenum)|

### PostStatusEnum:
|Constant|Value|
|---|---|
|Unknown|0|
|Draft|1|
|Published|2|

### PostResponse Class:
| Property | Type|
|----|---|
|Meta|[PostMeta](#postmeta-class)|
|Data|[Post](#post-class)|

### PostMeta Class:
| Property | Type|
|----|---|
|NextPost|[PostLight](#postlight-class)|
|PreviousPost|[PostLight](#postlight-class)|

### PostLight Class:
| Property | Type|
|----|---|
|Slug|string|
|Title|string|
|FeaturedImage|string|

### Author Class:
| Property | Type|
|----|---|
|FirstName| string|
|LastName| string|
|Email| string|
|Slug| string|
|Bio| string|
|Title| string|
|LinkedinUrl| string|
|FacebookUrl| string|
|InstagramUrl| string|
|PinterestUrl| string|
|TwitterHandle| string|
|ProfileImage| string|
|RecentPosts| IEnumerable&lt;[Post](#post-class)&gt;|

### Category Class:
| Property | Type|
|----|---|
|Name| string|
|Slug| string|
|RecentPosts| IEnumerable&lt;[Post](#post-class)&gt;|

### PagesResponse Class:
| Property | Type|
|----|---|
|Meta| [PageMeta](#pagemeta-class)|
|Data| IEnumerable&lt;[Page](#page-class)&lt;T&gt;&gt;|

### PageMeta Class:
| Property | Type|
|----|---|
|Count| int|
|PreviousPage| int?|
|NextPage| int?|

### Page Class:
| Property | Type|
|----|---|
|Slug| string|
|Fields|T|

## Exceptions

### InvalidKeyException
The library throws this exception when the Butter API key used to instatiate the client was not valid. 

### ContentFieldObjectMismatchException
This exception will be thrown when the library can't fit the returned data from a Content Field request into the passed object class.

### PagesObjectMismatchException
This exception will be thrown when the library can't fit the returned data from a Pages request into the passed object class.