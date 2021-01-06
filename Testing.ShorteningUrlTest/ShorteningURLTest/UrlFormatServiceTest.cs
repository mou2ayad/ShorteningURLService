using Microsoft.Extensions.Options;
using App.Component.ShorteningURL.Contracts;
using App.Component.ShorteningURL.Services;
using NUnit.Framework;
using App.Component.Utilities.ErrorHandling;

namespace Testing.Component.ShorteningUrlTest
{
    public class UrlFormatServiceTest
    {
        IUrlFormatService urlFormatService;
        public UrlFormatServiceTest()
        {
            urlFormatService = ServiceProviderFactory.GetServiceProvider().GetUrlFormatService();
        }        

        [Test]
        public void Same_URL_With_diff_QueryString_keys_order()
        {
            string firsturl = urlFormatService.FormateURL("https://App.com/sub?key1=10&key2=name&key3=lastname");
            string Second = urlFormatService.FormateURL("https://App.com/sub?key2=name&key3=lastname&key1=10");
            Assert.AreEqual(firsturl, Second);
        }
        [TestCase("https://App.com/sub?key1=10&key2=name&key3=lastname", "https://App.com/sub?key1=10&key2=name&key3=lastname", TestName = "test with https without subdmain")]
        [TestCase("http://App.com/sub?key1=10&key2=name&key3=lastname", "http://App.com/sub?key1=10&key2=name&key3=lastname", TestName = "test with http without subdmain")]
        [TestCase("https://www.App.com/sub?key1=10&key2=name&key3=lastname", "https://www.App.com/sub?key1=10&key2=name&key3=lastname", TestName = "test with https with subdmain")]
        [TestCase("App.com/sub?key1=10&key2=name&key3=lastname", "https://App.com/sub?key1=10&key2=name&key3=lastname", TestName = "test without subdmain and Protocol")]
        [TestCase("www.App.com/sub?key1=10&key2=name&key3=lastname", "https://www.App.com/sub?key1=10&key2=name&key3=lastname", TestName = "test with subdmain and without Protocol")]
        [TestCase("www.App.com/sub?", "https://www.App.com/sub", TestName = "test with subdmain and without Protocol and querysting")]
        [TestCase("https://www.App.com:4923/sub", "https://www.App.com:4923/sub", TestName = "test with port")]             
        public void Validate_FormateURL(string InputUrl,string expectedUrl)
        {
            Assert.AreEqual(urlFormatService.FormateURL(InputUrl), expectedUrl);
        }
        [TestCase("ftp://www.App.com:4923/sub", TestName = "test non http(s) Protocol")]
        [TestCase("App/sub?key1=10&key2=name&key3=lastname", TestName = "test Url without domain ")]
        [TestCase("FreeText", TestName = "test freetext as Url")]
        [TestCase("", TestName = "test empty Url")]
        [TestCase(" ", TestName = "test space empty Url")]
        [TestCase(null, TestName = "test null Url")]
        public void Test_invalid_url(string url)
        {
            string formatedUrl=null;
            TestDelegate d = new TestDelegate(
                () =>
                {
                    formatedUrl = urlFormatService.FormateURL(url);
                }
                );

            var exception = Assert.Catch(d);
            Assert.IsInstanceOf(typeof(InvalidRequestException), exception);
            Assert.IsNull(formatedUrl);
            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.IsClientException());                            
            Assert.IsTrue(exception.Message.Contains("invalid Url"));
        }
    }
}