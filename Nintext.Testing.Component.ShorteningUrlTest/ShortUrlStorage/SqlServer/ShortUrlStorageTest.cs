using Microsoft.Data.SqlClient;
using Nintex.NetCore.Component.Common.Contracts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintex.Testing.Component.ShorteningUrlTest.ShortUrlStorage.SqlServer
{
    [TestFixture]
    public class ShortUrlStorageTest
    {
        private IShortURLStorage shortURLStorageService;
        public  string FirstCreatedKey { set; get; }
        public string createdUrl { set; get; }
        public ShortUrlStorageTest()
        {
            shortURLStorageService = ServiceProviderFactory.GetServiceProvider().GetShortURLStorageService();
        }        
        [Test, Order(1)]
        public void TestCreateNewURL()
        {
            FirstCreatedKey = UrlGenerator.RandomString(6);
            createdUrl = UrlGenerator.GenerateUniqueURL(FirstCreatedKey);
            var key=shortURLStorageService.CreateNewURLAsync(FirstCreatedKey, createdUrl).Result;                
            Assert.AreEqual(key, FirstCreatedKey);
        }
        [Test, Order(2)]
        public void TestGetUrlByKey()
        {                      
            var url = shortURLStorageService.GetUrlByKeyAsync(FirstCreatedKey).Result;
            Assert.AreEqual(url, createdUrl);
        }
        [Test, Order(3)]
        public void TestCreateURLAlreadyCreated()
        {
            var NewKey = UrlGenerator.RandomString(6);
            var key = shortURLStorageService.CreateNewURLAsync(NewKey, createdUrl).Result;
            Assert.AreNotEqual(key, NewKey);
            Assert.AreEqual(key, FirstCreatedKey);
        }
        [Test, Order(4)]
        public void TestDuplicatedKeyWithNewUrl()
        {
            string NewUrl = $"https://nintex.com/sub/{FirstCreatedKey}";
            TestDelegate d = new TestDelegate(
                () =>
                {
                    var key = shortURLStorageService.CreateNewURLAsync(FirstCreatedKey, NewUrl).Result;
                }
                );
            
            var exception= Assert.Catch(d);
            if (exception is AggregateException)                
                exception = exception.InnerException;            
            Assert.IsInstanceOf(typeof(SqlException), exception);
            Assert.AreEqual(((SqlException)exception).Number, 2601);
            Assert.IsTrue(exception.Message.Contains("Cannot insert duplicate key row in object 'dbo.Urls' with unique index 'IX_ShortKey'"));

        }
        [Test, Order(5)]
        public void TesGettingNotExistKey()
        {
            var NewKey = UrlGenerator.RandomString(6);
            var key = shortURLStorageService.GetUrlByKeyAsync(NewKey).Result;
            Assert.IsNull(key);            
        }

    }
}
