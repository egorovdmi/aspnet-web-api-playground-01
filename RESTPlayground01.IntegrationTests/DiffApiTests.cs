using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTPlayground01.IntegrationTests.Utils;

namespace RESTPlayground01.IntegrationTests
{
    [TestClass]
    public class DiffApiTests
    {
        private static int _diffUnderTestId;
        private DiffsUrlHelper _urlHelper;
        private HttpClient _httpClient;

        [TestInitialize]
        public void BeforeTest()
        {
            // increment working id => In each test we need unique id.
            _diffUnderTestId++;

            // setup http client
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(string.Format("{0}:{1}", 
                ServiceHostSetup.WebapiServiceBaseurl,
                ServiceHostSetup.WebapiServicePort));
            _urlHelper = new DiffsUrlHelper("/v1");
        }

        [TestCleanup]
        public void Cleanup()
        {
            _httpClient.Dispose();
        }

        [TestMethod]
        public async Task Put_LeftSide_201Status()
        {
            var url = _urlHelper.PutLeftSide(_diffUnderTestId);
            var result = await PutData(url, "AAAAAA==");
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [TestMethod]
        public async Task Put_RightSide_201Status()
        {
            var url = _urlHelper.PutRightSide(_diffUnderTestId);
            var result = await PutData(url, "AAAAAA==");
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [TestMethod]
        public async Task Put_LeftSideThenRightSide_201Status()
        {
            await Put_LeftSide_201Status();
            await Put_RightSide_201Status();
        }

        [TestMethod]
        public async Task Put_LeftSideNullContent_400Status()
        {
            var url = _urlHelper.PutLeftSide(_diffUnderTestId);
            var result = await PutData(url, null);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public async Task Put_RightSideNullContent_400Status()
        {
            var url = _urlHelper.PutRightSide(_diffUnderTestId);
            var result = await PutData(url, null);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public async Task GetDiff_OnlyLeftSideIsDefined_404Status()
        {
            // put left side content
            await PutData(_urlHelper.PutLeftSide(_diffUnderTestId), "AAAAAA==");

            // try to get diff
            var url = _urlHelper.GetDiff(_diffUnderTestId);
            var result = await _httpClient.GetAsync(url);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public async Task GetDiff_OnlyRightSideIsDefined_404Status()
        {
            // put left side content
            await PutData(_urlHelper.PutRightSide(_diffUnderTestId), "AAAAAA==");

            // try to get diff
            var url = _urlHelper.GetDiff(_diffUnderTestId);
            var result = await _httpClient.GetAsync(url);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public async Task GetDiff_BothSidesAreNotDefined_404Status()
        {
            var url = _urlHelper.GetDiff(_diffUnderTestId);

            var result = await _httpClient.GetAsync(url);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public async Task GetDiff_EqualContent_200StatusAndResponseTextIsValid()
        {
            await PutData(_urlHelper.PutLeftSide(_diffUnderTestId), "AAAAAA==");
            await PutData(_urlHelper.PutRightSide(_diffUnderTestId), "AAAAAA==");
            var url = _urlHelper.GetDiff(_diffUnderTestId);

            var result = await _httpClient.GetAsync(url);
            var responseText = await result.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(DiffApiTestsExpectedResponses.GetDiff_EqualContent_200StatusAndResponseIsValid,
                responseText);
        }

        [TestMethod]
        public async Task GetDiff_SameSizeDifferentContent_200StatusAndResponseTextIsValid()
        {
            await PutData(_urlHelper.PutLeftSide(_diffUnderTestId), "AAAAAA==");
            await PutData(_urlHelper.PutRightSide(_diffUnderTestId), "AQABAQ==");
            var url = _urlHelper.GetDiff(_diffUnderTestId);

            var result = await _httpClient.GetAsync(url);
            var responseText = await result.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(DiffApiTestsExpectedResponses.GetDiff_SameSizeDifferentContent_200StatusAndResponseIsValid,
                responseText);
        }

        [TestMethod]
        public async Task GetDiff_DifferentSize_200StatusAndResponseTextIsValid()
        {
            await PutData(_urlHelper.PutLeftSide(_diffUnderTestId), "AAA=");
            await PutData(_urlHelper.PutRightSide(_diffUnderTestId), "AQABAQ==");
            var url = _urlHelper.GetDiff(_diffUnderTestId);

            var result = await _httpClient.GetAsync(url);
            var responseText = await result.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(DiffApiTestsExpectedResponses.GetDiff_DifferentSize_200StatusAndResponseTextIsValid,
                responseText);
        }

        private async Task<HttpResponseMessage> PutData(string url, string data)
        {
            var content = data == null 
                ? @"{ ""data"": null }"
                : string.Format(@"{{ ""data"": ""{0}"" }}", data);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await _httpClient.PutAsync(url, stringContent);
        }
    }
}
