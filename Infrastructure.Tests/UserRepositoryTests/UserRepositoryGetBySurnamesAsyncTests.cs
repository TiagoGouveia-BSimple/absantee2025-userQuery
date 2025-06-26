using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Visitor;
using Infrastructure.DataModel;
using AutoMapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Domain.Models;
using Domain.Factory;
using Infrastructure.Resolvers;

namespace Infrastructure.Tests.UserRepositoryTests;
public class UserRepositoryGetBySurnamesAsyncTests
{
    [Theory]
    [InlineData("Silva", "Silvana", 2)]
    [InlineData("Silva", "Gomes", 1)]
    [InlineData("Gomes", "Pereira", 0)]
    public async Task WhenGettingBySurnamesAsync_ShouldReturnCorrectUsers(string surname1, string surname2, int expectedCount)
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new AbsanteeContext(options);

        var id1 = new Guid();
        var user1 = new UserDataModel
        {
            Id = id1,
            Names = "John",
            Surnames = surname1,
            Email = "user1@gmail.com"
        };

        var id2 = new Guid();
        var user2 = new UserDataModel
        {
            Id = id2,
            Names = "Johnny",
            Surnames = surname2,
            Email = "user2@gmail.com"
        };

        var userMock = new Mock<IUser>();
        userMock.Setup(u => u.Id).Returns(user1.Id);
        userMock.Setup(u => u.Names).Returns(user1.Names);
        userMock.Setup(u => u.Surnames).Returns(user1.Surnames);

        context.Users.AddRange(user1, user2);
        await context.SaveChangesAsync();

        var userFactoryMock = new Mock<IUserFactory>();
        userFactoryMock.Setup(f => f.Create(user1)).Returns(
            userMock.Object);
        userFactoryMock.Setup(f => f.Create(user2)).Returns(
            userMock.Object);

        var converter = new UserDataModelConverter(userFactoryMock.Object);

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<IUser, UserDataModel>();
            cfg.CreateMap<UserDataModel, IUser>().ConvertUsing(converter);
        });
        var mapper = mockMapper.CreateMapper();

        var userRepository = new UserRepositoryEF(context, mapper);

        // Act
        var result = await userRepository.GetBySurnamesAsync("Silva");

        // Assert
        Assert.Equal(expectedCount, result.Count());
        var users = result.Select(id => userRepository.GetById(id));
        if (expectedCount > 0)
        {
            Assert.All(users, u => Assert.Contains("Silva", u.Surnames, StringComparison.OrdinalIgnoreCase));
        }
        else
        {
            Assert.Empty(result);
        }
    }
}

