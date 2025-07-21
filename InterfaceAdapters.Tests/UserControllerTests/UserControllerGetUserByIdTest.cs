using Application.DTO;
using Application.IService;
using InterfaceAdapters.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InterfaceAdapters.Tests.UserControllerTests;

public class UserControllerGetUserByIdTest
{
    [Fact]
    public async Task WhenGetUsersIsCalled_ThenReturnOkWithUsers()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var user = new UserDTO { Id = Guid.NewGuid(), Names = "test", Email = "test", Surnames = "test" };

        mockUserService.Setup(service => service.GetById(It.IsAny<Guid>())).ReturnsAsync(user);

        var controller = new UserController(mockUserService.Object);

        // Act
        var result = await controller.GetUserById(user.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUser = Assert.IsType<UserDTO>(okResult.Value);
        Assert.Equal(user, returnedUser);
    }
}
