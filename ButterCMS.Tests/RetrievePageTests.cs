using ButterCMS.Tests.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("RetrievePage")]
    public class RetrievePageTests
    {
        private ButterCMSClientWithMockedHttp butterClient;

        [SetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpMockedButterClient();
        }

        [Test]
        public void RetrievePage_ShouldReturnPage()
        {
            butterClient.MockSuccessfullPageResponse(PagesMocks.Page.Slug);

            var response = butterClient.RetrievePage<Things>(PagesMocks.Page.PageType, PagesMocks.Page.Slug);
            Assert.IsNotNull(response);

            var page = response.Data;
            Assert.AreEqual(PagesMocks.Page.Name, page.Name);
            Assert.AreEqual(PagesMocks.Page.Slug, page.Slug);
            Assert.AreEqual(PagesMocks.Page.Updated, page.Updated);
            Assert.AreEqual(PagesMocks.Page.Fields.Thing1, page.Fields.Thing1);
        }

        [Test]
        public async Task RetrievePageAsync_ShouldReturnPage()
        {
            butterClient.MockSuccessfullPageResponse(PagesMocks.Page.Slug);

            var response = await butterClient.RetrievePageAsync<Things>(PagesMocks.Page.PageType, PagesMocks.Page.Slug);
            Assert.IsNotNull(response);

            var page = response.Data;
            Assert.AreEqual(PagesMocks.Page.Name, page.Name);
            Assert.AreEqual(PagesMocks.Page.Slug, page.Slug);
            Assert.AreEqual(PagesMocks.Page.Updated, page.Updated);
            Assert.AreEqual(PagesMocks.Page.Fields.Thing1, page.Fields.Thing1);
        }

        [Test]
        public void RetrievePage_NoResults_ShouldReturnNull()
        {
            var slug = "nothings";

            butterClient.MockSuccessfullNullPageResponse(slug);

            var response = butterClient.RetrievePage<Things>(slug, slug);
            Assert.IsNull(response);
        }

        [Test]
        public async Task RetrievePageAsync_NoResults_ShouldReturnNull()
        {
            var slug = "nothings";

            butterClient.MockSuccessfullNullPageResponse(slug);

            var response = await butterClient.RetrievePageAsync<Things>(slug, slug);
            Assert.IsNull(response);
        }
    }
}
