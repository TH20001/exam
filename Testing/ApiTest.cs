using finalProject.Controllers;
using finalProject.Data;
using finalProject.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class ApiTest
{
    [Fact]
    public async Task Api_Returns_Json_List()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        using var context = new ApplicationDbContext(options);

        context.Junk.Add(new Junk { Id = 1, Name = "Test", Price = 10, Quantity = 2 });
        context.SaveChanges();

        var controller = new JunkApiController(context);

        var result = await controller.Get();

        Assert.NotNull(result.Value);
        Assert.Single(result.Value);
    }
}
