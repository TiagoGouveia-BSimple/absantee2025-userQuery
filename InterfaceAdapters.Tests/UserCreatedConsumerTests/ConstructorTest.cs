using Application.IService;
using Moq;
using Xunit;

namespace InterfaceAdapters.Tests.UserCreatedConsumerTests;

public class ConstructorTest
{
    [Fact]
    public void WhenConstructorIsCalled_ThenObjectIsInstantiated()
    {
        // Arrange
        var serviceMock = new Mock<IUserService>();

        // Act
        var consumer = new UserCreatedConsumer(serviceMock.Object);

        // Assert
    }
}
