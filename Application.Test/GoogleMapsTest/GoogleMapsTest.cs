using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Application.DTO.GoogleMaps;
using Application.Services.GoogleMaps;
using Domain.AzureVault;

namespace Application.Test.GoogleMapsTest
{
    public class NearbySearchServiceTests
    {
        [Fact]
        public async Task Search_ReturnsCorrectStoreListDTO()
        {
            // Arrange
            var keyVaultServiceMock = new Mock<IKeyVaultService>();
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();

            // Mocking IKeyVaultService
            string expectedApiKey = "test-api-key";
            keyVaultServiceMock.Setup(s => s.GetSecretAsync("Google-Maps-API-Key")).ReturnsAsync(expectedApiKey);

            // Mocking HttpMessageHandler
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            string jsonResponse = @"{
                ""results"": [
                    {
                        ""name"": ""Store 1"",
                        ""vicinity"": ""Address 1"",
                        ""geometry"": {
                            ""location"": {
                                ""lat"": 1.234567,
                                ""lng"": 2.345678
                            }
                        }
                    },
                    {
                        ""name"": ""Store 2"",
                        ""vicinity"": ""Address 2"",
                        ""geometry"": {
                            ""location"": {
                                ""lat"": 3.456789,
                                ""lng"": 4.567890
                            }
                        }
                    }
                ]
            }";
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var service = new NearbySearchService(httpClientFactoryMock.Object, keyVaultServiceMock.Object);

            // Act
            var result = await service.Search("40.748817,-73.985428", 1000, new[] { "grocery_or_supermarket" });

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Stores.Count > 0);

            Assert.Equal("Store 1", result.Stores[0].Name);
            Assert.Equal("Address 1", result.Stores[0].Address);
            Assert.Equal(1.234567, result.Stores[0].Latitude, 6);
            Assert.Equal(2.345678, result.Stores[0].Longitude, 6);

            Assert.Equal("Store 2", result.Stores[1].Name);
            Assert.Equal("Address 2", result.Stores[1].Address);
            Assert.Equal(3.456789, result.Stores[1].Latitude, 6);
            Assert.Equal(4.567890, result.Stores[1].Longitude, 6);
        }
    }
}
