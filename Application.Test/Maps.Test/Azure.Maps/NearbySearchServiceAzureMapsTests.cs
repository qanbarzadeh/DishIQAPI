using Moq;
using Moq.Protected;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Application.Services.AzureMaps;
using Domain.AzureVault;
using Microsoft.Extensions.Configuration;
using Application.DTO.Azure.maps;

namespace Application.Test.Maps.Test.Azure.Maps
{
    public class NearbySearchServiceAzureMapsTests
    {
        [Fact]
        public async Task Search_ReturnsCorrectStoreListDTO()
        {
            // Arrange
            var keyVaultServiceMock = new Mock<IKeyVaultService>();
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var configurationMock = new Mock<IConfiguration>();

            // Mocking IKeyVaultService
            string expectedApiKey = "test-api-key";
            keyVaultServiceMock.Setup(s => s.GetSecretAsync("azure-maps-key")).ReturnsAsync(expectedApiKey);

            // Mocking IConfiguration
            string expectedBaseUrl = "https://atlas.microsoft.com/search/poi/json";
            configurationMock.Setup(s => s["AzureMaps:BaseUrl"]).Returns(expectedBaseUrl);

            // Mocking HttpMessageHandler
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            string jsonResponse = @"{
                ""results"": [
                    {
                        ""poi"": { ""name"": ""Store 1"" },
                        ""address"": { ""freeformAddress"": ""Address 1"" },
                        ""position"": {
                            ""lat"": 1.234567,
                            ""lon"": 2.345678
                        }
                    },
                    {
                        ""poi"": { ""name"": ""Store 2"" },
                        ""address"": { ""freeformAddress"": ""Address 2"" },
                        ""position"": {
                            ""lat"": 3.456789,
                            ""lon"": 4.567890
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

            var service = new NearbySearchServiceAzureMaps(httpClientFactoryMock.Object, keyVaultServiceMock.Object, configurationMock.Object);

            // Act
            var result = await service.Search(new SearchRequestDTO
            {
                Latitude = "40.748817",
                Longitude = "-73.985428",
                Radius = 1000
            });

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
