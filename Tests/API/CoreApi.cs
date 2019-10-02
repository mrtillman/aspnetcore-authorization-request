using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using AuthDemo.API;
using AuthDemo.Constants;
using System.Net;
using System.Net.Http;
using Moq;
using Moq.Protected;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AuthDemo.Tests
{
  [TestClass]
    public class CoreApiTests
    {
        public CoreApiTests(){

            // TODO: refactor

            var mockConfiguration = Mock.Of<IConfiguration>();

            var mockServerUrls = Mock.Of<IServerUrls>();
            Mock.Get(mockServerUrls)
                .Setup(urls => urls.API)
                .Returns("https://api.counter-culture.io");

            var mockResponse = Mock.Of<HttpResponseMessage>();
            mockResponse.StatusCode = HttpStatusCode.OK;
            
            var jsonCounters = "[{\"_id\":\"5d16c0cd11ee4a3d6f44b045\",\"name\":\"alcohol\",\"value\":0,\"skip\":1,\"__v\":0},{\"_id\":\"5d16c0cd11ee4a3d6f44b046\",\"name\":\"tobacco\",\"value\":0,\"skip\":1,\"__v\":0},{\"_id\":\"5d16c0cd11ee4a3d6f44b047\",\"name\":\"firearms\",\"value\":0,\"skip\":1,\"__v\":0}]";
            mockResponse.Content = new StringContent(jsonCounters);

            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                              .Setup<Task<HttpResponseMessage>>(
                                  "SendAsync",
                                  ItExpr.IsAny<HttpRequestMessage>(),
                                  ItExpr.IsAny<CancellationToken>()
                              )
                              .ReturnsAsync(mockResponse);

            var httpClient = new HttpClient(mockMessageHandler.Object);

            coreApi = new CoreApi(mockConfiguration, mockServerUrls, httpClient);
        }

        private CoreApi coreApi { get; set; }

        [TestMethod]
        public async Task Should_Work()
        {
            var result = await coreApi.GetCounters();
            Assert.IsTrue(result.DidSucceed);
        }
    }
}
