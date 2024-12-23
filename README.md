# buttercms-csharp

.NET Standard (2.1) Library for ButterCMS API.

## Documentation

For a comprehensive list of examples, check out the [API documentation](https://buttercms.com/docs/api/).


## Installation

To install ButterCMS, run the following command in the [Package Manager Console](https://docs.nuget.org/ndocs/tools/package-manager-console)

```PowerShell
PM> Install-Package ButterCMS
```

The library can also be installed to the project via .NET CLI

```bash
dotnet add package ButterCMS
```

Or by adding the package manually to the project file

<!-- {x-release-please-start-version} -->
```xml
<ItemGroup>
<PackageReference Include="ButterCMS" Version="2.1.0" />
</ItemGroup>
```
<!-- {x-release-please-end} -->

## Usage

To get started with the Butter API, instantiate the ButterCMSClient with the API key found in the [Butter Admin Settings](https://buttercms.com/settings/). An optional timeout parameter can be passed as a [TimeSpan](https://msdn.microsoft.com/en-us/library/system.timespan%28v=vs.110%29.aspx); the default is 10 seconds.

```C#
using ButterCMS;
...
var butterClient = new ButterCMSClient("API KEY");
```

If the application will be making many Butter API calls, it is recommended to store and re-use the client object.

Each Butter client method has a synchronous version and an asynchronous version. Asynchronous methods are appended with the word "Async".

### Preview mode

Preview mode can be used to setup a staging website for previewing content fields or for testing content during local development. To fetch content from preview mode add an additional argument, `true`, to the package initialization:

```C#
using ButterCMS;
...
bool previewMode = true;

var butterClient = new ButterCMSClient("API KEY", null, 3, null, previewMode);
```

## Sections

* [Posts](#posts)
* [Authors](#authors)
* [Categories](#categories)
* [Feeds](#feeds)
* [Collections](#collections)
* [Pages](#pages)
* [Class Definitions](#class-definitions)
* [Exceptions](#exceptions)

## Posts

### List Posts

Listing posts returns a [PostsResponse](#postsresponse-class) object. This object consists of a [metadata](#postsmeta-class) object and IEnumerable&lt;[Post](#post-class)&gt;

#### ListPosts() Parameters

| Parameter|Default|Description|
| ---|---|---|
| page(optional) | 1 | Used to paginate through older posts. |
| pageSize(optional) | 10 |  Used to set the number of blog posts shown per page. |
| excludeBody(optional) | false | When true, does not return the full post body. Useful for keeping response size down when showing a list of blog posts. |
|authorSlug(optional) | |Filter posts by an author’s slug.|
|categorySlug(optional) | | Filter posts by a category’s slug.

#### Examples

```C#
PostsResponse posts = butterClient.ListPosts();

PostsResponse filteredPosts = await butterClient.ListPostsAsync(page: 2, pageSize: 5, excludeBody: true, authorSlug: "alice", categorySlug: "dot-net");
```

### Retrieving a Single Post

Retrieving a single Post will return a PostResponse object. This object consists of a single [Post](#post-class) and Post [Metadata](#postmeta-class). Post Metadata offers hints about the Previous and Next posts.

#### RetrievePost() Parameters

| Parameter|Description|
| ---|---|
| slug|The slug of the post to be retrieved.|

#### Examples

```C#
PostResponse controversialPost = butterClient.RetrievePost("tabs-vs-spaces-throwdown");

PostResponse safePost = await butterClient.RetrievePostAsync("puppies-and-kittens");

```

### Searching Posts

Searching posts will return the same object as listing posts, [PostsResponse](#postsresponse-class)

#### SearchPosts() Parameters

| Parameter|Default|Description|
| ---|---|---|
| query |  | Search query |
| page(optional) | 1 | Used to paginate through older posts. |
| pageSize(optional) | 10 |  Used to set the number of blog posts shown per page. |

#### Examples

```C#
PostsResponse posts = butterClient.SearchPosts("spaceships");

PostsResponse caffeinePosts = await butterClient.SearchPostsAsync(query: "coffee", page: 3, pageSize: 5);

```

## Authors

### List Authors

Listing Authors returns IEnumerable&lt;[Author](#author-class)&gt;

#### ListAuthors() Parameters

| Parameter|Description|
| ---|---|
| includeRecentPosts(optional)|If true, will return the author's recent posts in the response|

#### Examples

```C#
IEnumerable<Author> authors = butterClient.ListAuthors();

IEnumerable<Author> authorsWithPosts = await butterClient.ListAuthorsAsync(includeRecentPosts: true);

```

### Retrieve a Single Author

Retrieving an author returns an [Author](#author-class) object

#### RetrieveAuthor() Parameters

| Parameter|Description|
| ---|---|
|authorSlug|The slug of the author to be retrieved.|
| includeRecentPosts(optional)|If true, will return the author's recent posts in the response|

#### Examples

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

#### Examples

```C#
IEnumerable<Category> categories = butterClient.ListCategories();

IEnumerable<Category> categoriesWithPosts = await butterClient.ListCategoriesAsync(includeRecentPosts: true);

```

### Retrieve a Single Category

Retrieving a single category returns [Category](#category-class)

#### RetrieveCategory() Parameters

| Parameter|Description|
| ---|---|
|categorySlug|The slug of the category to be retrieved.|
| includeRecentPosts(optional)|If true, will return recent posts along with category|

#### Examples

```C#
 Category dotnetCategory = butterClient.RetrieveCategory("dotnet");

 Category fsharpCategoryWithPosts = await butterClient.RetrieveCategoryAsync(categorySlug: "fsharp", includeRecentPosts: true);

```

## Feeds

Each of the feeds methods returns an [XmlDocument](https://msdn.microsoft.com/en-us/library/system.xml.xmldocument%28v=vs.110%29.aspx).


### RSS Feed

Retrieve a fully generated RSS feed for your blog.

#### Examples

```C#
 XmlDocument rssFeed = butterClient.GetRSSFeed();

 XmlDocument rssFeed = await butterClient.GetRSSFeedAsync();

```

### Atom Feed

Retrieve a fully generated Atom feed for your blog.

#### Examples

```C#
 XmlDocument atomFeed = butterClient.GetAtomFeed();

 XmlDocument atomFeed = await butterClient.GetAtomFeedAsync();

```

### Sitemap

Retrieve a fully generated sitemap for your blog.

#### Examples

```C#
 XmlDocument siteMap = butterClient.GetSitemap();

 XmlDocument siteMap = await butterClient.GetSitemapAsync();

```

## Collections

**New in version 1.3.0**

By the power of .NET generics, Collections can now be deserialized by the library! The former method that would defer deserialization is still available to ease transition.

#### RetrieveContentFields() Parameters

| Parameter|Description|
| ---|---|
|key|String array of the Collection key|
|parameterDictionary(optional)|Dictionary of additional parameters, such as "locale" or "preview"|

#### RetrieveContentFields() Exceptions:

| Exception|Description|
| ---|---|
|[ContentFieldObjectMismatchException](#contentfieldobjectmismatchexception)|This exception will be thrown when the library can't fit the returned data into the passed object class. |

#### Examples

```C#
var key = new string[1] { "collection_key" };
var dict = new Dictionary<string, string>()
            {
                { "locale", "de" }
            };
var teamMembersAndHeadline = butterClient.RetrieveContentFields<TeamMembersHeadline>(key, dict);

```

** Collection JSON documentation** :

As demonstrated in the [Collection documentation](https://buttercms.com/docs/api/?csharp#collections), any number of user-defined Collections can be retrieved from the API, these can get complicated in C# and you may choose to handle the response yourself. The RetrieveContentFieldsJSON() method will return the raw JSON response from the Butter API.

#### RetrieveContentFieldsJSON() Parameters

| Parameter|Description|
| ---|---|
|key|String array of the Collection key|
|parameterDictionary(optional)|Dictionary of additional parameters, such as "locale" or "preview"|

#### Examples

```C#
var key = new string[1] { "collection_key" };
var dict = new Dictionary<string, string>()
            {
                { "locale", "de" }
            };
var contentFields = await butterClient.RetrieveContentFieldsJSONAsync(key, dict);

```

## Pages

[ButterCMS Pages](https://buttercms.com/blog/page-types-cms-powered-pages-for-any-tech-stack) can be retrieved by first defining the custom Page Type as a class. These custom Page Types are defined in the [Butter admin](https://buttercms.com/pages/).

### List Pages

Listing Pages returns a [PagesResponse&lt;T&gt;](#pagesresponse-class) object. Full API documentation can be found at [https://buttercms.com/docs/api/#list-pages-for-a-page-type](https://buttercms.com/docs/api/#list-pages-for-a-page-type).

#### ListPages() Parameters

| Parameter|Description|
| ---|---|
|pageType| Desired page type|
|parameterDictionary| Dictionary of additional parameters. These options are described in greater detail in the [full API documentation](https://buttercms.com/docs/api/#get-multiple-pages-(page-type)). |

#### ListPages() Exceptions

| Exception|Description|
| ---|---|
|[PagesObjectMismatchException](#pagesobjectmismatchexception)|This exception will be thrown when the library can't fit the returned data into the passed object class. |

### Retrieve a Single Page

Retrieving a single page returns a [PageResponse&lt;T&gt;](#page-response-class) object

#### RetrievePage() Parameters

| Parameter|Description|
| ---|---|
|pageType| Desired page type|
|pageSlug| Slug of the desired page|
|parameterDictionary| Dictionary of additional parameters|

#### RetrievePage() Exceptions:
| Exception|Description|
| ---|---|
|[PagesObjectMismatchException](#pagesobjectmismatchexception)|This exception will be thrown when the library can't fit the returned data into the passed object class. |

#### Examples

##### Page Type Definition in the Butter Admin

![alt text](/Examples/RecipePageType.png "Page Type Definition")

##### Controller and ViewModel examples

```C#

public namespace HungryDevApp
{
    public class RecipePage
    {
        public string category { get; set; }
        public string recipe_name { get; set; }
        public string main_ingredient { get; set; }
        public double estimated_cooking_time_in_minutes { get; set; }
        public string ingredient_list { get; set; }
        public string instructions { get; set; }
    }

    public class RecipesController : Controller
    {
        [Route(recipes/)]
        public virtual ActionResult Index(int page = 1, int pageSize = 10)
        {
            var butterClient = new ButterCMSClient("API KEY");

            var parameterDict = new Dictionary<string, string>()
            {
                {"page", page.ToString()},
                {"page_size", pageSize.ToString()}
            };

            PagesResponse<RecipePage> recipePages = butterClient.ListPages<RecipePage>("recipe", parameterDict);

            var viewModel = new RecipesViewModel();
            viewModel.PreviousPageNumber = recipePages.Meta.PreviousPage;
            viewModel.NextPageNumber = recipePages.Meta.NextPage;
            viewModel.PagesCount = recipePages.Meta.Count;

            viewModel.Recipes = new List<RecipeViewModel>();
            foreach (Page<RecipePage> recipe in recipePages.Data)
            {
                RecipeViewModel recipeViewModel = new RecipeViewModel();
                recipeViewModel.Category = recipe.Fields.category;
                recipeViewModel.RecipeName = recipe.Fields.recipe_name;
                recipeViewModel.MainIngredient = recipe.Fields.main_ingredient;
                recipeViewModel.EstimatedCookingTimeInMinutes = recipe.Fields.estimated_cooking_time_in_minutes;
                recipeViewModel.IngredientList = recipe.Fields.ingredient_list;
                recipeViewModel.Instructions = recipe.Fields.instructions;

                viewModel.Recipes.Add(recipeViewModel);
            }

            return View(viewModel);
        }

        [Route(recipe/{slug})]
        public virtual ActionResult Recipe(string slug)
        {
            var butterClient = new ButterCMSClient("API KEY");

            PageResponse<RecipePage> recipe = butterClient.RetrievePage<RecipePage>("recipe", slug);

            var viewModel = new RecipeViewModel();
            viewModel.Category = recipe.Data.Fields.category;
            viewModel.RecipeName = recipe.Data.Fields.recipe_name;
            viewModel.MainIngredient = recipe.Data.Fields.main_ingredient;
            viewModel.EstimatedCookingTimeInMinutes = recipe.Data.Fields.estimated_cooking_time_in_minutes;
            viewModel.IngredientList = recipe.Data.Fields.ingredient_list;
            viewModel.Instructions = recipe.Data.Fields.instructions;

            return View(viewModel);
        }
    }

    public class RecipesViewModel
    {
        public List<RecipeViewModel> Recipes { get; set; }
        public int? PreviousPageNumber { get; set; }
        public int? NextPageNumber { get; set; }
        public int PagesCount { get; set; }
    }

    public class RecipeViewModel
    {
        public string Category { get; set; }
        public string RecipeName { get; set; }
        public string MainIngredient { get; set; }
        public double EstimatedCookingTimeInMinutes { get; set; }
        public string IngredientList { get; set; }
        public string Instructions { get; set; }
    }

}


```

##### Example View usage for ListPages

```HTML
@using HungryDevApp
@{
Layout = "~/Views/Shared/Layouts/_Layout.cshtml";
}
@model RecipesViewModel
<div>
    <a href="/recipes/?page=@{Model.PreviousPageNumber}&pageSize=10">Previous page</a>
    <a href="/recipes/?page=@{Model.NextPageNumber}&pageSize=10">Next page</a>
</div>
<div>
    <p>@{Model.PagesCount} recipes total</p>
</div>
<div>
    <ul>
        @foreach(var page in Model.Recipes)
        {
            <li>
                <a href="/recipe/@{page.Slug}">@{page.RecipeName}
            </li>
        }
    </ul>
</div>

```

##### Example View usage for RetrievePage

```HTML
@using HungryDevApp
@{
Layout = "~/Views/Shared/Layouts/_Layout.cshtml";
}
@model RecipeViewModel
<div>
    <h2>@{Model.RecipeName}</h2>
    <p>Estimated cooking time: @{Model.EstimatedCookingTimeInMinutes} minutes</p>
    <h3>Ingredients:</h3>
    <p>@{Model.IngredientList}</p>
    <h3>Instructions:</h3>
    <p>@{Model.Instructions}</p>
</div>

```

## Class Definitions

### PostsResponse Class

| Property | Type|
|----|---|
|Meta| [PostsMeta](#postsmeta-class)|
|Data| IEnumerable&lt;[Post](#post-class)&gt;|

### PostsMeta Class

| Property | Type|
|----|---|
|Count| int|
|NextPage| int?|
|PreviousPage| int?|

### Post Class

| Property | Type|
|----|---|
|Url|string|
|Created|DateTime|
|Published|DateTime|
|Author|[Author](#author-class)|
|Categories|IEnumerable&lt;[Category](#category-class)&gt;
|FeaturedImage| string|
|FeaturedImageAlt| string|
|Slug|string|
|Title|string|
|Body|string|
|Summary|string|
|SeoTitle|string|
|MetaDescription|string|
|Status|[StatusEnum](#statusenum)|
|Scheduled|DateTime|

### StatusEnum

|Constant|Value|
|---|---|
|Unknown|0|
|Draft|1|
|Published|2|
|Scheduled|3|


### PostResponse Class

| Property | Type|
|----|---|
|Meta|[PostMeta](#postmeta-class)|
|Data|[Post](#post-class)|

### PostMeta Class

| Property | Type|
|----|---|
|NextPost|[PostLight](#postlight-class)|
|PreviousPost|[PostLight](#postlight-class)|

### PostLight Class

| Property | Type|
|----|---|
|Slug|string|
|Title|string|
|FeaturedImage|string|

### Author Class

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

### Category Class

| Property | Type|
|----|---|
|Name| string|
|Slug| string|
|RecentPosts| IEnumerable&lt;[Post](#post-class)&gt;|

### PagesResponse Class

| Property | Type|
|----|---|
|Meta| [PageMeta](#pagemeta-class)|
|Data| IEnumerable&lt;[Page](#page-class)&lt;T&gt;&gt;|

### PageMeta Class

| Property | Type|
|----|---|
|Count| int|
|PreviousPage| int?|
|NextPage| int?|

### PageResponse Class

| Property | Type|
|----|---|
|Data| [Page](#page-class)&lt;T&gt;|

### Page Class

| Property | Type|
|----|---|
|Slug| string|
|Updated| DateTime|
|Published| DateTime?|
|PageType| string|
|Fields|T|
|Status|[StatusEnum](#statusenum)|
|Scheduled| DateTime|

## Exceptions

## Testing

To run SDK test just simply:

```
PM> dotnet test
```

Or use Visual Studio Test Explorer. 

### InvalidKeyException

The library throws this exception when the Butter API key used to instatiate the client was not valid.

### ContentFieldObjectMismatchException

This exception will be thrown when the library can't fit the returned data from a Content Field request into the passed object class.

### PagesObjectMismatchException

This exception will be thrown when the library can't fit the returned data from a Pages request into the passed object class.

