
using App.Component.ShorteningURL.Contracts;
using Testing.Component.ShorteningUrlTest.ShortUrlStorage.SqlServer;
using NUnit.Framework;
using System;
using System.Net.Http;


namespace Testing.Component.ShorteningUrlTest.ShorteningURLTest
{
    [TestFixture]
    public class ShorteningServiceTest
    {
        private readonly IShorteningService shorteningService;              
        public ShorteningServiceTest()
        {
            shorteningService = ServiceProviderFactory.GetServiceProvider().GetShorteningService();           
        }
        [Test]
        public void TestGetActualUrlByShortkey()
        {
            ShortUrlStorageTest shortUrlStorageTest = new ShortUrlStorageTest();
            shortUrlStorageTest.TestCreateNewURL();
            var url = shorteningService.GetActualUrlByShortkey(shortUrlStorageTest.FirstCreatedKey);
            Assert.AreEqual(shortUrlStorageTest.createdUrl, url);
        }
        [Test]
        public void TestRegisterUrlAsync()
        {            
            var newurl =UrlGenerator.GenerateUniqueURL(UrlGenerator.RandomString(6));
            var shortUrl = shorteningService.RegisterUrlAsync(newurl).Result;
            HttpClient client = new HttpClient();
            var response= client.GetAsync(shortUrl).Result;            
            Assert.AreEqual(response.RequestMessage.RequestUri.AbsoluteUri, newurl);
        }
    }
}
