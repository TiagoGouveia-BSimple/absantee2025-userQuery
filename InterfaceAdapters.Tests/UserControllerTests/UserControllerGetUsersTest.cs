using Application.DTO;
using Application.IService;
using InterfaceAdapters.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InterfaceAdapters.Tests.UserControllerTests;

public class UserControllerGetUsersTest
{
    [Fact]
    public async Task WhenGetUsersIsCalled_ThenReturnOkWithUsers()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var users = new List<UserDTO>
        {
            new UserDTO { Id = Guid.NewGuid(), Names = "test", Email = "test", Surnames = "test" },
            new UserDTO { Id = Guid.NewGuid(), Names = "testinald", Email = "testinald", Surnames = "testinald" }
        };
        mockUserService.Setup(service => service.GetAll()).ReturnsAsync(users);

        var controller = new UserController(mockUserService.Object);

        // Act
        var result = await controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUsers = Assert.IsType<List<UserDTO>>(okResult.Value);
        Assert.Equal(users.Count, returnedUsers.Count);
    }
}
