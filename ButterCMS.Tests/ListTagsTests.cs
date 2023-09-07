using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("ListTags")]
    public class ListTagsTests
    {

        private ButterCMSClientWithMockedHttp butterClient;

        [SetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpMockedButterClient();
        }

        [Test]
        public void ListTags_ShouldReturnListOfTagsWithoutPosts()
        {
            butterClient.MockSuccessfullTagsResponse();

            var response = butterClient.ListTags();
            Assert.IsNotNull(response);

            var tag = response.FirstOrDefault();
            Assert.AreEqual(TagsMocks.Tag.Slug, tag.Slug);
            Assert.AreEqual(TagsMocks.Tag.Name, tag.Name);
            Assert.IsNull(tag.RecentPosts);
        }

        [Test]
        public async Task ListTagsAsync_ShouldReturnListOfTagsWithoutPosts()
        {
            butterClient.MockSuccessfullTagsResponse();

            var response = await butterClient.ListTagsAsync();
            Assert.IsNotNull(response);

            var tag = response.FirstOrDefault();
            Assert.AreEqual(TagsMocks.Tag.Slug, tag.Slug);
            Assert.AreEqual(TagsMocks.Tag.Name, tag.Name);
            Assert.IsNull(tag.RecentPosts);
        }

        [Test]
        public void ListTags_ShouldReturnListOfTagsWithPosts()
        {
            butterClient.MockSuccessfullTagsWithPostsResponse();

            var response = butterClient.ListTags(true);
            Assert.IsNotNull(response);

            var tag = response.FirstOrDefault();
            Assert.AreEqual(TagsMocks.Tag.Slug, tag.Slug);
            Assert.AreEqual(TagsMocks.Tag.Name, tag.Name);
            Assert.IsNotEmpty(tag.RecentPosts);

            Assert.AreEqual(PostsMocks.Post.Slug, tag.RecentPosts.FirstOrDefault().Slug);
        }

        [Test]
        public async Task ListTagsAsync_ShouldReturnListOfTagsWithPosts()
        {
            butterClient.MockSuccessfullTagsWithPostsResponse();

            var response = await butterClient.ListTagsAsync(true);
            Assert.IsNotNull(response);

            var tag = response.FirstOrDefault();
            Assert.AreEqual(TagsMocks.Tag.Slug, tag.Slug);
            Assert.AreEqual(TagsMocks.Tag.Name, tag.Name);
            Assert.IsNotEmpty(tag.RecentPosts);
            Assert.AreEqual(PostsMocks.Post.Slug, tag.RecentPosts.FirstOrDefault().Slug);
        }
    }
}
