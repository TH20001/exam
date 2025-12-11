using finalProject.Controllers;
using finalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class AdminIndexTest
{
    [Fact]
    public async Task Admin_Index_Returns_View()
    {
        // using mock to make a mock json return for testing here cause why not
        var fakeJson = "[{\"id\":1,\"name\":\"Testing thingy\",\"price\":10,\"quantity\":5}]";

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
               "SendAsync",
               ItExpr.IsAny<HttpRequestMessage>(),
               ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(fakeJson)
            });

        var client = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new System.Uri("https://localhost:7151/api/")
        };

        var factoryMock = new Mock<IHttpClientFactory>();
        factoryMock.Setup(f => f.CreateClient("api")).Returns(client);

        var controller = new JunkController(factoryMock.Object);

        var result = await controller.Index();

        Assert.IsType<ViewResult>(result);
    }
}
