using ButterCMS.Models;
using ButterCMS.Tests.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("ListPages")]
    public class ListPagesTests
    {
        private ButterCMSClientWithMockedHttp butterClient;

        [SetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpMockedButterClient();
        }

        [Test]
        public void ListPages_ShouldReturnPages()
        {
            var dict = new Dictionary<string, string>()
            {
                {"fields.thing1", "1"},
            };

            butterClient.MockSuccessfullPagesResponse(parameters: dict); 

            var response = butterClient.ListPages<Things>(PagesMocks.PageType, dict);
            var page = response.Data.First();
            Assert.AreEqual(page.Name, PagesMocks.Page.Name);
            Assert.AreEqual(page.Slug, PagesMocks.Page.Slug);
            Assert.AreEqual(page.Updated, PagesMocks.Page.Updated);
            Assert.AreEqual(page.PageType, PagesMocks.Page.PageType);
            Assert.AreEqual(page.Fields.Thing1, PagesMocks.Fields.Thing1);
            Assert.AreEqual(page.Fields.Thing2, PagesMocks.Fields.Thing2);
        }

        [Test]
        public async Task ListPagesAsync_ShouldReturnPages()
        {
            butterClient.MockSuccessfullPagesResponse(); 

            var response = await butterClient.ListPagesAsync<Things>(PagesMocks.PageType);
            
            var page = response.Data.First();
            Assert.AreEqual(page.Name, PagesMocks.Page.Name);
            Assert.AreEqual(page.Slug, PagesMocks.Page.Slug);
            Assert.AreEqual(page.Updated, PagesMocks.Page.Updated);
            Assert.AreEqual(page.PageType, PagesMocks.Page.PageType);
            Assert.AreEqual(page.Fields.Thing1, PagesMocks.Fields.Thing1);
            Assert.AreEqual(page.Fields.Thing2, PagesMocks.Fields.Thing2);
        }

        [Test]
        public void ListPages_NoResults_ShouldReturnNull()
        {
            var response =  butterClient.ListPages<Things>("nothings");
            Assert.IsNull(response);
        }

        [Test]
        public async Task ListPagesAsync_NoResults_ShouldReturnNull()
        {
            var response = await butterClient.ListPagesAsync<Things>("nothings");
            Assert.IsNull(response);
        }
    }
}
