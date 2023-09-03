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
            butterClient.MockSuccessfullPagesResponse(); 

            var dict = new Dictionary<string, string>()
            {
                {"fields.thing1", "1"},
            };
            var response = butterClient.ListPages<things>("things", dict);
            var page = response.Data.First();
            Assert.AreEqual(page.Name, PagesMocks.Page.Name);
            Assert.AreEqual(page.Slug, PagesMocks.Page.Slug);
            Assert.AreEqual(page.Updated, PagesMocks.Page.Updated);
            Assert.AreEqual(page.PageType, PagesMocks.Page.PageType);
            Assert.AreEqual(page.Fields.thing1, PagesMocks.Fields.thing1);
            Assert.AreEqual(page.Fields.thing2, PagesMocks.Fields.thing2);
        }

        [Test]
        public async Task ListPagesAsync_ShouldReturnPages()
        {
            butterClient.MockSuccessfullPagesResponse(); 

            var response = await butterClient.ListPagesAsync<things>("things");
            
            var page = response.Data.First();
            Assert.AreEqual(page.Name, PagesMocks.Page.Name);
            Assert.AreEqual(page.Slug, PagesMocks.Page.Slug);
            Assert.AreEqual(page.Updated, PagesMocks.Page.Updated);
            Assert.AreEqual(page.PageType, PagesMocks.Page.PageType);
            Assert.AreEqual(page.Fields.thing1, PagesMocks.Fields.thing1);
            Assert.AreEqual(page.Fields.thing2, PagesMocks.Fields.thing2);
        }

        [Test]
        public void ListPages_NoResults_ShouldReturnNull()
        {
            var response =  butterClient.ListPages<things>("nothings");
            Assert.IsNull(response);
        }

        [Test]
        public async Task ListPagesAsync_NoResults_ShouldReturnNull()
        {
            var response = await butterClient.ListPagesAsync<things>("nothings");
            Assert.IsNull(response);
        }
    }
}
