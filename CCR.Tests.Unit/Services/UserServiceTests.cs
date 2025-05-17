using CCR.Application.DTOs;
using CCR.Application.Interfaces;
using CCR.Application.Services;
using CCR.Domain.Entities;
using CCR.Domain.Interfaces;
using Moq;
using Xunit;

namespace CCR.Tests.Unit.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldThrow_WhenEmailAlreadyExists()
    {
        // Arrange
        var request = new RegisterUserRequest
        {
            Name = "Saurav",
            Email = "saurav@example.com",
            PhoneNumber = "1234567890",
            Password = "Password123"
        };

        _userRepositoryMock.Setup(r => r.ExistsAsync(request.Email)).ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _userService.RegisterUserAsync(request));
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldReturnUserDto_WhenValidRequest()
    {
        // Arrange
        var request = new RegisterUserRequest
        {
            Name = "Saurav",
            Email = "saurav@example.com",
            PhoneNumber = "1234567890",
            Password = "Password123"
        };

        _userRepositoryMock.Setup(r => r.ExistsAsync(request.Email)).ReturnsAsync(false);
        _userRepositoryMock.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(
            (User user) => user
        );

        // Act
        var result = await _userService.RegisterUserAsync(request);

        // Assert
        Assert.Equal(request.Email, result.Email);
        Assert.Equal(request.Name, result.Name);
        Assert.NotEqual(Guid.Empty, result.Id);
    }
}
