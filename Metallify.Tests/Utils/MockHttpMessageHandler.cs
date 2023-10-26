using Metallify.API.Models;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace Metallify.Tests.Utils;

internal static class MockHttpMessageHandler<T>
{
    internal static Mock<HttpMessageHandler> SetupGetResourceList(List<T> expectedResponse)
    {
        // mock returns a list of resources 
        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
        };

        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var handler = new Mock<HttpMessageHandler>();

        handler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(mockResponse);

        return handler;
    }

    internal static Mock<HttpMessageHandler> SetupGetResourceList(List<T> expectedResponse, string endpoint)
    {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
        };

        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var handler = new Mock<HttpMessageHandler>();

        handler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(mockResponse);

        return handler;
    }

    internal static Mock<HttpMessageHandler> SetupReturn404()
    {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
            Content = new StringContent("")
        };

        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var handler = new Mock<HttpMessageHandler>();

        handler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(mockResponse);

        return handler;
    }
}
